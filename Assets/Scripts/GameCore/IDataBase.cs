using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataBase 
{

    void AddEntityToDataBase(object entity);
    object GetEntity(int storedEntityID, Type typeOfEntity);
    int GetIndexOfStoredEntity(object entity);
    Type GetTypeOutOfID(IDataBase.EntityTypeID entityTypeID);
    IDataBase.EntityTypeID GetIDofType(Type EntityType);
    List<ISynchronizableObject> GetSynchronizableObjects();
    void Dispose();

    public enum EntityTypeID : byte
    {
        Unit = 0,
        Faction = 1
    }
}
