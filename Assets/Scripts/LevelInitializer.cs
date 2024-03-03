using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelInitializer : MonoBehaviour
{
    [SerializeField] LevelSetup setup;
    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject boxPrefab;
    [SerializeField] GameObject spikePrefab;
    [SerializeField] GameObject openTilePrefab;
    [SerializeField] GameObject closedTilePrefab;
    [SerializeField] GameObject finishTilePrefab;
    [SerializeField] GameObject objectParent;

    TileType[,] grid;
    List<Block> blocks;
    List<(int x, int y, SpikeDirection dir)> spikes;

    int tileSize = 50;

    void Start()
    {
        if (!ValidSetup())
            Debug.LogError("invalid setup");

        openTilePrefab.GetComponent<RectTransform>().sizeDelta = new Vector2(tileSize, tileSize);
        closedTilePrefab.GetComponent<RectTransform>().sizeDelta = new Vector2(tileSize, tileSize);
        finishTilePrefab.GetComponent<RectTransform>().sizeDelta = new Vector2(tileSize, tileSize);

        InitializeGrid();
    }

    void InitializeGrid()
    {
        int width = setup.Width;
        int height = setup.Height;
        grid = new TileType[width, height];
        blocks = new();
        spikes = new();

        for(int x = 0; x < width; x++)
            for(int y = 0; y < height; y++)
            {
                // create grid
                TileType t = new TileType();
                switch (setup.Layout[y * width + x])
                {
                    case 'o':
                        t = TileType.Open;
                        GameObject oTile = Instantiate(openTilePrefab, objectParent.transform);
                        oTile.transform.localPosition = GetWorldLocation(x,y);
                        break;
                    case 'x':
                        t = TileType.Closed;
                        GameObject cTile = Instantiate(closedTilePrefab, objectParent.transform);
                        cTile.transform.localPosition = GetWorldLocation(x,y);
                        break;
                    case 'e':
                        t = TileType.Finish;
                        GameObject fTile = Instantiate(finishTilePrefab, objectParent.transform);
                        fTile.transform.localPosition = GetWorldLocation(x,y);
                        break;
                    case 'b':
                        t = TileType.Open;
                        PlaceBlock(x, y);
                        break;
                    case 'p':
                        t = TileType.Open;
                        PlacePlayer(x, y);
                        break;
                }

                grid[x,y] = t;

                // add spikes
                for(int i = 0; i < setup.numberOfSpikeSets; i++)
                {
                    SpikeDirection dir = new SpikeDirection();

                    switch(setup.SpikeSets[i * width * height + y * width + x])
                    {
                        case 'n':
                            dir = SpikeDirection.North;
                            break;
                        case 'e':
                            dir = SpikeDirection.East;
                            break;
                        case 's':
                            dir = SpikeDirection.South;
                            break;
                        case 'w':
                            dir = SpikeDirection.West;
                            break;
                    }

                    PlaceSpike(x, y, dir);

                    spikes.Add((x, y, dir));
                }
            }
    }
    void PlaceBlock(int x, int y)
    {
        //blocks.Add(new Block());
    }

    void PlacePlayer(int x, int y)
    {
        Instantiate(playerPrefab, GetWorldLocation(x,y), new Quaternion());
    }

    void PlaceSpike(int x, int y, SpikeDirection dir)
    {
        
    }

    bool ValidSetup()
    {
        return setup.Layout.Length == setup.Width * setup.Height 
            && setup.Layout.Length == setup.SpikeSets.Length / setup.numberOfSpikeSets;
    }

    Vector2 GetWorldLocation(int gridX, int gridY)
    {
        float width = (int)(setup.Width/2);
        float height = (int)(setup.Height/2);

        float x = (gridX - width) * tileSize + tileSize/2;
        float y = -((gridY - height) * tileSize + tileSize/2);
        Debug.Log(x + " " + y);

        return new Vector2(x,y);
    }
}

public enum TileType
{
    Open,
    Closed,
    Finish
}

public enum SpikeDirection
{
    North,
    East,
    South,
    West
}