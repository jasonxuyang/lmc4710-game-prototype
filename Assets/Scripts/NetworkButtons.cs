using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NetworkButtons : MonoBehaviour
{
    public Button create;
    public Button join;

    private void Start()
    {
        create.onClick.AddListener(Create);
        join.onClick.AddListener(Join);
    }

    //private void OnGUI()
    //{
    //    GUILayout.BeginArea(new Rect(10, 10, 300, 300));
    //    if (!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
    //    {
    //        //if (GUILayout.Button("Host")) NetworkManager.Singleton.StartHost();
    //        //if (GUILayout.Button("Server")) NetworkManager.Singleton.StartServer();
    //        //if (GUILayout.Button("Client")) NetworkManager.Singleton.StartClient();
    //    }
    //    GUILayout.EndArea();
    //}

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
