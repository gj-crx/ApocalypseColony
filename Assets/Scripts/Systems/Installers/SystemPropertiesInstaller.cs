using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Zenject;
using Pathfinding;
using Systems;

namespace Systems.Installers
{
    public class SystemPropertiesInstaller : MonoInstaller
    {

        public override void InstallBindings()
        {
            Container.Bind<IPathfindingMap>().To<PathfindingMap>().AsTransient();
            Container.Bind<IPathfinding>().To<NormalPathfinding>().AsTransient();
            Container.Bind<IDataBase>().To<GameDataBase>().AsTransient();

            Container.Bind<GameDataBase>().AsTransient();

            Container.Bind<MovementSystem>().AsTransient();
            Container.Bind<HealthSystem>().AsTransient();
            Container.Bind<BuildingOperationSystem>().AsTransient();

            Container.Bind<UnitModifyingSystem>().AsTransient();
            Container.Bind<UnitAISystem>().AsTransient();
            Container.Bind<PlayerOperatingSystem>().AsTransient();
            Container.Bind<FactionOperatorSystem>().AsTransient();
            Container.Bind<PathfindingMap>().AsTransient();
            Container.Bind<NormalPathfinding>().AsTransient();
        }
    }
}
