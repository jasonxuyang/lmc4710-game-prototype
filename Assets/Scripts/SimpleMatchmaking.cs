using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine;

public class SimpleMatchmaking : MonoBehaviour
{
    // Attributes
    public GameObject startScreen;
    private Lobby connected_lobby;
    private UnityTransport transport;
    private const string join_code = "";
    private string player_id;


    // Get Unity Transport
    private void Awake() => transport = FindObjectOfType<UnityTransport>();

    // Create or Join a Lobby
    public async void CreateOrJoinLobby()
    {
        await UnityServices.InitializeAsync(new InitializationOptions());
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
        player_id = AuthenticationService.Instance.PlayerId;

        connected_lobby = await QuickJoinLobby() ?? await CreateLobby();

        if (connected_lobby != null) startScreen.SetActive(false);
    }

    // Quick Join
    private async Task<Lobby> QuickJoinLobby()
    {
        try
        {
            var lobby = await Lobbies.Instance.QuickJoinLobbyAsync();
            var a = await RelayService.Instance.JoinAllocationAsync(lobby.Data[join_code].Value);

            SetTransformAsClient(a);

            NetworkManager.Singleton.StartClient();
            return lobby;
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
            return null;
        }
    }

    // Create Lobby
    private async Task<Lobby> CreateLobby()
    {
        try
        {
            const int max_players = 8;

            var a = await RelayService.Instance.CreateAllocationAsync(max_players);
            var joinCode = await RelayService.Instance.GetJoinCodeAsync(a.AllocationId);

            var options = new CreateLobbyOptions
            {
                Data = new Dictionary<string, DataObject> { { join_code, new DataObject(DataObject.VisibilityOptions.Public, joinCode) } }
            };
            var lobby = await Lobbies.Instance.CreateLobbyAsync("Lobby Name", max_players, options);

            StartCoroutine(Heartbeat(lobby.Id, 15));

            transport.SetHostRelayData(a.RelayServer.IpV4, (ushort)a.RelayServer.Port, a.AllocationIdBytes, a.Key, a.ConnectionData);

            NetworkManager.Singleton.StartHost();
            return lobby;
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
            return null;
        }
    }

    // Set Transform As Client
    private void SetTransformAsClient(JoinAllocation j)
    {
        transport.SetClientRelayData(j.RelayServer.IpV4, (ushort)j.RelayServer.Port, j.AllocationIdBytes, j.Key, j.ConnectionData, j.HostConnectionData);
    }

    // Lobby Access Heartbeat
    private static IEnumerator Heartbeat(string lobbyId, float wait_seconds)
    {
        var delay = new WaitForSecondsRealtime(wait_seconds);
        while (true)
        {
            Lobbies.Instance.SendHeartbeatPingAsync(lobbyId);
            yield return delay;
        }
    }

    // On Destroy
    private void OnDestroy()
    {
        try
        {
            StopAllCoroutines();
            if (connected_lobby != null) 
            {
                if (connected_lobby.HostId == player_id) Lobbies.Instance.DeleteLobbyAsync(connected_lobby.Id);
                else Lobbies.Instance.RemovePlayerAsync(connected_lobby.Id, player_id);
            }
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
    }
}
