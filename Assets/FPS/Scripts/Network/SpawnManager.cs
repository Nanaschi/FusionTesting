using System;
using System.Collections.Generic;
using System.Reflection;
using Fusion;
using Fusion.Sockets;
using Network;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

public class SpawnManager : MonoBehaviour, INetworkRunnerCallbacks
{
    [SerializeField] private NetworkObject _networkPlayer;
    private CharacterInputHandler _characterInputHandler;

    private void Awake()
    {
        
    }

    [SerializeField] [Range(0, 100)] private int _rangeToSpawn;
    private NetworkObject _newPlayer;

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        if (runner.IsServer)
        {
            print(MethodBase.GetCurrentMethod() + "and we are the server");
            _newPlayer = runner.Spawn(_networkPlayer,
                new Vector3(Random.Range(-_rangeToSpawn, _rangeToSpawn), 1, Random.Range(-_rangeToSpawn, _rangeToSpawn)),
                Quaternion.identity, player);
        }
        _characterInputHandler = _newPlayer.GetComponent<CharacterInputHandler>();

    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        
        var data = new NetworkInputData();

        if (Input.GetKey(KeyCode.W))
            data.MovementInput += Vector3.forward;

        if (Input.GetKey(KeyCode.S))
            data.MovementInput += Vector3.back;

        if (Input.GetKey(KeyCode.A))
            data.MovementInput += Vector3.left;

        if (Input.GetKey(KeyCode.D))
            data.MovementInput += Vector3.right;
        
        input.Set(data);
        /*var networkInputData = new NetworkInputData();

        if (Input.GetKey(KeyCode.W))
        {
            print("WWWWWWWW");
        }*/



        /*if (_characterInputHandler == null && _newPlayer.HasStateAuthority)
        {
            
            print(_newPlayer.gameObject.name);
        }

        if (_characterInputHandler != null)
        {
            input.Set(_characterInputHandler.GetNetworkInput());
        }*/
    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
    }

    public void OnConnectedToServer(NetworkRunner runner)
    {
    }

    public void OnDisconnectedFromServer(NetworkRunner runner)
    {
    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {
    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {
    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {
    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data)
    {
    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {
    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {
    }
}