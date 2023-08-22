using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Zenject;
using Units.ClientSide;
using Factions.ClientSide;
using Unity.Netcode;

namespace ClientSideLogic
{
    public class ClientDataBase : IDataBase
    {
        public static IDataBase Singleton;

        public  List<UnitRepresentation> UnitRepresentations = new List<UnitRepresentation>();
        public  List<FactionRepresentation> FactionRepresentations = new List<FactionRepresentation>();

        public bool IsKilled => isKilled;


        private bool isKilled = false;

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
            isKilled = true;
        }

        public object GetEntity(int storedEntityID, Type typeOfEntity)
        {
            if (typeOfEntity == typeof(UnitRepresentation))
            {
                try { return UnitRepresentations[storedEntityID]; }
                catch { return null; }
            }
            else if (typeOfEntity == typeof(FactionRepresentation))
            {
                try { return FactionRepresentations[storedEntityID]; }
                catch { return null; }
            }
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
