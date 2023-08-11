using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

using Factions;
using Units;
using Zenject;

namespace Systems
{
    public class FactionOperatorSystem : GameSystem, ISystem
    {
        [Inject]
        private UnitModifyingSystem unitModifying;

        public Faction SpawnNewFaction(short townhallUnitTypeID, Vector3 townhallPosition, Game gameToCreateIn, Player controllingPlayer = null)
        {
            Unit townHall = unitModifying.SpawnNewUnit(townhallUnitTypeID, townhallPosition, gameToCreateIn);

            Faction newFaction = new Faction(true) { ControllingPlayerID = controllingPlayer.OwnerClientId, AssociatedGame = gameToCreateIn };
            newFaction.Buildings.Add(townHall);

            gameToCreateIn.DataBase.AddEntityToDataBase(newFaction);

            newFaction.FactionID = (short)gameToCreateIn.DataBase.GetIndexOfStoredEntity(newFaction);

            return newFaction;
        }
    }
}
