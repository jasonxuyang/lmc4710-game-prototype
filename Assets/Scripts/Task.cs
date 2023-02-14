using System;

public class Task
{
    public const float COMPLETION_TIME = 3;
    public const int INTERACTION_RANGE = 2;
  

    public bool isComplete;
    public float interactingClock;
    public int interactionRange;
    public bool isInteracting;
    public Room room;
    public int x;
    public int y;
    public int globalX;
    public int globalY;

    public Task(Room room, int x, int y)
    {
        this.room = room;
        this.x = x;
        this.y = y;
        this.globalX = room.globalX + x;
        this.globalY = room.globalY + y;
        this.isComplete = false;
        this.interactingClock = 0;
        this.interactionRange = INTERACTION_RANGE;
        this.isInteracting = false;
    }

}
