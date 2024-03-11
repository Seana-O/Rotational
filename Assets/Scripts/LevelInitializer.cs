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
    [SerializeField] GameObject cornerPrefab;
    [SerializeField] GameObject canvas;
    [SerializeField] GameObject componentParent;
    [SerializeField] GameObject nextLevelButton;
    GameObject tileParent;
    GameObject spikeParent;
    GameObject boxParent;

    TileType[,] grid;

    readonly int tileSize = 100;

    void Start()
    {
        setup = GetComponent<SetupPicker>().GetSetup();

        if (!ValidSetup())
            Debug.LogError("invalid setup");

        Vector2 size = new Vector2(tileSize, tileSize);

        SetTileSize(openTilePrefab, size);
        SetTileSize(closedTilePrefab, size);
        SetTileSize(finishTilePrefab, size);
        SetTileSize(boxPrefab, size - new Vector2(1, 1));
        SetTileSize(spikePrefab, new Vector2(tileSize, tileSize/8));
        SetTileSize(playerPrefab, size - new Vector2(1, 1));

        tileParent = new GameObject("Tiles");
        tileParent.transform.parent = componentParent.transform;
        tileParent.transform.localScale = new Vector3(1, 1, 1);
        tileParent.transform.localPosition = new Vector3(0, 0, 0);

        spikeParent = new GameObject("Spikes");
        spikeParent.transform.parent = componentParent.transform;
        spikeParent.transform.localScale = new Vector3(1, 1, 1);
        spikeParent.transform.localPosition = new Vector3(0, 0, 0);

        boxParent = new GameObject("Boxes");
        boxParent.transform.parent = componentParent.transform;
        boxParent.transform.localScale = new Vector3(1, 1, 1);
        boxParent.transform.localPosition = new Vector3(0, 0, 0);

        InitializeGrid();

        if(setup.TutorialBox != null)
            Instantiate(setup.TutorialBox, canvas.transform);

        if(GetComponent<SetupPicker>().IsLastLevel())
            nextLevelButton.SetActive(false);
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
        AddCorners(width, height);

        for(int x = 0; x < width; x++)
            for(int y = 0; y < height; y++)
            {
                // create grid
                TileType t = new();
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
                        CreateTile(x, y, openTilePrefab);
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
                for(int i = 0; i < setup.NumberOfSpikeSets; i++)
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
        GameObject box = Instantiate(boxPrefab, boxParent.transform);
        box.transform.localPosition = GetWorldLocation(x,y);
    }

    void PlacePlayer(int x, int y)
    {
        GameObject player = Instantiate(playerPrefab, componentParent.transform);
        player.transform.localPosition = GetWorldLocation(x,y);
    }

    void PlaceSpike(int x, int y, SpikeDirection dir)
    {
        GameObject spike = Instantiate(spikePrefab, spikeParent.transform);

        int zRotation = 0;
        Vector2 location = GetWorldLocation(x,y);
        float locationOffset = tileSize/2 + spikePrefab.GetComponent<RectTransform>().sizeDelta.y / 2 - 1;

        switch (dir)
        {
            case SpikeDirection.North:
                location.y += locationOffset;
                zRotation = 180;
                break;
            case SpikeDirection.East:
                location.x += locationOffset;
                zRotation = 90;
                break;
            case SpikeDirection.South:
                location.y -= locationOffset;
                break;
            case SpikeDirection.West:
                location.x -= locationOffset;
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

    void AddCorners(int w, int h)
    {
        cornerPrefab.transform.rotation = Quaternion.identity;
        cornerPrefab.transform.Rotate(new Vector3(0, 0, 45));
        CreateTile(-1, -1, cornerPrefab);

        cornerPrefab.transform.Rotate(new Vector3(0, 0, 90));
        CreateTile(-1, h, cornerPrefab);

        cornerPrefab.transform.Rotate(new Vector3(0, 0, 90));
        CreateTile(w, h, cornerPrefab);
        
        cornerPrefab.transform.Rotate(new Vector3(0, 0, 90));
        CreateTile(w, -1, cornerPrefab);
    }

    bool ValidSetup()
    {
        return setup.Layout.Length == setup.Width * setup.Height 
            && setup.Layout.Length == setup.SpikeSets.Length / setup.NumberOfSpikeSets;
    }

    Vector2 GetWorldLocation(int gridX, int gridY)
    {
        float width = (float)setup.Width/2;
        float height = (float)setup.Height/2;

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