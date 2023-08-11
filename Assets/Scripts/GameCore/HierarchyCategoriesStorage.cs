using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class HierarchyCategoriesStorage : MonoBehaviour
{
    public static Transform GamesCategory;

    [SerializeField] private Transform gamesCategory;

    [Inject]
    public void Install()
    {
        GamesCategory = gamesCategory;
    }
}
