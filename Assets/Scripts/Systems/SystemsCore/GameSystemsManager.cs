using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

using Zenject;
using Systems.Pathfinding;

namespace Systems
{
    public class GameSystemsManager : MonoBehaviour
    {
        [SerializeField]
        private List<string> systemsToDeactivateInNewGame = new List<string>();
        [SerializeField]
        private List<string> systemsToActivateDebugLogging = new List<string>();

        private static List<string> systemsToDeactivate = new List<string>();
        private Game associatedGame;

        [Inject]
        public void InstallGameSystems()
        {
            systemsToDeactivate = systemsToDeactivateInNewGame;

            transform.SetParent(HierarchyCategoriesStorage.GamesCategory);
            Game gameToInstall = GetComponent<Game>();
            associatedGame = gameToInstall;
            //database
            gameToInstall.DataBase = new GameDataBase();

            //Single purpose systems
            AddNewSystem(new MovementSystem(this));
            AddNewSystem(new PathfindingMap());
            AddNewSystem(new NormalPathfinding(this));
            AddNewSystem(new HealthSystem(this));
            AddNewSystem(new BuildingOperatingSystem(this));
            AddNewSystem(new UnitSpawningSystem(this));
            AddNewSystem(new UnitAISystem(this));
            AddNewSystem(new FactionOperatingSystem(this));
            AddNewSystem(new PlayerOperatingSystem(this));
            AddNewSystem(new UnitTrainingSystem(this));
            AddNewSystem(new UnitOrderProcessingSystem(this));
            AddNewSystem(new UnitFightingSystem(this));
            AddNewSystem(new UnitCollisionSystem(this));

            ActivateDebugLogging();
            foreach (var system in  gameToInstall.GameSystems) Task.Run(() => system.SystemIterationCycle());
        }
        public object GetSystem(Type systemType)
        {
            foreach (var system in associatedGame.GameSystems)
            {
                if (system.GetType() == systemType) return system;
            }
            Debug.LogError("System not found! " + systemType.Name);
            return null;
        }
        public GameDataBase GetDataBase() => (GameDataBase)associatedGame.DataBase;
        public Game GetGame() => associatedGame;

        private void ActivateDebugLogging()
        {
            foreach (string systemName in systemsToActivateDebugLogging)
            {
                foreach (var system in associatedGame.GameSystems)
                {
                    if (system.GetType().Name == systemName)
                    {
                        (system as GameSystem).DebugLoggingActive = true;
                        break;
                    }
                }
            }
        }
        private void AddNewSystem(object newSystemObject)
        {
            if (systemsToDeactivate.Contains(newSystemObject.GetType().Name) == false)
            {
                associatedGame.GameSystems.Add(newSystemObject as ISystem);
            }
        }

    }
}
