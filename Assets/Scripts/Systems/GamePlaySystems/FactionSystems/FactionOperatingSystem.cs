using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

using Factions;
using Units;
using Zenject;

namespace Systems
{
    public class FactionOperatingSystem : GameSystem, ISystem
    {
        private UnitSpawningSystem unitModifying;

        public FactionOperatingSystem(GameSystemsManager systemsManager) : base(systemsManager) => Resolve(systemsManager);
        protected override void Resolve(GameSystemsManager systemsManager)
        {
            base.Resolve(systemsManager);
            unitModifying = (UnitSpawningSystem)systemsManager.GetSystem(typeof(UnitSpawningSystem));

            SpawnNewAbstractFaction(systemsManager.GetGame());
        }
        public Faction SpawnNewFaction(short townhallUnitTypeID, Vector3 townhallPosition, Game gameToCreateIn, Player controllingPlayer = null)
        {
            try
            {
                Faction newFaction = new Faction(true) { ControllingPlayerID = controllingPlayer.OwnerClientId, AssociatedGame = gameToCreateIn };
                gameToCreateIn.DataBase.AddEntityToDataBase(newFaction);
                newFaction.FactionID = (short)gameToCreateIn.DataBase.GetIndexOfStoredEntity(newFaction);

                Unit townHall = unitModifying.SpawnNewUnit(townhallUnitTypeID, townhallPosition, newFaction.FactionID);

                if (controllingPlayer != null) controllingPlayer.PlayerGameplayObjectID = newFaction.FactionID;

                return newFaction;
            }
            catch(Exception error)
            {
                Debug.Log("Faction spawning failed! " + error);
                return null;
            }
        }
        public Faction SpawnNewAbstractFaction(Game gameToCreateIn)
        {
            Faction newFaction = new Faction(true) { ControllingPlayerID = 0, AssociatedGame = gameToCreateIn };
            gameToCreateIn.DataBase.AddEntityToDataBase(newFaction);

            newFaction.FactionID = (short)gameToCreateIn.DataBase.GetIndexOfStoredEntity(newFaction);

            return newFaction;
        }
    }
}
