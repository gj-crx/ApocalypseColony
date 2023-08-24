using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Zenject;
using Unity.Netcode;

using Units;
using Factions;
using Systems;

public class PlayerInput : NetworkBehaviour
{

    //SERVER SIDE ONLY VARIABLES
     private IDataBase dataBase;
     private TrainingSystem training;
     private UnitAISystem unitAI;
     private UnitModifyingSystem unitModifying;

    [Inject]
    public void Resolve(GameSystemsManager systemsManager)
    {

    }

    [ServerRpc]
    public void TestForceSpawnUnitServerRpc()
    {
        unitModifying.SpawnNewUnit(0, Vector3.zero);
    }

    [ServerRpc]
    public void OrderMoveUnitServerRpc(int unitToOrderID, Vector3 targetPosition)
    {
        Unit controlledUnit = (Unit)dataBase.GetEntity(unitToOrderID, typeof(Unit));

        Debug.Log("order recieved " + controlledUnit.UnitName + " target position " + targetPosition);

        //Gets the reference to the ordering system, instantiates new order and enqueues it to the system
         unitAI.EnqueueNewOrder(controlledUnit, new Unit.Order { Type = Unit.OrderType.MoveToPosition, TargetPosition = targetPosition });
    }
    [ServerRpc]
    public void OrderTrainingOfANewUnitServerRpc(short unitTypeIDToTrain, short playerFactionID)
    {
        Debug.Log("Training order recieved " + unitTypeIDToTrain);

        Faction playerFaction = null;
        try { playerFaction = (Faction)dataBase.GetEntity(playerFactionID, typeof(Faction)); }
        catch { Debug.LogError("Faction not found!"); }

        foreach (var building in playerFaction.Buildings)
        {
            if (building.AbleToTrainUnits && building.ComponentTraining.AllowedUnitIDsToTrain.Contains(unitTypeIDToTrain))
            {
                bool result = training.AddUnitToTrainingQueue(building, unitTypeIDToTrain, playerFaction.Resources);
                Debug.Log(result);
            }
        }
    }


    [ServerRpc]
    public void TestPingServerRpc(byte rndNumber)
    {
        Debug.Log("test ping recieved " + rndNumber);
    }
}
    
