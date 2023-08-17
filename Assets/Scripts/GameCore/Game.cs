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

    public object GetSystem(Type systemType)
    {
        foreach (var system in GameSystems)
        {
            if (system.GetType() == systemType) return system;
        }
        Debug.LogError("System not found! " + systemType.Name);
        return null;
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
