using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System.Threading.Tasks;

using Zenject;
using Networking;
using Systems;

    /// <summary>
    /// Entry point to run all games on this server
    /// </summary>
    public class LocalServerStartingSystem : NetworkBehaviour
    {
        [Inject] private DatabaseSynchronizator dataBaseSynchronizatorSingle;
        [Inject] private GameFactory gameFactorySingle;


        private List<Game> GamesOfThisServer;

    [Inject]
    private void Install()
    {
        GamesOfThisServer = new List<Game>();
    }

        public void HostNewGameFromLocalServer()
        {
            NetworkManager.StartHost();
            StartCoroutine(StartNewGameDelayedAction());
        }

        public IEnumerator StartNewGameDelayedAction()
        {
            if (gameFactorySingle == null) gameFactorySingle = new GameFactory();

            GetLocalPlayer();
            Game newGame = gameFactorySingle.Create();
            GameSystemsManager.InstallGameSystems(newGame);

            newGame.transform.SetParent(HierarchyCategoriesStorage.GamesCategory);


            yield return new WaitForSeconds(1.0f);

            Debug.Log(Player.LocalPlayerObject);
            ((PlayerOperatingSystem)newGame.GetSystem(typeof(PlayerOperatingSystem))).AddPlayer(Player.LocalPlayerObject, newGame);
            GamesOfThisServer.Add(newGame);

            dataBaseSynchronizatorSingle.GameSynchronizationProcess(newGame.DataBase);
        }
        
        private void OnApplicationQuit()
        {
            foreach (Game game in GamesOfThisServer) GameObject.Destroy(game.gameObject);
            GamesOfThisServer = null;
            System.GC.Collect();
            Debug.Log("server terminated");
        }
        private void GetLocalPlayer()
        {
            Player.LocalPlayerObject = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }
    }
