using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ComponentCreator : MonoBehaviour
{
    InitializationHelper helper;
    
    int width, height;

    [SerializeField] GameObject componentParent;

    GameObject addOnParent;

    void Start()
    {
        helper = new();

        addOnParent  = helper.CreateParent("AddOns", componentParent);
    }

    public void SetGridSize(int width, int height)
    {
        this.width = width;
        this.height = height;
    }

    public void CreateComponent(int x, int y, GameObject prefab)
    {
        GameObject obj = Instantiate(prefab, componentParent.transform);
        obj.transform.localPosition = helper.GetWorldLocation(x,y, width, height);
    }

    public void CreateAddOn(int x, int y, GameObject prefab, AddOnDirection dir)
    {
        GameObject obj = Instantiate(prefab, addOnParent.transform);

        int zRotation = 0;
        Vector2 location = helper.GetWorldLocation(x,y, width, height);
        float locationOffset = Const.TileSize/2 + prefab.GetComponent<RectTransform>().sizeDelta.y / 2 - 1;

        switch (dir)
        {
            case AddOnDirection.North:
                location.y += locationOffset;
                zRotation = 180;
                break;
            case AddOnDirection.East:
                location.x += locationOffset;
                zRotation = 90;
                break;
            case AddOnDirection.South:
                location.y -= locationOffset;
                break;
            case AddOnDirection.West:
                location.x -= locationOffset;
                zRotation = -90;
                break;
        }

        obj.transform.localPosition = location;
        obj.transform.Rotate(new Vector3(0, 0, zRotation)); 
    }
}

