using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Zenject;
using Networking;

namespace Systems.Installers
{
    public class NetworkingInstaller : MonoInstaller
    {
        [SerializeField] private LocalNetworkManager localNetworkManager;
        [SerializeField] private DatabaseSynchronizator databaseSynchronizator;
        public override void InstallBindings()
        {
            Container.Bind<LocalNetworkManager>().FromInstance(localNetworkManager).AsSingle().NonLazy();
            Container.Bind<DatabaseSynchronizator>().FromInstance(databaseSynchronizator).AsSingle().NonLazy();
        }
    }
}
