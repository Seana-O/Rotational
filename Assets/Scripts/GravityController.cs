using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityController : MonoBehaviour
{
    public List<Rigidbody2D> rigidbodies = new List<Rigidbody2D>();

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow))
            SetGravity(GravityDirection.Up);
        else if(Input.GetKeyDown(KeyCode.DownArrow))
            SetGravity(GravityDirection.Down);
        else if(Input.GetKeyDown(KeyCode.LeftArrow))
            SetGravity(GravityDirection.Left);
        else if(Input.GetKeyDown(KeyCode.RightArrow))
            SetGravity(GravityDirection.Right);
    }

    void SetGravity(GravityDirection dir)
    {
        int gravityMultiplier = 250;

        switch (dir)
        {
            case GravityDirection.Up:
                Physics2D.gravity = new Vector2(0, gravityMultiplier);
                break;
            case GravityDirection.Down:
                Physics2D.gravity = new Vector2(0, -gravityMultiplier);
                break;
            case GravityDirection.Left:
                Physics2D.gravity = new Vector2(-gravityMultiplier, 0);
                break;
            case GravityDirection.Right:
                Physics2D.gravity = new Vector2(gravityMultiplier, 0);
                break;
        }

        foreach(Rigidbody2D rb in rigidbodies)
            rb.velocity = Vector2.zero;
    }

    enum GravityDirection
    {
        Up,
        Down,
        Left,
        Right
    }
}
