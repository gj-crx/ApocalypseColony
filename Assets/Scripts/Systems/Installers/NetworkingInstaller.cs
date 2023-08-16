using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Zenject;
using Networking;

namespace Systems.Installers
{
    public class NetworkingInstaller : MonoInstaller
    {
        [SerializeField] private DatabaseSynchronizator databaseSynchronizator;
        [SerializeField] private LocalServerStartingSystem startingSystem;
        public override void InstallBindings()
        {
            Container.Bind<DatabaseSynchronizator>().FromInstance(databaseSynchronizator).AsSingle().NonLazy();
            Container.Bind<LocalServerStartingSystem>().FromInstance(startingSystem).AsSingle().NonLazy();

            Container.BindFactory<GameSystemsManager, SystemsManagerFactory>().FromComponentInNewPrefab(PrefabManager.GetGamePrefab());

        }
    }
}
