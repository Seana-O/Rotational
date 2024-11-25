using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO.Pipes;
using UnityEditor;

public class LevelInitializer : MonoBehaviour
{
    [SerializeField] LevelSetup setup;
    [SerializeField] GameObject canvas;
    
    [SerializeField] GameObject nextLevelButton;

    [SerializeField] GameObject 
        playerPrefab, boxPrefab, spikePrefab, 
        spikeBoxPrefab, stickyPrefab, finishTilePrefab, 
        openTilePrefab, closedTilePrefab, cornerPrefab;

    GridInitializer gridInitializer;
    ComponentCreator componentCreator;

    void Start()
    {
        setup = GetComponent<SetupPicker>().GetSetup();

        if (!ValidSetup())
            Debug.LogError("invalid setup");

        InitializeObjects();

        gridInitializer = FindObjectOfType<GridInitializer>();
        gridInitializer.SetGridSize(setup.Width, setup.Height);
        gridInitializer.CreateGridBase(closedTilePrefab, openTilePrefab, cornerPrefab);

        componentCreator = FindObjectOfType<ComponentCreator>();
        componentCreator.SetGridSize(setup.Width, setup.Height);
        CreateComponents();

        if(setup.TutorialBox != null)
            Instantiate(setup.TutorialBox, canvas.transform);

        if(GetComponent<SetupPicker>().IsLastLevel())
            nextLevelButton.SetActive(false);
    }

    bool ValidSetup()
    {
        return setup.Layout.Length == setup.Width * setup.Height 
            && (setup.SpikeSets == null || setup.Layout.Length == setup.SpikeSets.Length / setup.NumberOfSpikeSets);
    }

    void InitializeObjects()
    {
        int tileSize = Const.TileSize;
        Vector2 size = new(tileSize, tileSize);
        
        // set tile sizes
        SetTileSize(openTilePrefab  , size);
        SetTileSize(closedTilePrefab, size);
        SetTileSize(finishTilePrefab, size);
        SetTileSize(boxPrefab       , size - new Vector2(1, 1));
        SetTileSize(spikeBoxPrefab  , size - new Vector2(1, 1));
        SetTileSize(spikePrefab     , new Vector2(tileSize, tileSize/8));
        SetTileSize(stickyPrefab    , new Vector2(tileSize, tileSize/8));
        SetTileSize(playerPrefab    , size - new Vector2(1, 1));
    }

    public void SetTileSize(GameObject prefab, Vector2 size)
    {
        prefab.GetComponent<RectTransform>().sizeDelta = size;
        if(prefab.TryGetComponent(out BoxCollider2D b))
            b.size = size;
    }

    void CreateComponents()
    {
        int width = setup.Width;
        int height = setup.Height;

        for(int x = 0; x < width; x++)
            for(int y = 0; y < height; y++)
            {
                switch (setup.Layout[y * width + x])
                {
                    case 'x':
                        gridInitializer.CreateTile(x, y, closedTilePrefab);
                        break;
                    case 'e':
                        gridInitializer.CreateTile(x, y, finishTilePrefab);
                        break;
                    case 'b':
                        componentCreator.CreateComponent(x, y, boxPrefab);
                        break;
                    case 's':
                        componentCreator.CreateComponent(x, y, spikeBoxPrefab);
                        break;
                    case 'p':
                        componentCreator.CreateComponent(x, y, playerPrefab);
                        break;
                }

                if(setup.SpikeSets != null)
                {
                    // add spikes
                    for(int i = 0; i < setup.NumberOfSpikeSets; i++)
                    {
                        char c = setup.SpikeSets[i * width * height + y * width + x];

                        AddOnDirection dir = GetDirection(c);

                        if(dir != AddOnDirection.none)
                        {
                            componentCreator.CreateAddOn(x, y, spikePrefab, dir);
                        }
                    }
                }

                if(setup.StickySets != null)
                {
                    
                    // add sticky tiles
                    char c = setup.StickySets[y * width + x];

                    AddOnDirection dir = GetDirection(c);

                    if(dir != AddOnDirection.none)
                    {
                        componentCreator.CreateAddOn(x, y, stickyPrefab, dir);
                    }

                }
            }
    }

    AddOnDirection GetDirection(char c)
    {
        switch(c)
        {
            case 'n':
                return AddOnDirection.North;
            case 'e':
                return AddOnDirection.East;
            case 's':
                return AddOnDirection.South;
            case 'w':
                return AddOnDirection.West;
            default:
                return AddOnDirection.none;
        }
    }
}

public enum AddOnDirection
{
    North,
    East,
    South,
    West,
    none
}