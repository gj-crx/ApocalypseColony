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
        }

        public Faction SpawnNewFaction(short townhallUnitTypeID, Vector3 townhallPosition, Game gameToCreateIn, Player controllingPlayer = null)
        {
            Unit townHall = unitModifying.SpawnNewUnit(townhallUnitTypeID, townhallPosition);

            Faction newFaction = new Faction(true) { ControllingPlayerID = controllingPlayer.OwnerClientId, AssociatedGame = gameToCreateIn };
            newFaction.Buildings.Add(townHall);

            gameToCreateIn.DataBase.AddEntityToDataBase(newFaction);

            newFaction.FactionID = (short)gameToCreateIn.DataBase.GetIndexOfStoredEntity(newFaction);

            return newFaction;
        }
    }
}
