using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Zenject;

public class TestingToolsInstaller : MonoInstaller
{
    public TestSimpleTool TestingTool;
    public override void InstallBindings()
    {
        Container.Bind<TestSimpleTool>().FromInstance(TestingTool).AsSingle();
    }
}
