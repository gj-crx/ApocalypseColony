using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Systems
{
    public class GameSystemsManager : MonoBehaviour
    {
        [SerializeField]
        private List<string> systemsToDeactivateInNewGame = new List<string>();

        private static List<string> systemsToDeactivate = new List<string>();

        private void Awake() => systemsToDeactivate = systemsToDeactivateInNewGame;

        public static void InstallGameSystems(Game gameToInstall)
        {
            //database
            gameToInstall.DataBase = new GameDataBase();

            //Single purpose systems
            AddNewSystem(new MovementSystem().GetType(), gameToInstall);
            AddNewSystem(new HealthSystem().GetType(), gameToInstall);
            AddNewSystem(new BuildingOperationSystem().GetType(), gameToInstall);
            AddNewSystem(new UnitModifyingSystem().GetType(), gameToInstall);
            AddNewSystem(new UnitAISystem().GetType(), gameToInstall);
            AddNewSystem(new FactionOperatorSystem().GetType(), gameToInstall);
            AddNewSystem(new PlayerOperatingSystem().GetType(), gameToInstall);

            foreach (var system in  gameToInstall.GameSystems) Task.Run(() => system.SystemIterationCycle());
        }

        private static void AddNewSystem(Type systemType, Game gameToAddInto)
        {
            if (systemsToDeactivate.Contains(systemType.Name) == false) gameToAddInto.GameSystems.Add((ISystem)systemType);
        }
    }
}
