using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace LevelCreation
{
    public class LC_Component : MonoBehaviour, IDragHandler, IEndDragHandler
    {
        LC_GridTile currentGridTile;
        //bool active = false;

        void Start()
        {
            // Remove ComponentSpawner properties from this object
            if(TryGetComponent<LC_ComponentSpawner>(out LC_ComponentSpawner spawner))
                Destroy(spawner);
            if(TryGetComponent<EventTrigger>(out EventTrigger trigger))
                Destroy(trigger);
        }

        public void OnDrag (PointerEventData eventData)
        {
            //if (active) return;

            this.transform.position += (Vector3)eventData.delta;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            //if(active) return;

            if(currentGridTile != null)
            {
                currentGridTile.component = null;
                currentGridTile = null;
            }

            PointerEventData pointerEventData = new(EventSystem.current);           
            pointerEventData.position = Input.mousePosition;                            // get mouse position

            ///////(Vector3)eventData.delta

            List<RaycastResult> results = new();
            EventSystem.current.RaycastAll(pointerEventData, results);                  // raycast all on mouseposition

            foreach (RaycastResult r in results)
                if (r.gameObject.TryGetComponent<LC_GridTile>(out LC_GridTile t))       // if a gridtile is raycasted
                {
                    if(t.component != null)                                             // if gridtile already contains a component
                        Destroy(t.component.gameObject);                                // destroy that component

                    gameObject.transform.position = t.gameObject.transform.position;    // place object on gridtile
                    t.component = this;

                    return;
                }

            Destroy(gameObject);                                                        // object was dropped outside grid; destroy it
        }

        public void Activate()
        {
            //active = true;
            transform.GetChild(0).gameObject.SetActive(true);                           // activate playable object
            GetComponent<BoxCollider2D>().enabled = false;
        }

        public void Deactivate()
        {
            //active = false;
            transform.GetChild(0).transform.localPosition = Vector3.zero;               // set playable object back to original position
            transform.GetChild(0).gameObject.SetActive(false);                          // deactivate playable object
            GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}
