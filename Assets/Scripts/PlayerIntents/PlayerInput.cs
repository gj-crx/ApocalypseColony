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

    [Inject] private IDataBase dataBase;
    [Inject] private TrainingSystem training;


    [Inject]
    private void InstallPlayerInputs()
    {

    }

    [ServerRpc]
    public void OrderMoveUnitServerRpc(int unitToOrderID, Vector3 targetPosition)
    {
        Unit controlledUnit = (Unit)dataBase.GetEntity(unitToOrderID, typeof(Unit));

        Debug.Log("order recieved " + controlledUnit.UnitName + " target position " + targetPosition);

        //Gets the reference to the ordering system, instantiates new order and enqueues it to the system
       ((UnitAISystem)controlledUnit.CurrentGame.GetSystem(typeof(UnitAISystem))).EnqueueNewOrder(controlledUnit, 
           new Unit.Order { Type = Unit.OrderType.MoveToPosition, TargetPosition = targetPosition });
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
    
