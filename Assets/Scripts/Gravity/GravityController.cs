using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static LevelRotater;

public class GravityController : MonoBehaviour
{
    public List<FallingComponent> fallingComponents = new();
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

        if (waitingForRotation && !levelRotater.Rotating)                   // if levelrotater just stopped rotating
        {
            foreach(FallingComponent f in fallingComponents)                // drop all objects
                f.TryStartFalling();

            waitingForRotation = false;
        }
    }

    void TrySetGravity(Direction dir)
    {
        if(waitingForRotation) return;                                       // don't start rotating if already rotating

        foreach(FallingComponent obj in fallingComponents)                   // don't start rotating if any object is still falling
            if(obj.IsFalling) return;

        levelRotater.Rotate(dir);
        waitingForRotation = true;
    }
}
