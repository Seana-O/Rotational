using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Grid : MonoBehaviour
{
    int width = 3, height = 3;

    [SerializeField] GameObject cornerPrefab;
    [SerializeField] GameObject closedTilePrefab;
    [SerializeField] GameObject openTilePrefab;
    [SerializeField] GameObject componentParent;
    [SerializeField] TMP_InputField xInput, yInput;

    TileType[,] grid;
    readonly int tileSize = 100;

    void Start()
    {
        xInput.onValueChanged.AddListener(delegate {SetGridSize(); });
        yInput.onValueChanged.AddListener(delegate {SetGridSize(); });
        ResetGrid();
    }

    void SetGridSize()
    {
        width = int.Parse(xInput.text);
        height = int.Parse(yInput.text);
        ResetGrid();
    }

    void ResetGrid()
    {
        ClearGrid();

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
    }

    void AddCorners(int w, int h)
    {
        cornerPrefab.transform.rotation = Quaternion.identity;
        cornerPrefab.transform.Rotate(new Vector3(0, 0, 45));
        CreateCorner(-1, -1, cornerPrefab);

        cornerPrefab.transform.Rotate(new Vector3(0, 0, 90));
        CreateCorner(-1, h, cornerPrefab);

        cornerPrefab.transform.Rotate(new Vector3(0, 0, 90));
        CreateCorner(w, h, cornerPrefab);
        
        cornerPrefab.transform.Rotate(new Vector3(0, 0, 90));
        CreateCorner(w, -1, cornerPrefab);
    }

    void CreateCorner(int x, int y, GameObject prefab)
    {
        GameObject corner = Instantiate(prefab, componentParent.transform);
        corner.transform.localPosition = GetWorldLocation(x,y);
    }

    void CreateTile(int x, int y, GameObject prefab)
    {
        GameObject tile = Instantiate(prefab, componentParent.transform);
        tile.transform.localPosition = GetWorldLocation(x,y);
    }

    Vector2 GetWorldLocation(int gridX, int gridY)
    {
        float w = (float)width/2;
        float h = (float)height/2;

        float x = (gridX - w) * tileSize + tileSize/2;
        float y = -((gridY - h) * tileSize + tileSize/2);

        return new Vector2(x,y);
    }

    public void ClearGrid()
    {
        foreach(Transform child in componentParent.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
