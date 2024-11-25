using UnityEngine;

public class GridInitializer : MonoBehaviour
{
    InitializationHelper helper;

    int width, height;

    [SerializeField] GameObject gridParent;

    GameObject tileParent, cornerParent;

    void Start()
    {
        helper = new();

        tileParent   = helper.CreateParent("Tiles"   , gridParent);
        cornerParent = helper.CreateParent("Corners" , gridParent);
    }

    public void SetGridSize(int width, int height)
    {
        this.width = width;
        this.height = height;
    }
    
    public void CreateGridBase(GameObject closedTilePrefab, GameObject openTilePrefab, GameObject cornerPrefab)
    {
        for(int x = 0; x < width; x++)
        {
            CreateTile(x, -1, closedTilePrefab);        // lower grid boundaries
            CreateTile(x, height, closedTilePrefab);    // upper grid boundaries

            for(int y = 0; y < height; y++)             // fill grid with open tiles
                CreateTile(x, y, openTilePrefab);
        }
        for(int y = 0; y < height; y++)
        {
            CreateTile(-1, y, closedTilePrefab);        // left grid boundaries
            CreateTile(width, y, closedTilePrefab);     // right grid boundaries
        }

        AddCorners(width, height, cornerPrefab);
    }

    public void CreateTile(int x, int y, GameObject prefab)
    {
        GameObject tile = Instantiate(prefab, tileParent.transform);
        tile.transform.localPosition = helper.GetWorldLocation(x,y, width, height);
    }

    void AddCorners(int w, int h, GameObject cornerPrefab)
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
        GameObject corner = Instantiate(prefab, cornerParent.transform);
        corner.transform.localPosition = helper.GetWorldLocation(x, y, width, height);
    }
}
