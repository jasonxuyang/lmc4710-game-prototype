using System;
using System.Collections.Generic;

public class Room
{
    public enum DIRECTION
    {
        NORTH, EAST, SOUTH, WEST, NONE
    }
    public Room parent;
    public Room north;
    public Room east;
    public Room south;
    public Room west;
    public int x;
    public int y;
    public int globalX;
    public int globalY;

    public Room(Room parent = null, DIRECTION parentDirection = DIRECTION.NONE)
    {
        this.parent = parent;
        switch(parentDirection)
        {
            case DIRECTION.NORTH:
                this.x = parent.x;
                this.y = parent.y - 1;
                this.parent.north = this;
                break;
            case DIRECTION.EAST:
                this.x = parent.x - 1;
                this.y = parent.y;
                this.parent.east = this;
                break;
            case DIRECTION.SOUTH:
                this.x = parent.x;
                this.y = parent.y + 1;
                this.parent.south = this;
                break;
            case DIRECTION.WEST:
                this.x = parent.x + 1;
                this.y = parent.y;
                this.parent.west = this;
                break;
            default:
                this.x = 0;
                this.y = 0;
                break;
        }
        this.globalX = this.x * GameStateManager.ROOM_WIDTH;
        this.globalY = this.y * GameStateManager.ROOM_HEIGHT;
    }

    public List<DIRECTION> getOpenDirections()
    {
        List<DIRECTION> openDirections = new List<DIRECTION>();
        if (this.north == null)
        {
            openDirections.Add(DIRECTION.NORTH);
        }
        if (this.east == null)
        {
            openDirections.Add(DIRECTION.EAST);
        }
        if (this.south == null)
        {
            openDirections.Add(DIRECTION.SOUTH);
        }
        if (this.west == null)
        {
            openDirections.Add(DIRECTION.WEST);
        }
        return openDirections;
    }

}
