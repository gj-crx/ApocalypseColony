using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Zenject;

namespace ClientSideLogic.Installers
{
    public class ClientSideObjectsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ClientDataBase>().FromNew().AsSingle().NonLazy();
        }
    }
}
