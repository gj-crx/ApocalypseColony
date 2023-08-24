using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Zenject;
using Systems;
using Systems.Pathfinding;

namespace Systems.Installers
{
    public class SystemPropertiesInstaller : MonoInstaller
    {

        public override void InstallBindings()
        {
            //Interfaces
            Container.Bind<IPathfindingMap>().To<PathfindingMap>().AsTransient();
            Container.Bind<IPathfinding>().To<NormalPathfinding>().AsTransient();
            Container.Bind<IDataBase>().To<GameDataBase>().AsTransient();
            /*

            //Data storages
            Container.Bind<GameDataBase>().AsTransient();

            //Systems
            Container.Bind<MovementSystem>().AsTransient();
            Container.Bind<HealthSystem>().AsTransient();
            Container.Bind<BuildingOperatingSystem>().AsTransient();
            Container.Bind<UnitModifyingSystem>().AsTransient();
            Container.Bind<UnitAISystem>().AsTransient();
            Container.Bind<FactionOperatingSystem>().AsTransient();
            Container.Bind<PlayerOperatingSystem>().AsTransient();
            Container.Bind<PathfindingMap>().AsTransient();
            Container.Bind<NormalPathfinding>().AsTransient();
            Container.Bind<TrainingSystem>().AsTransient();

            */
        }
    }
}
