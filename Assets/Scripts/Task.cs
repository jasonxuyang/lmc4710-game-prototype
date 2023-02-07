using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task
{
    private const int COMPLETION_TIME = 5;
    private const int INTERACTION_RANGE = 10;
  

    public int completionTime;
    public bool isComplete;
    public int interactingClock;
    public int interactionRange;
    public bool isInteracting;
    public int x;
    public int y;

    public Task(int x, int y)
    {
        this.x = x;
        this.y = y;
        this.completionTime = COMPLETION_TIME;
        this.isComplete = false;
        this.interactingClock = 0;
        this.interactionRange = INTERACTION_RANGE;
        this.isInteracting = false;
    }

}
