using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using System.Net;
using System.Linq;
using System.Net.Sockets;

public class GameStateManager : NetworkBehaviour
{
    public const int PLAYER_Z_INDEX = -2;
    public const int TASK_Z_INDEX = -1;
    public const int ROOM_Z_INDEX = 0;

    public const float CREATE_TASK_INTERVAL_RESET = 5;
    public float createTaskClock;
    public static HashSet<Task> tasks;
    public static Task lastAddedTask;
    public float gameTime;

    public const int ROOM_WIDTH = 8;
    public const int ROOM_HEIGHT = 8;
    public static List<Room> rooms;
    public float createRoomClock;
    public const float CREATE_ROOM_INTERVAL_RESET = 10;

    public GameObject room;
    public GameObject task;
    public GameObject mapManager;

    public static List<PlayerController> players;

    public static int score = 0;



    // Start is called before the first frame update
    void Start()
    {
        tasks = new HashSet<Task>();
        rooms = new List<Room>();
        players = new List<PlayerController>();

        gameTime = 0;
        createTaskClock = 0;
        createRoomClock = 0;
        Debug.Log("Game start");
        NetworkManager.Singleton.GetComponent<UnityTransport>().SetConnectionData("127.0.0.1", (ushort)12345, "0.0.0.0"); //"143.215.84.16"

        //var host = Dns.GetHostEntry(Dns.GetHostName());
        //foreach (var ip in host.AddressList)
        //{
        //    if (ip.AddressFamily == AddressFamily.InterNetwork)
        //    {
        //        Debug.Log(ip.ToString());
        //        NetworkManager.Singleton.GetComponent<UnityTransport>().SetConnectionData(ip.ToString(), (ushort)12345, "0.0.0.0");
        //    }
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner) return;

        if (NetworkManager.Singleton.IsListening)
        {
            if (rooms.Count == 0)
            {
                generateNewRoom();
            }
            if (createTaskClock >= CREATE_TASK_INTERVAL_RESET)
            {
                generateNewTaskServerRpc();
                createTaskClock = 0;
            }
            if (score >= 5/*createRoomClock >= CREATE_ROOM_INTERVAL_RESET*/)
            {
                generateNewRoom();
                createRoomClock = 0;
                score = 0;
            }
            gameTime += Time.deltaTime;
            createTaskClock += Time.deltaTime;
            //createRoomClock += Time.deltaTime;
        }
    }

    // Generates a new task with a random position
    [ServerRpc]
    void generateNewTaskServerRpc()
    {
        Room randomRoom = rooms[Random.Range(0, rooms.Count)]; // make network variable (or at least try)
        int randomX = Random.Range(-ROOM_WIDTH / 2, ROOM_WIDTH / 2);
        int randomY = Random.Range(-ROOM_HEIGHT / 2, ROOM_HEIGHT / 2);
        Task newTask = new Task(randomRoom, randomX, randomY);
        lastAddedTask = newTask; // make network variable
        tasks.Add(newTask); // make network vairable
        spawnTaskClientRpc(newTask.globalX, newTask.globalY);
    }

    // Adds a room to the map
    void generateNewRoom()
    {
        if (rooms.Count == 0) {
            Room startRoom = new Room();
            rooms.Add(startRoom);
            spawnRoom(startRoom);
        } 
        else
        {
            // pick a random room
            Room parentRoom = rooms[Random.Range(0, rooms.Count)];

            // get random direction
            List<Room.DIRECTION> openDirections = parentRoom.getOpenDirections();
            Room.DIRECTION randomDirection = openDirections[Random.Range(0, openDirections.Count)];

            // add child room
            Room childRoom = new Room(parentRoom, randomDirection);

            // build room on map
            rooms.Add(childRoom);
            spawnRoom(childRoom);
        }
        Room newRoom = rooms[rooms.Count - 1];
        mapManager.GetComponent<FloorGenerator>().makeRoom(new Vector2Int(newRoom.globalX, newRoom.globalY), (ROOM_WIDTH / 2) + 1, (ROOM_HEIGHT / 2) + 1);
    }


    void spawnRoom(Room newRoom)
    {
        GameObject roomToSpawn = Instantiate(room);
        roomToSpawn.name = "Room " + rooms.Count;
        roomToSpawn.transform.position = new Vector3(newRoom.globalX, newRoom.globalY, ROOM_Z_INDEX);
        Debug.Log("Spawned new room");
    }

    // Spawn same task to all clients
    [ClientRpc] 
    void spawnTaskClientRpc(int globalX, int globalY)
    {
        GameObject taskToSpawn = Instantiate(task);
        taskToSpawn.name = "Task " + tasks.Count;
        taskToSpawn.transform.position = new Vector3(globalX, globalY, TASK_Z_INDEX);
        Debug.Log("Spawned new room");
    }

    //// Prints the current grid of rooms
    //void printGrid()
    //{
    //    string message = "";
    //    for (int i = 0; i < 10; i++)
    //    {
    //        for (int j = 0; j < 10; j++)
    //        {
    //            if (j == 10 - 1)
    //            {
    //                if (this.map[i][j] == null)
    //                {
    //                    message = message + "[--]\n";
    //                }
    //                else if (this.map[i][j] < 10)
    //                {
    //                    message = message + "[0" + this.map[i][j] + "]\n";
    //                }
    //                else
    //                {
    //                    message = message + "[" + this.map[i][j] + "]\n";
    //                }
    //            }
    //            else
    //            {
    //                if (this.map[i][j] == null)
    //                {
    //                    message = message + "[--] ";
    //                }
    //                else if (this.map[i][j] < 10)
    //                {
    //                    message = message + "[0" + this.map[i][j] + "] ";
    //                }
    //                else
    //                {
    //                    message = message + "[" + this.map[i][j] + "] ";
    //                }
    //            }
    //        }
    //    }
    //    Debug.Log(message);
    //}
}
