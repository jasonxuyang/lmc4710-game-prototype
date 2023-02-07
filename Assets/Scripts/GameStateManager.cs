using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public const float CREATE_TASK_INTERVAL_RESET = 5;
    public const int MAP_WIDTH = 32;
    public const int MAP_HEIGHT = 32;

    public float createTaskClock;
    public List<Task> tasks;
    public float gameTime;

    public int mapWidth;
    public int mapHeight;

    // Start is called before the first frame update
    void Start()
    {
        tasks = new List<Task>();
        gameTime = 0;
        createTaskClock = 0;
        mapWidth = MAP_WIDTH;
        mapHeight = MAP_HEIGHT;
        Debug.Log("Game start");
    }

    // Update is called once per frame
    void Update()
    {
        if (createTaskClock >= CREATE_TASK_INTERVAL_RESET)
        {
            generateNewTask();
            createTaskClock = 0;
            Debug.Log("Created a new task");
        }
        gameTime += Time.deltaTime ;
        createTaskClock += Time.deltaTime;
    }

    // Generates a new task with a random position
    void generateNewTask()
    {
        int randX = Random.Range(0, MAP_WIDTH);
        int randY = Random.Range(0, MAP_HEIGHT);
        Task newTask = new Task(randX, randY);
        tasks.Add(new Task(randX, randY));

    }
}
