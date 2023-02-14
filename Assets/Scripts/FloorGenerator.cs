using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FloorGenerator : MonoBehaviour
{
    public TileBase wallTile;
    public TileBase floorTile;
    public Tilemap tilemap_floor;
    public Tilemap tilemap_walls;

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void makeRoom(Vector2Int origin, int width, int height)
    {
        // North/South Walls
        for (int i = -width + origin.x; i < width + origin.x; i++)
        {
            Vector3Int northWall = new Vector3Int(i, (height - 1 + origin.y), 0);
            Vector3Int southWall = new Vector3Int(i, -height + origin.y, 0);

            if (tilemap_floor.GetTile(northWall) != floorTile)
            {
                tilemap_walls.SetTile(northWall, wallTile);
            }
            if (tilemap_floor.GetTile(southWall) != floorTile)
            {
                tilemap_walls.SetTile(southWall, wallTile);
            }            
        }

        // East/West Walls
        for (int i = -height + origin.y; i < height + origin.y; i++)
        {
            Vector3Int eastWall = new Vector3Int((width - 1) + origin.x, i, 0);
            Vector3Int westWall = new Vector3Int(-width + origin.x, i, 0);

            if (tilemap_floor.GetTile(eastWall) != floorTile)
            {
                tilemap_walls.SetTile(eastWall, wallTile);
            }
            if (tilemap_floor.GetTile(westWall) != floorTile)
            {
                tilemap_walls.SetTile(westWall, wallTile);
            }
        }

        // Flooring
        width--;
        height--;
        for (int i = -width + origin.x; i < width + origin.x; i++)
        {
            for (int j = -height + origin.y; j < height + origin.y; j++)
            {
                Vector3Int floor = new Vector3Int(i, j, 0);
                if (tilemap_walls.GetTile(floor) == wallTile)
                {
                    tilemap_walls.SetTile(floor, null);
                }
                tilemap_floor.SetTile(floor, floorTile);
            }
        }
    }
}
