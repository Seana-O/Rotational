using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR;

public class ComponentSpawner : MonoBehaviour, IDragHandler
{
    GameObject componentParent;
    void Start()
    {
        componentParent = GameObject.Find("Components");
    }

    public void OnClick()
    {
        GameObject clone = Instantiate(gameObject);
        clone.transform.SetParent(transform.parent, false);
        clone.transform.localPosition = transform.localPosition;
        transform.SetParent(componentParent.transform, false);
        gameObject.AddComponent(typeof(LevelComponent));
    }

    public void OnDrag (PointerEventData eventData)
    {
        
    }
}
