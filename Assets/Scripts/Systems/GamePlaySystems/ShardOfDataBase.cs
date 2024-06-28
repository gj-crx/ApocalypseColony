using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Units;
using Factions;

public class ShardOfDataBase
{
    public List<Unit> Units = new List<Unit>();
    public List<Faction> Factions = new List<Faction>();



    private GameDataBase mainDataBase;


    public ShardOfDataBase(GameDataBase gameDataBase)
    {
        this.mainDataBase = gameDataBase;
    }
    public void Dispose()
    {
        Units = null;
        Factions = null;
        mainDataBase = null;
    }
}
