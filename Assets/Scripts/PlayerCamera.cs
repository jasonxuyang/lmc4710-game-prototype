using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerCamera : MonoBehaviour 
{
    private static PlayerCamera instance = null;
    private GameObject playerCamera;

    private void Awake()
    {
        if (instance == null)
        {
            playerCamera = GameObject.Find("Main Camera");
            instance = this;
        }
    }

    public static PlayerCamera getInstance() 
    {
        if (instance != null)
        { 
            return instance;
        }
        return null;
    }

    public void FollowPlayer(Transform transform)
    {
        //Vector2 smooth = Vector2.Lerp(playerCamera.transform.position, transform.position, 0.5f);
        playerCamera.transform.SetPositionAndRotation(new Vector3(transform.position.x,
            transform.position.y, playerCamera.transform.position.z), playerCamera.transform.rotation);
    }
}
