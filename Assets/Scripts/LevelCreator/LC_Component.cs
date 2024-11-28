using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace LevelCreation
{
    public class LC_Component : MonoBehaviour, IDragHandler, IEndDragHandler
    {
        protected LC_GridTile currentGridTile;

        protected virtual void Start()
        {
            // remove ComponentSpawner properties from this object
            if(TryGetComponent<LC_ComponentSpawner>(out LC_ComponentSpawner spawner))
                Destroy(spawner);
            if(TryGetComponent<EventTrigger>(out EventTrigger trigger))
                Destroy(trigger);
        }

        public virtual void OnDrag (PointerEventData eventData)
        {
            this.transform.position += (Vector3)eventData.delta;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if(currentGridTile != null)
                RemoveFromCurrentTile();

            List<RaycastResult> results = new();
            EventSystem.current.RaycastAll(eventData, results);                         // raycast all on mouseposition

            foreach (RaycastResult r in results)
                if (r.gameObject.TryGetComponent<LC_GridTile>(out LC_GridTile t))       // if a gridtile is raycasted
                {
                    Place(t);
                    currentGridTile = t;

                    FindObjectOfType<LC_Controller>().CheckValidity();                  // tell controller to check level validity

                    return;
                }

            FindObjectOfType<LC_Controller>().CheckValidity();                          // tell controller to check level validity
            Destroy(gameObject);                                                        // object was dropped outside grid; destroy it
        }

        public virtual void RemoveFromCurrentTile()
        {
            currentGridTile.component = null;
            currentGridTile = null;
        }

        protected virtual void Place(LC_GridTile t)
        {   
            if(t.component != null)                                                     // if gridtile already contains a component
                Destroy(t.component.gameObject);                                        // destroy that component

            gameObject.transform.position = t.gameObject.transform.position;            // place object on gridtile
            t.component = this;

        }

        public void Activate()
        {
            transform.GetChild(0).gameObject.SetActive(true);                           // activate playable object

            GetComponent<BoxCollider2D>().enabled = false;                              // deactivate component functionality
            GetComponent<Image>().enabled = false;                                      // deactivate component visibility
            Transform images = transform.Find("Images");
            if(images != null) images.gameObject.SetActive(false);
        }

        public void Deactivate()
        {
            transform.GetChild(0).transform.localPosition = Vector3.zero;               // set object back to original position
            transform.GetChild(0).gameObject.SetActive(false);                          // deactivate playable object

            GetComponent<BoxCollider2D>().enabled = true;                               // activate component functionality
            GetComponent<Image>().enabled = true;                                       // activate component visibility
            Transform images = transform.Find("Images");
            if(images != null) images.gameObject.SetActive(true);
        }
    }
}
