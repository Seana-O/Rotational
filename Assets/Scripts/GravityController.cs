using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityController : MonoBehaviour
{
    public List<Object> objects = new List<Object>();
    public Vector2 gravityDirection = Vector2.zero;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            TrySetGravity(GravityDirection.Up);
        else if(Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            TrySetGravity(GravityDirection.Down);
        else if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            TrySetGravity(GravityDirection.Left);
        else if(Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            TrySetGravity(GravityDirection.Right);

        Physics2D.gravity = gravityDirection;
    }

    void TrySetGravity(GravityDirection dir)
    {
        foreach(Object obj in objects)
            if(obj.moving) return;

        SetGravity(dir);
    }

    void SetGravity(GravityDirection dir)
    {
        int gravityMultiplier = 250;
        bool horizontalGravity = false;

        switch (dir)
        {
            case GravityDirection.Up:
                gravityDirection = new Vector2(0, gravityMultiplier);
                break;
            case GravityDirection.Down:
                gravityDirection = new Vector2(0, -gravityMultiplier);
                break;
            case GravityDirection.Left:
                gravityDirection = new Vector2(-gravityMultiplier, 0);
                horizontalGravity = true;
                break;
            case GravityDirection.Right:
                gravityDirection = new Vector2(gravityMultiplier, 0);
                horizontalGravity = true;
                break;
        }
        
        Physics2D.gravity = gravityDirection;

        foreach(Object obj in objects)
        {
            obj.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            obj.moving = true;

            if(horizontalGravity)
                obj.GetComponent<Rigidbody2D>().constraints &= ~RigidbodyConstraints2D.FreezePositionX;
            else
                obj.GetComponent<Rigidbody2D>().constraints &= ~RigidbodyConstraints2D.FreezePositionY;
        }
    }

    enum GravityDirection
    {
        Up,
        Down,
        Left,
        Right
    }
}
