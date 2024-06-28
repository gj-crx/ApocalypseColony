using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Units;
using Factions;
using System;
using Zenject;
using Unity.Netcode;

[System.Serializable]
public class GameDataBase : IDataBase
{
    public int MapRadius = 100;
    public int ShardsCount = 4;

    public List<Unit> Units = new List<Unit>();

    public List<Faction> Factions = new List<Faction>();

    public List<ISynchronizableObject> synchronizableObjects = new List<ISynchronizableObject>();

    public bool IsKilled => isKilled;
    private bool isKilled = false;

    private ShardOfDataBase[,] shards;
    private byte shardMapRadius = 4;

    private int shardRadiusInUnits { get { return MapRadius / shardMapRadius; } }

    [Inject]
    public GameDataBase()
    {
        //Creating shards
        shardMapRadius = (byte)Mathf.Sqrt(ShardsCount);
        shards = new ShardOfDataBase[shardMapRadius, shardMapRadius];
        for (sbyte y = 0; y < shardMapRadius; y++)
            for (sbyte x = 0; x < shardMapRadius; x++)
            {
                shards[x, y] = new ShardOfDataBase(this);
            }
    }
    private void SpreadUnitsByShards()
    {
        foreach (var unit in Units)
        {
            byte shardX = (byte)(unit.Position.x / shardMapRadius);
            byte shardY = (byte)(unit.Position.y / shardMapRadius);

            foreach (var shard in shards) shard.Units.Remove(unit);
            shards[shardX, shardY].Units.Add(unit);
        }
    }

    public void AddEntityToDataBase(object entity)
    {
        if (entity.GetType() == typeof(Unit)) Units.Add((Unit)entity);
        else if (entity.GetType() == typeof(Faction)) Factions.Add((Faction)entity);
        else
        {
            Debug.LogError("Invalid type!");
            return;
        }

        if (entity != null && entity is ISynchronizableObject) synchronizableObjects.Add(entity as ISynchronizableObject);
    }
    public int GetIndexOfStoredEntity(object entity)
    {
        if (entity.GetType() == typeof(Unit)) return Units.IndexOf((Unit)entity);
        if (entity.GetType() == typeof(Faction)) return Factions.IndexOf((Faction)entity);

        Debug.LogError("Invalid type!");
        return -1;
    }

    public object GetEntity(int storedEntityID, Type typeOfEntity)
    {
        if (typeOfEntity == typeof(Unit)) return Units[storedEntityID];
        else if (typeOfEntity == typeof(Faction)) return Factions[storedEntityID];
        else
        {
            Debug.LogError("Invalid type!");
            return null;
        }
    }
    public Unit GetNearestUnitInShard(Vector3 referencePosition, float minDistance = 9999)
    {
        Debug.Log((byte)referencePosition.x / shardRadiusInUnits + " " + (byte)referencePosition.y / shardRadiusInUnits);
        ShardOfDataBase shardOfAPosition = shards[(byte)referencePosition.x / shardRadiusInUnits, (byte)referencePosition.y / shardRadiusInUnits];
        float currentDistance;
        Unit nearestUnitInShard = null;

        foreach (var unitInShard in shardOfAPosition.Units)
        {
            currentDistance = Vector3.Distance(referencePosition, unitInShard.Position);
            if (currentDistance < minDistance)
            {
                currentDistance = minDistance;
                nearestUnitInShard = unitInShard;
            }
        }

        return nearestUnitInShard;
    }
    public Type GetTypeOutOfID(IDataBase.EntityTypeID entityTypeID)
    {
        if (entityTypeID == IDataBase.EntityTypeID.Unit) return typeof(Unit);
        if (entityTypeID == IDataBase.EntityTypeID.Faction) return typeof(Faction);
        Debug.LogError("Invalid type!");
        return null;
    }
    public IDataBase.EntityTypeID GetIDofType(Type EntityType)
    {
        if (EntityType == typeof(Unit)) return IDataBase.EntityTypeID.Unit;
        if (EntityType == typeof(Faction)) return IDataBase.EntityTypeID.Faction;
        Debug.LogError("Invalid type!");
        return 0;
    }
    public List<ISynchronizableObject> GetSynchronizableObjects() => synchronizableObjects;

    public void Dispose()
    {
        Units = null;
        Factions = null;

        foreach (var shard in shards) shard.Dispose();
        shards = null;

        isKilled = true;
    }
}
