using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;
using Zenject;

using Units;
using System;

namespace Systems
{
    public class PlayerOperatingSystem : GameSystem, ISystem
    {
      
        private FactionOperatingSystem factionOperator;

        [Inject]
        public PlayerOperatingSystem(FactionOperatingSystem factionOperator, GameDataBase dataBase)
        {
            this.dataBase = dataBase;
            this.factionOperator = factionOperator;
        }
        public PlayerOperatingSystem() 
        {

        }

        public void AddPlayer(Player playerToAdd, Game game)
        {
            //adding network data to player object
            playerToAdd.ConnectedGame = game;
            playerToAdd.PlayerID = playerToAdd.OwnerClientId;

            //updating rpc params
            game.ConnectedClientsParams = new ClientRpcParams();
            game.ConnectedClientsParams.Send = new ClientRpcSendParams
            {
                TargetClientIds = new ulong[] { playerToAdd.PlayerID },
            };

            Debug.Log(factionOperator);
            playerToAdd.PlayerGameplayObjectID = factionOperator.SpawnNewFaction(UnitTypesStorage.GetUnitByClass(Unit.UnitClassification.Townhall).UnitTypeID,
                new Vector3(UnityEngine.Random.Range(-50, 50), 1, UnityEngine.Random.Range(-50, 50)), game, playerToAdd).FactionID;
        }
    }
}
