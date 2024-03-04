using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Object obj = collision.gameObject.GetComponent<Object>();
        if(obj != null)
            obj.currentGridPoint = this;
    }
}
