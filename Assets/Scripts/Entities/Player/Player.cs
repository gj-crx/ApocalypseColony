using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.Netcode;
using Zenject;

public class Player : NetworkBehaviour
{
    [Header("-- BOTH SERVER AND CLIENT VARIABLES")]
    public ulong PlayerID = 0;

    [Header("-- SERVER SIDE ONLY VARIABLES")]
    public Game ConnectedGame = null;
    /// <summary>
    /// Can represent separate player object like his faction or player unit ID or something else
    /// </summary>
    public int PlayerGameplayObjectID = -1;


    [Header("-- CLIENT SIDE ONLY VARIABLES")]
    public static Player LocalPlayerObject = null;


    //Local host assignment
    private void Awake()
    {
        if (IsLocalPlayer || IsHost) LocalPlayerObject = this;
        Debug.Log("localplayer " + LocalPlayerObject);
    }


   
}
