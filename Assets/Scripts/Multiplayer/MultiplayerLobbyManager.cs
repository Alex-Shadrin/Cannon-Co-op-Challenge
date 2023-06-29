using QFSW.QC;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using Unity.Services.Relay;
using UnityEngine;

#nullable enable

public class MultiplayerLobbyManager : PersistentSingleton<MultiplayerLobbyManager>
{
    private const int MAX_PLAYERS = 2;

    private UnityTransport _transport;
    private Lobby? _currentLobby = null;
    private string? _playerId = null; 

    async void Start()
    {
        _transport = FindAnyObjectByType<UnityTransport>();
        if (_transport == null)
        {
            Debug.LogError("UnityTransport is not found. Multiplayer won't work.");
            return;
        }

        _playerId = await InitializeAsync();

        if (_playerId == null)
            Debug.Log("LobbyManager could not initialize. Multiplayer won't work.");
    }

    public event Action<string>? SignedIn;
    private async Task<string?> InitializeAsync()
    {
        try
        {
            await UnityServices.InitializeAsync();

            AuthenticationService.Instance.SignedIn += () =>
            {
                Debug.Log($"Player id: {AuthenticationService.Instance.PlayerId}");
                SignedIn(AuthenticationService.Instance.PlayerId);
                //Debug.Log($"Access token: {AuthenticationService.Instance.AccessToken}");
            };
            AuthenticationService.Instance.SignInFailed += (ex) =>
            {
                Debug.Log($"Sign in failed!" + ex);
            };
            AuthenticationService.Instance.SignedOut += () =>
            {
                Debug.Log($"Player signed out");
            };

            await AuthenticationService.Instance.SignInAnonymouslyAsync();

            return AuthenticationService.Instance.PlayerId;

        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }

        return null;
    }

    public event Action<LobbyInfo>? HostedLobby;

    [Command]
    public async Task<Lobby?> Host()
    {
        try
        {
            var a = await RelayService.Instance.CreateAllocationAsync(MAX_PLAYERS);
            var joinCode = await RelayService.Instance.GetJoinCodeAsync(a.AllocationId);

            var lobby = await Lobbies.Instance.CreateLobbyAsync(
                "Lobby names are useless", 
                MAX_PLAYERS, new CreateLobbyOptions
                {
                    Data = new Dictionary<string, DataObject> {
                        { 
                            "RelayJoinCode", new DataObject(
                                DataObject.VisibilityOptions.Public, 
                                value: joinCode, 
                                index: DataObject.IndexOptions.S1
                            ) 
                        }
                    }
                }
            );

            StartCoroutine(LobbyHeardbeat(lobby.Id, 15));

            var _hostData = new RelayHostData
            {
                Key = a.Key,
                Port = (ushort)a.RelayServer.Port,
                AllocationID = a.AllocationId,
                AllocationIDBytes = a.AllocationIdBytes,
                ConnectionData = a.ConnectionData,
                IPv4Address = a.RelayServer.IpV4
            };

            // point to the relay server
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(
                _hostData.IPv4Address, 
                _hostData.Port, 
                _hostData.AllocationIDBytes, 
                _hostData.Key, 
                _hostData.ConnectionData);

            // continue as a host

            Debug.Log($"Starting host connection to the {_hostData.IPv4Address}:{_hostData.Port}...");
            NetworkManager.Singleton.StartHost();

            await SimpleLevelManager.Instance.LoadLevelAsync(SimpleLevelManager.Instance.LobbyLevel);
            _currentLobby = lobby;

            HostedLobby?.Invoke(new LobbyInfo()
            {
                JoinCode = joinCode,
                LobbyId = lobby.Id,
            });

            return lobby;
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to create a lobby" + e);
            return null;
        }
    }

    public event Action<LobbyInfo>? JoinedLobby;
    
