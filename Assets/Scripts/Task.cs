using System;

public class Task
{
    private const int COMPLETION_TIME = 5;
    private const int INTERACTION_RANGE = 10;
  

    public int completionTime;
    public bool isComplete;
    public int interactingClock;
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
        this.completionTime = COMPLETION_TIME;
        this.isComplete = false;
        this.interactingClock = 0;
        this.interactionRange = INTERACTION_RANGE;
        this.isInteracting = false;
    }

}
