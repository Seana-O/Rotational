using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO.Pipes;

public class LevelInitializer : MonoBehaviour
{
    [SerializeField] LevelSetup setup;
    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject boxPrefab;
    [SerializeField] GameObject spikePrefab;
    [SerializeField] GameObject openTilePrefab;
    [SerializeField] GameObject closedTilePrefab;
    [SerializeField] GameObject finishTilePrefab;
    [SerializeField] GameObject canvas;
    GameObject tileParent;

    TileType[,] grid;

    int tileSize = 50;

    void Start()
    {
        if (!ValidSetup())
            Debug.LogError("invalid setup");

        Vector2 size = new Vector2(tileSize, tileSize);

        SetTileSize(openTilePrefab, size);
        SetTileSize(closedTilePrefab, size);
        SetTileSize(finishTilePrefab, size);
        SetTileSize(boxPrefab, size);
        SetTileSize(spikePrefab, new Vector2(tileSize, tileSize/8));
        SetTileSize(playerPrefab, size - new Vector2(1, 1));

        tileParent = new GameObject("Tiles");
        tileParent.transform.parent = canvas.transform;
        tileParent.transform.localScale = new Vector3(1, 1, 1);
        tileParent.transform.localPosition = new Vector3(0, 0, 0);

        InitializeGrid();
    }

    void SetTileSize(GameObject prefab, Vector2 size)
    {
        prefab.GetComponent<RectTransform>().sizeDelta = size;
        if(prefab.TryGetComponent(out BoxCollider2D b))
            b.size = size;
    }

    void InitializeGrid()
    {
        int width = setup.Width;
        int height = setup.Height;
        grid = new TileType[width, height];

        // create grid boundaries
        for(int x = 0; x < width; x++)
        {
            CreateTile(x, -1, closedTilePrefab);
            CreateTile(x, height, closedTilePrefab);
        }
        for(int y = 0; y < height; y++)
        {
            CreateTile(-1, y, closedTilePrefab);
            CreateTile(width, y, closedTilePrefab);  
        }

        for(int x = 0; x < width; x++)
            for(int y = 0; y < height; y++)
            {
                // create grid
                TileType t = new TileType();
                switch (setup.Layout[y * width + x])
                {
                    case 'o':
                        t = TileType.Open;
                        CreateTile(x, y, openTilePrefab);
                        break;
                    case 'x':
                        t = TileType.Closed;
                        CreateTile(x, y, closedTilePrefab);
                        break;
                    case 'e':
                        t = TileType.Finish;
                        CreateTile(x, y, finishTilePrefab);
                        break;
                    case 'b':
                        t = TileType.Open;
                        PlaceBlock(x, y);
                        CreateTile(x, y, openTilePrefab);
                        break;
                    case 'p':
                        t = TileType.Open;
                        PlacePlayer(x, y);
                        CreateTile(x, y, openTilePrefab);
                        break;
                }
                grid[x,y] = t;

                // add spikes
                for(int i = 0; i < setup.numberOfSpikeSets; i++)
                {
                    switch(setup.SpikeSets[i * width * height + y * width + x])
                    {
                        case 'n':
                            PlaceSpike(x, y, SpikeDirection.North);
                            break;
                        case 'e':
                            PlaceSpike(x, y, SpikeDirection.East);
                            break;
                        case 's':
                            PlaceSpike(x, y, SpikeDirection.South);
                            break;
                        case 'w':
                            PlaceSpike(x, y, SpikeDirection.West);
                            break;
                    }
                }
            }
    }
    void PlaceBlock(int x, int y)
    {
        GameObject box = Instantiate(boxPrefab, canvas.transform);
        box.transform.localPosition = GetWorldLocation(x,y);
    }

    void PlacePlayer(int x, int y)
    {
        GameObject player = Instantiate(playerPrefab, canvas.transform);
        player.transform.localPosition = GetWorldLocation(x,y);
    }

    void PlaceSpike(int x, int y, SpikeDirection dir)
    {
        GameObject spike = Instantiate(spikePrefab, canvas.transform);

        int zRotation = 0;
        Vector2 location = GetWorldLocation(x,y);

        switch (dir)
        {
            case SpikeDirection.North:
                location.y += tileSize/2 - spikePrefab.GetComponent<RectTransform>().sizeDelta.y / 2;
                zRotation = 180;
                break;
            case SpikeDirection.East:
                location.x += tileSize/2 - spikePrefab.GetComponent<RectTransform>().sizeDelta.y / 2;
                zRotation = 90;
                break;
            case SpikeDirection.South:
                location.y -= tileSize/2 - spikePrefab.GetComponent<RectTransform>().sizeDelta.y / 2;
                break;
            case SpikeDirection.West:
                location.x -= tileSize/2 - spikePrefab.GetComponent<RectTransform>().sizeDelta.y / 2;
                zRotation = -90;
                break;
        }

        spike.transform.localPosition = location;
        spike.transform.Rotate(new Vector3(0, 0, zRotation)); 
    }

    void CreateTile(int x, int y, GameObject prefab)
    {
        GameObject tile = Instantiate(prefab, tileParent.transform);
        tile.transform.localPosition = GetWorldLocation(x,y);
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