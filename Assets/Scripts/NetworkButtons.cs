using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine;
using UnityEngine.UI;

public class NetworkButtons : MonoBehaviour
{
    public Button create;
    public Button join;
    public GameObject startScreen;

    private UnityTransport transport;
    private const int MaxPlayers = 8;

    //private async void Awake()
    //{
    //    startScreen.SetActive(false);
    //    transport = GetComponent<UnityTransport>();

    //await UnityServices.InitializeAsync();
    //await AuthenticationService.Instance.SignInAnonymouslyAsync();

    //    startScreen.SetActive(true);
    //}

    //public async void CreateGame()
    //{
    //    startScreen.SetActive(false);

    //    Allocation a = await RelayService.Instance.CreateAllocationAsync(MaxPlayers);
    //    string code = await RelayService.Instance.GetJoinCodeAsync(a.AllocationId);
    //    Debug.Log(code);

    //    transport.SetHostRelayData(a.RelayServer.IpV4, (ushort)a.RelayServer.Port, a.AllocationIdBytes, a.Key, a.ConnectionData);

    //    NetworkManager.Singleton.StartHost();
    //}

    //public async void JoinGame()
    //{
    //    startScreen.SetActive(false);

    //    JoinAllocation j = await RelayService.Instance.JoinAllocationAsync(""); // creat textbox

    //    transport.SetClientRelayData(j.RelayServer.IpV4, (ushort)j.RelayServer.Port, j.AllocationIdBytes, j.Key, j.ConnectionData, j.HostConnectionData);

    //    NetworkManager.Singleton.StartClient();
    //}




    private void Start()
    {
        create.onClick.AddListener(Create);
        join.onClick.AddListener(Join);
    }

    private void Create()
    {
        NetworkManager.Singleton.StartHost();
        GameObject.Find("Start Screen").SetActive(false);
    }
    private void Join()
    {
        NetworkManager.Singleton.StartClient();
        GameObject.Find("Start Screen").SetActive(false);
    }
}
