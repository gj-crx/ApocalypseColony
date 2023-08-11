using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Zenject;
using Units.ClientSide;
using Factions.ClientSide;


namespace ClientSideLogic
{
    public class ClientDataBase : IDataBase
    {
        public static IDataBase Singleton;

        public  List<UnitRepresentation> UnitRepresentations = new List<UnitRepresentation>();
        public  List<FactionRepresentation> FactionRepresentations = new List<FactionRepresentation>();

        [Inject]
        private ClientDataBase()
        {
            Singleton = this;
        }
        public bool IsUnitRepresentedInClient(int unitID)
        {
            foreach (var unit in UnitRepresentations) if (unit.UnitID == unitID) return true;
            return false;
        }
        public bool IsFactionRepresentedInClient(short factionID)
        {
            foreach (var faction in FactionRepresentations) if (faction.FactionID == factionID) return true;
            return false;
        }

        public void AddEntityToDataBase(object entity)
        {
            if (entity.GetType() == typeof(UnitRepresentation)) UnitRepresentations.Add((UnitRepresentation)entity);
            else if (entity.GetType() == typeof(FactionRepresentation)) FactionRepresentations.Add((FactionRepresentation)entity);
            else
            {
                Debug.LogError("Invalid type!");
                return;
            }
        }
        public int GetIndexOfStoredEntity(object entity)
        {
            if (entity.GetType() == typeof(UnitRepresentation)) return UnitRepresentations.IndexOf((UnitRepresentation)entity);
            else if (entity.GetType() == typeof(FactionRepresentation)) return FactionRepresentations.IndexOf((FactionRepresentation)entity);

            Debug.LogError("Invalid type!");
            return -1;
        }


        public void Dispose()
        {
            UnitRepresentations.Clear();
            UnitRepresentations = null;

            FactionRepresentations.Clear();
            FactionRepresentations = null;
        }

        public object GetEntity(int storedEntityID, Type typeOfEntity)
        {
            if (typeOfEntity == typeof(UnitRepresentation)) return UnitRepresentations[storedEntityID];
            else if (typeOfEntity == typeof(FactionRepresentation)) return FactionRepresentations[storedEntityID];
            else
            {
                Debug.LogError("Invalid type!");
                return null;
            }
        }

        public List<ISynchronizableObject> GetSynchronizableObjects()
        {
            throw new NotImplementedException();
        }

        public Type GetTypeOutOfID(IDataBase.EntityTypeID entityTypeID)
        {
            if (entityTypeID == IDataBase.EntityTypeID.Unit) return typeof(UnitRepresentation);
            if (entityTypeID == IDataBase.EntityTypeID.Faction) return typeof(FactionRepresentation);
            Debug.LogError("Invalid type!");
            return null;
        }

        public IDataBase.EntityTypeID GetIDofType(Type EntityType)
        {
            throw new NotImplementedException();
        }
    }
}
