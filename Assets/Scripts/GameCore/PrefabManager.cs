using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PrefabManager : MonoBehaviour
{
    public static GameObject GamePrefab;
    public static GameObject PlayerNetObjectPrefab;
    public static List<GameObject> UnitRepresentationPrefabs;
    public static GameObject FactionPrefab;

    [Header("Technical prefabs")]
    [SerializeField] private GameObject gamePrefab;
    [SerializeField] private GameObject playerNetObjectPrefab;
    [SerializeField] private GameObject factionPrefab;

    [Header("Gameplay prefabs")]
    [SerializeField] private List<GameObject> unitRepresentationPrefabs;

    [Inject]
    public void Install()
    {
        GamePrefab = gamePrefab;
        PlayerNetObjectPrefab = playerNetObjectPrefab;
        UnitRepresentationPrefabs = unitRepresentationPrefabs;
        FactionPrefab = factionPrefab;
    }
    //Pre-install methods to get prefabs for binding
    public static Game GetGamePrefab() => GameObject.Find("StaticDataStorage").GetComponent<PrefabManager>().gamePrefab.GetComponent<Game>();
    public static GameObject GetPlayerPrefab() => GameObject.Find("StaticDataStorage").GetComponent<PrefabManager>().playerNetObjectPrefab;
}
