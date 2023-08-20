using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

using UnityEngine;
using Zenject;
using Unity.Netcode;

[System.Serializable]
public class Game : MonoBehaviour
{
    public List<ISystem> GameSystems = new List<ISystem>();

    public IDataBase DataBase;

    public ClientRpcParams ConnectedClientsParams;

    public bool IsActive = true;

    public void Awake()
    {
        ConnectedClientsParams = new ClientRpcParams();
    }

    [Inject]
    public void InstallGame()
    {

    }
    private void Update()
    {
        Debug.Log(DataBase.GetSynchronizableObjects().Count.ToString() + " _ " + (DataBase.GetSynchronizableObjects() != null).ToString());
    }

    private void Dispose()
    {
        foreach (ISystem system in GameSystems) system.IsActive = false;

        DataBase.Dispose();
        DataBase = null;

        Debug.Log("Disposed");
    }
    private void OnDestroy()
    {
        Dispose();
    }
}
