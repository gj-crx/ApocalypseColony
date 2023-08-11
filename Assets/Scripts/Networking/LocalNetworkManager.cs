using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.Netcode;
using Zenject;


public class LocalNetworkManager : NetworkBehaviour
{
    public ApplicationQuit OnQuit;

    [Inject]
    private void OnInstasll()
    {
    }

    public delegate void ApplicationQuit();
    private void OnApplicationQuit()
    {
        OnQuit();
    }
}
