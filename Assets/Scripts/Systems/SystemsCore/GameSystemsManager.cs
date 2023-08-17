using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

using Zenject;

namespace Systems
{
    public class GameSystemsManager : MonoBehaviour
    {
        [SerializeField]
        private List<string> systemsToDeactivateInNewGame = new List<string>();

        private static List<string> systemsToDeactivate = new List<string>();

        [Inject]
        public void InstallGameSystems(MovementSystem movement, HealthSystem health, BuildingOperatingSystem buildingOperating, UnitModifyingSystem unitModifying, UnitAISystem unitAI,
            FactionOperatingSystem factionOperating, PlayerOperatingSystem playerOperating, TrainingSystem training,
            GameDataBase dataBase)
        {
            systemsToDeactivate = systemsToDeactivateInNewGame;

            transform.SetParent(HierarchyCategoriesStorage.GamesCategory);
            Game gameToInstall = GetComponent<Game>();
            //database
            gameToInstall.DataBase = dataBase;

            //Single purpose systems
            AddNewSystem(movement, gameToInstall);
            AddNewSystem(health, gameToInstall);
            AddNewSystem(buildingOperating, gameToInstall);
            AddNewSystem(unitModifying, gameToInstall);
            AddNewSystem(unitAI, gameToInstall);
            AddNewSystem(factionOperating, gameToInstall);
            AddNewSystem(playerOperating, gameToInstall);
            AddNewSystem(training, gameToInstall);

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
