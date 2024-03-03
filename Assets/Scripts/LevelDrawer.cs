using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class LevelDrawer : MonoBehaviour
{    
    public void DrawLevel(TileType[,] grid, List<(int x, int y, SpikeDirection dir)> spikes)
    {
        // draw the grid
        for(int x = 0; x < grid.GetLength(0); x++)
            for(int y = 0; y < grid.GetLength(1); y++)
            {
                DrawTile(grid[x, y], x, y);
            }

        // draw the spikes
        foreach((int x, int y, SpikeDirection dir) in spikes)
            DrawSpike(x, y, dir);
    }

    void DrawTile(TileType t, int x, int y)
    {
        switch (t)
        {
            case TileType.Open:
                break;
            case TileType.Closed:
                break;
            case TileType.Finish:
                break;
        }
    }

    void DrawSpike(int x, int y, SpikeDirection dir)
    {
        switch (dir)
        {
            case SpikeDirection.North:
                break;
            case SpikeDirection.East:
                break;
            case SpikeDirection.South:
                break;
            case SpikeDirection.West:
                break;

        }
    }
}
