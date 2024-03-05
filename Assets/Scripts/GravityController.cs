using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static LevelRotater;

public class GravityController : MonoBehaviour
{
    public List<Object> objects = new();
    public Vector2 gravityDirection = Vector2.down;
    public bool waitingForRotation = false;
    LevelRotater levelRotater;

    private void Start()
    {
        levelRotater = FindObjectOfType<LevelRotater>();
        gravityDirection = new Vector2(0, -500);
        Physics2D.gravity = gravityDirection;
    }

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

        if (waitingForRotation && !levelRotater.Rotating)
        {
            foreach(Object obj in objects)
            {
                obj.moving = true;

                obj.GetComponent<Rigidbody2D>().constraints &= ~RigidbodyConstraints2D.FreezePositionX;
                obj.GetComponent<Rigidbody2D>().constraints &= ~RigidbodyConstraints2D.FreezePositionY;
            }

            waitingForRotation = false;
        }
    }

    void TrySetGravity(GravityDirection dir)
    {
        if(waitingForRotation) return;

        foreach(Object obj in objects)
            if(obj.moving) return;

        SetGravity(dir);
    }

    void SetGravity(GravityDirection dir)
    {
        int gravityMultiplier = 250;
        bool horizontalGravity = false;
        /*
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
            obj.moving = true;

            if(horizontalGravity)
                obj.GetComponent<Rigidbody2D>().constraints &= ~RigidbodyConstraints2D.FreezePositionX;
            else
                obj.GetComponent<Rigidbody2D>().constraints &= ~RigidbodyConstraints2D.FreezePositionY;
        }
        */
        switch (dir)
        {
            case GravityDirection.Up:
                break;
            case GravityDirection.Down:
                break;
            case GravityDirection.Left:
                levelRotater.Rotate(Direction.Left);
                break;
            case GravityDirection.Right:
                levelRotater.Rotate(Direction.Right);
                break;
        }

       
        waitingForRotation = true;
    }

    enum GravityDirection
    {
        Up,
        Down,
        Left,
        Right
    }
}