    [Command]
    public async Task<Lobby?> Join(string joinCode)
    {
        try
        {
            var filters = new List<QueryFilter>()
            {
                new QueryFilter(
                    field: QueryFilter.FieldOptions.S1,
                    op: QueryFilter.OpOptions.EQ,
                    value: joinCode
                )
            };

            var lobbies = await Lobbies.Instance.QueryLobbiesAsync(new QueryLobbiesOptions() {
                Filters = filters
            });

            // we should get a single lobby that is found by the joinCode
            var lobby = lobbies.Results.FirstOrDefault();
            if (lobby == null)
            {
                Debug.Log($"Lobby with the code [{joinCode}] was not found...");
                return null;
            }

            var joinedLobby = await Lobbies.Instance.JoinLobbyByIdAsync(lobby.Id);
            if (joinedLobby == null)
            {
                Debug.LogError($"Somehow could not join lobby with the correct code [{joinCode}] and id [{lobby.Id}] !!!");
                return null;
            }
            Debug.Log($"Lobby[{joinedLobby.Id}]: joined!");
            Debug.Log($"Lobby[{joinedLobby.Id}]: Players count: {joinedLobby.Players.Count}");

            var a = await RelayService.Instance.JoinAllocationAsync(joinCode);
            _currentLobby = joinedLobby;

            // Create Object
            var _joinData = new RelayJoinData
            {
                Key = a.Key,
                Port = (ushort)a.RelayServer.Port,
                AllocationID = a.AllocationId,
                AllocationIDBytes = a.AllocationIdBytes,
                ConnectionData = a.ConnectionData,
                HostConnectionData = a.HostConnectionData,
                IPv4Address = a.RelayServer.IpV4
            };

            // Set transport data
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(
                _joinData.IPv4Address,
                _joinData.Port,
                _joinData.AllocationIDBytes,
                _joinData.Key,
                _joinData.ConnectionData,
                _joinData.HostConnectionData);

            await SimpleLevelManager.Instance.LoadLevelAsync(null);

            // continue as a client
            Debug.Log($"Starting client connection to the {_joinData.IPv4Address}:{_joinData.Port}...");
            NetworkManager.Singleton.StartClient();

            // ------------------- TODO: here we could detect current level and join according to the current level, but.....
            //await SimpleLevelManager.Instance.LoadLevelAsync(SimpleLevelManager.Instance.LobbyLevel);
            // -------------------
            _currentLobby = joinedLobby;

            JoinedLobby?.Invoke(new LobbyInfo()
            {
                LobbyId = joinCode,
                JoinCode = joinCode
            });

            return joinedLobby;

        }
        catch (Exception e) 
        {
            Debug.LogError(e);
            return null;
        }
    }

    public event Action? LeftLobby;

    [Command]
    public async Task Leave(bool loadMainMenu = true)
    {
        if (_currentLobby == null)
            return;

        if (_currentLobby.HostId == _playerId)
        {
            Debug.Log($"Leaving as host from Lobby[{_currentLobby.Id}]: Deleting...");
            StopAllCoroutines();
            await Lobbies.Instance.DeleteLobbyAsync(_currentLobby.Id);
            _currentLobby = null;
        }
        else
        {
            Debug.Log($"Leaving from Lobby[{_currentLobby.Id}]: ...");
            await Lobbies.Instance.RemovePlayerAsync(_currentLobby.Id, _playerId);
            _currentLobby = null;
        }

        Debug.Log($"Stopping connections...");
        if(NetworkManager.Singleton != null)
            NetworkManager.Singleton.Shutdown();
        LeftLobby?.Invoke();

        if(loadMainMenu)
            await SimpleLevelManager.Instance.LoadLevelAsync(SimpleLevelManager.Instance.MainMenuLevel);
    }

    private async void OnDestroy()
    {
        await Leave(loadMainMenu: false);
    }

    private IEnumerator LobbyHeardbeat(string lobbyId, float intervalSec)
    {
        var delay = new WaitForSecondsRealtime(intervalSec);
        while (true)
        {
            Lobbies.Instance.SendHeartbeatPingAsync(lobbyId);
            Debug.Log($"Lobby[{lobbyId}]: Heardbeat...");
            yield return delay;
        }
    }
}
