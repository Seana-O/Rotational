using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR;

namespace LevelCreation
{
    public class LC_ComponentSpawner : MonoBehaviour, IDragHandler
    {
        GameObject componentParent;

        void Start()
        {
            componentParent = FindObjectOfType<LC_Controller>().componentParent;

            transform.GetChild(0).gameObject.SetActive(false);  // set playable object inactive
        }

        public void OnClick()
        {
            // clone this object
            GameObject clone = Instantiate(gameObject); 
            clone.transform.SetParent(transform.parent, false);
            clone.transform.localPosition = transform.localPosition;

            // transform object into level component
            transform.SetParent(componentParent.transform, false);
            gameObject.AddComponent(typeof(LC_Component));              
        }

        public void OnDrag (PointerEventData eventData) { }
    }
}

