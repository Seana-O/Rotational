using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    private void Start()
    {
        FindObjectOfType<GravityController>().rigidbodies.Add(GetComponent<Rigidbody2D>());
    }
}
