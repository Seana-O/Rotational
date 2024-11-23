using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LevelComponent : MonoBehaviour, IDragHandler, IEndDragHandler
{
    void Start()
    {
        if(TryGetComponent<ComponentSpawner>(out ComponentSpawner spawner))
            Destroy(spawner);
        if(TryGetComponent<EventTrigger>(out EventTrigger trigger))
            Destroy(trigger);
    }

    public void OnDrag (PointerEventData eventData)
    {
        this.transform.position += (Vector3)eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // place on grid
    }
}
