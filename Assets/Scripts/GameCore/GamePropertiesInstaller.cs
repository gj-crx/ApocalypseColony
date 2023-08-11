using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Core.Installers
{
    public class GamePropertiesInstaller : MonoInstaller
    {
        [SerializeField] private PrefabManager prefabManagerObject;
        [SerializeField] private HierarchyCategoriesStorage hierarchyCategoriesStorage;

        public override void InstallBindings()
        {
            Application.targetFrameRate = 45;

            Container.Bind<PrefabManager>().FromInstance(prefabManagerObject).NonLazy();
            Container.Bind<HierarchyCategoriesStorage>().FromInstance(hierarchyCategoriesStorage).NonLazy();
        }
    }
}