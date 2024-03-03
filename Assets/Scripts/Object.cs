using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    public (int x,  int y) position;

    private void Start()
    {
        FindObjectOfType<GravityController>().rigidbodies.Add(GetComponent<Rigidbody2D>());
    }
}
