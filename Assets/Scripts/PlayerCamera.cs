using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerCamera : MonoBehaviour 
{
    private static PlayerCamera instance = null;
    private GameObject playerCamera;
    public float time = 1f;
    private Vector2 velocity = Vector2.zero;

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
        Vector2 smooth = Vector2.SmoothDamp(playerCamera.transform.position, transform.position, ref velocity, Time.deltaTime * time);
        playerCamera.transform.SetPositionAndRotation(new Vector3(smooth.x,
            smooth.y, playerCamera.transform.position.z), playerCamera.transform.rotation);
        //playerCamera.transform.SetPositionAndRotation(new Vector3(transform.position.x,
        //    transform.position.y, playerCamera.transform.position.z), playerCamera.transform.rotation);
    }
}
