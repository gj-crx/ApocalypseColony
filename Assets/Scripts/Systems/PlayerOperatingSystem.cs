using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;
using Zenject;

using Units;

namespace Systems
{
    public class PlayerOperatingSystem : GameSystem, ISystem
    {
      
        private FactionOperatorSystem factionOperator;
        [Inject]
        private string naruto;

        public PlayerOperatingSystem()
        {
            Debug.Log(naruto);
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
                new Vector3(Random.Range(-50, 50), 1, Random.Range(-50, 50)), game, playerToAdd).FactionID;
        }
    }
}
