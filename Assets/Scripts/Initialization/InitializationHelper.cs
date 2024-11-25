using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializationHelper
{
    public GameObject CreateParent(string name, GameObject parentObj)
    {
        GameObject obj = new GameObject(name);
        obj.transform.parent = parentObj.transform;
        obj.transform.localScale = new Vector3(1, 1, 1);
        obj.transform.localPosition = new Vector3(0, 0, 0);
        return obj;
    }

    public Vector2 GetWorldLocation(int gridX, int gridY, int width, int height)
    {
        float w = (float)width/2;
        float h = (float)height/2;

        float x = (gridX - w) * Const.TileSize + Const.TileSize/2;
        float y = -((gridY - h) * Const.TileSize + Const.TileSize/2);

        return new Vector2(x,y);
    }
}
