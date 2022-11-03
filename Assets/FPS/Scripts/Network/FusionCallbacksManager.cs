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

public class FusionCallbacksManager : MonoBehaviour, INetworkRunnerCallbacks
{
    [SerializeField] private NetworkObject _networkPlayer;

    [SerializeField] [Range(0, 100)] private int _rangeToSpawn;
    private Dictionary<PlayerRef, NetworkObject> _spawnedCharacters = new();

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        if (runner.IsServer)
        {
            NetworkObject newNetworkObject = runner.Spawn(_networkPlayer,
                new Vector3(Random.Range(-_rangeToSpawn, _rangeToSpawn), 1.1f, Random.Range(-_rangeToSpawn, _rangeToSpawn)),
                Quaternion.identity, player);

            _spawnedCharacters.Add(player, newNetworkObject);
        }
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        if (_spawnedCharacters.TryGetValue(player, out NetworkObject networkObject))
        {
            runner.Despawn(networkObject);
            _spawnedCharacters.Remove(player);
        }
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        var data = new NetworkInputData();

        data.RotationInput = new Vector2( Input.GetAxis("Mouse Y"),Input.GetAxis("Mouse X")); 

        if (Input.GetKey(KeyCode.W))
            data.MovementInput += Vector3.forward;

        if (Input.GetKey(KeyCode.S))
            data.MovementInput += Vector3.back;

        if (Input.GetKey(KeyCode.A))
            data.MovementInput += Vector3.left;

        if (Input.GetKey(KeyCode.D))
            data.MovementInput += Vector3.right;

        if (Input.GetKeyDown(KeyCode.Space)) data.IsJumpPressed = true;

            input.Set(data);
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