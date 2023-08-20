using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System.Threading.Tasks;

using Units;
using Units.ClientSide;
using Factions;
using Factions.ClientSide;
using ClientSideLogic;

namespace Networking
{
    /// <summary>
    /// Transfers in
    /// </summary>
    public class DatabaseSynchronizator : NetworkBehaviour
    {
        [Header("Synchronizes each game database to it's clients")]

        public int UnitSyncInterval = 75;


        public async void GameSynchronizationProcess(IDataBase dataBaseToSynchronize, ClientRpcParams clientRpcParamsToSync)
        {
            while (dataBaseToSynchronize.IsKilled == false)
            {
                Debug.Log("Sync process going on");
                try
                {
                    ISynchronizableObject[] synchronizableObjectsTempArray = dataBaseToSynchronize.GetSynchronizableObjects().ToArray();
                    Debug.Log("Object to sync amount " + synchronizableObjectsTempArray.Length);
                    foreach (var objectToSync in synchronizableObjectsTempArray)
                    {
                        if (objectToSync != null)
                        {
                            SyncObjectClientRpc(dataBaseToSynchronize.GetIndexOfStoredEntity(objectToSync), dataBaseToSynchronize.GetIDofType(objectToSync.GetType()),
                                objectToSync.GetDataToTransfer(), clientRpcParamsToSync);
                        }
                    }
                }
                catch
                {
                    Debug.Log("Synchronization error, might be just a game disposal");
                    return;
                }
                await Task.Delay(UnitSyncInterval);
            }
        }

        [ClientRpc]
        private void SyncObjectClientRpc(int objectToSyncID, IDataBase.EntityTypeID objectToSyncTypeID, IEntityData dataToSync, ClientRpcParams clientRpcParams)
        {
            (ClientDataBase.Singleton.GetEntity(objectToSyncID, ClientDataBase.Singleton.GetTypeOutOfID(objectToSyncTypeID)) as ISynchronizableObject).ApplyTransferedData(dataToSync);
            Debug.LogError("actually got it lol " + objectToSyncTypeID);
        }
        /*
        [ClientRpc]
        public void ObjectDeathClientRpc(int deadObjectReferenceID, ClientRpcParams clientRpcParams)
        {
            Debug.Log("Object death synced to client " + deadObjectReferenceID);
            if (ClientDataBase.UnitRepresentations[deadObjectReferenceID] != null)
            {
                ClientDataBase.UnitRepresentations[deadObjectReferenceID].DeathClientSide();
                ClientDataBase.UnitRepresentations.Remove(ClientDataBase.UnitRepresentations[deadObjectReferenceID]);
            }
        }
        */
    }
}