using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Units;
using Factions;
using System;

[System.Serializable]
public class GameDataBase : IDataBase
{
    public List<Unit> Units = new List<Unit>();

    public List<Faction> Factions = new List<Faction>();

    public List<ISynchronizableObject> synchronizableObjects;

    public void AddEntityToDataBase(object entity)
    {
        if (entity.GetType() == typeof(Unit)) Units.Add((Unit)entity);
        else if (entity.GetType() == typeof(Faction)) Factions.Add((Faction)entity);
        else
        {
            Debug.LogError("Invalid type!");
            return;
        }

        if (entity is ISynchronizableObject) synchronizableObjects.Add(entity as ISynchronizableObject);
    }
    public int GetIndexOfStoredEntity(object entity)
    {
        if (entity.GetType() == typeof(Unit)) return Units.IndexOf((Unit)entity);
        if (entity.GetType() == typeof(Faction)) return Factions.IndexOf((Faction)entity);

        Debug.LogError("Invalid type!");
        return -1;
    }


    public void Dispose()
    {
        foreach (var unit in Units) unit.CurrentGame = null;

        Units = null;
        Factions = null;
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
}
