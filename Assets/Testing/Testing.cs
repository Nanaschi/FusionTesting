using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fusion;
using Fusion.Sockets;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Testing : MonoBehaviour
{
    [SerializeField] private NetworkRunner _networkRunnerPrefab;
    private NetworkRunner _networkRunnerInstance;

    private void Start()
    {
        CreateNetworkRunner();

        var clientTask = Intitialize(_networkRunnerInstance, GameMode.AutoHostOrClient,
            NetAddress.Any(), SceneManager.GetActiveScene().buildIndex, "Test Scene", null);
    }

    private void CreateNetworkRunner()
    {
        _networkRunnerInstance = Instantiate(_networkRunnerPrefab);
        _networkRunnerInstance.name = nameof(NetworkRunner);
    }

    protected virtual Task Intitialize(NetworkRunner networkRunner, GameMode gameMode,
        NetAddress address, SceneRef scene, string sessionName, Action<NetworkRunner> initialized)
    {
        var networkSceneManagerDefault =
            networkRunner.gameObject.AddComponent<NetworkSceneManagerDefault>();


        networkRunner.ProvideInput = true;

        return networkRunner.StartGame(new StartGameArgs
        {
            GameMode = gameMode,
            Address = address,
            Scene = scene,
            SessionName = sessionName,
            Initialized = initialized,
            SceneManager = networkSceneManagerDefault
        });
    }
}
