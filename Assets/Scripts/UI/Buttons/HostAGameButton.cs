using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class HostAGameButton : MonoBehaviour
{
    [SerializeField] private LocalServerStartingSystem serverStartingSystem;
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnButtonClicked);
    }

    private void OnButtonClicked()
    {
        serverStartingSystem.HostNewGameFromLocalServer();
    }
}
