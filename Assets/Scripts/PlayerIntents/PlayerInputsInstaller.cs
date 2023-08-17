using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Zenject;

public class PlayerInputsInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<PlayerInput>().AsSingle();
    }
}
