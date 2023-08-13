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
            AddNewSystem(new MovementSystem(), gameToInstall);
            AddNewSystem(new HealthSystem(), gameToInstall);
            AddNewSystem(new BuildingOperationSystem(), gameToInstall);
            AddNewSystem(new UnitModifyingSystem(), gameToInstall);
            AddNewSystem(new UnitAISystem(), gameToInstall);
            AddNewSystem(new FactionOperatorSystem(), gameToInstall);
            AddNewSystem(new PlayerOperatingSystem(), gameToInstall);
            AddNewSystem(new TrainingSystem(), gameToInstall);

            foreach (var system in  gameToInstall.GameSystems) Task.Run(() => system.SystemIterationCycle());
        }

        private static void AddNewSystem(object newSystemObject, Game gameToAddInto)
        {
            if (systemsToDeactivate.Contains(newSystemObject.GetType().Name) == false)
            {
                gameToAddInto.GameSystems.Add(newSystemObject as ISystem);
            }
        }
    }
}
