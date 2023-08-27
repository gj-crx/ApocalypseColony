using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using Zenject;
using Systems.Installers;

public class TestSimpleTool : MonoBehaviour
{
    private PlayerInput playerInput;

    private void TryToStart()
    {
        if (playerInput != null) return;
        try
        {
            playerInput = Player.LocalPlayerObject.GetComponent<IPlayerControllerInstaller>() as PlayerInput;
        }
        catch { }
    }

    void Update()
    {
         TryToStart();
         if (Input.GetKey(KeyCode.E) && Input.GetMouseButtonDown(0)) ClickSpawnUnit();
         if (Input.GetKey(KeyCode.R) && Input.GetMouseButtonDown(0)) ClickOrderUnit();
         if (Input.GetKey(KeyCode.T) && Input.GetMouseButtonDown(0)) ClickTrainUnit();
    }
    private void ClickSpawnUnit()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit) == false) return;

        playerInput.TestForceSpawnUnitServerRpc();
    }
    private void ClickOrderUnit()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit) == false) return;

        playerInput.OrderMoveUnitServerRpc(0, hit.point);
    }
    private void ClickTrainUnit()
    {
        playerInput.OrderTrainingOfANewUnitServerRpc(0, (short)Player.LocalPlayerObject.PlayerGameplayObjectID);
    }
    private void TestServerPing()
    {
        playerInput.TestPingServerRpc((byte)Random.Range(byte.MinValue, byte.MaxValue));
    }
}
