using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UIElements;

public class TestingUI : MonoBehaviour
{
    [SerializeField] private LocalServerStartingSystem startingSystem;

    private void OnEnable()
    {
        InstallTestingButtons(GetComponent<UIDocument>().rootVisualElement);
    }

    private void InstallTestingButtons(VisualElement testingLayout)
    {
        Button hostButton = testingLayout.Q<Button>("HostButton");
        Button connectButton = testingLayout.Q<Button>("ConnectButton");

        hostButton.clicked += () => startingSystem.HostNewGameFromLocalServer();
        connectButton.clicked += () => startingSystem.LaunchClientAndConnect();
    }
}
