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
        if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            TrySetGravity(Direction.Left);
        else if(Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            TrySetGravity(Direction.Right);

        if (waitingForRotation && !levelRotater.Rotating)
        {
            foreach(Object obj in objects)
                obj.StartMoving();

            waitingForRotation = false;
        }
    }

    void TrySetGravity(Direction dir)
    {
        if(waitingForRotation) return;

        foreach(Object obj in objects)
            if(obj.moving) return;

        levelRotater.Rotate(dir);

        waitingForRotation = true;
    }
}
