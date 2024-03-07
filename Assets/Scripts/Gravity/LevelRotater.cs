using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class LevelRotater : MonoBehaviour
{
    public bool Rotating { get; private set; } = false;

    readonly int rotateSpeed = 80;

    float destination;
    Direction rotateDirection;
    float rotation = 0;

    void Update()
    {
        if (Rotating)
        {
            if(rotateDirection == Direction.Left)
            {
                transform.RotateAround(transform.position, Vector3.forward,  rotateSpeed * Time.deltaTime);
                rotation += rotateSpeed * Time.deltaTime;
                if(rotation >= 90)
                    StopRotation();
            }
            else
            {
                transform.RotateAround(transform.position, Vector3.forward,  -rotateSpeed * Time.deltaTime);
                rotation -= rotateSpeed * Time.deltaTime;
                if(rotation <= -90)
                    StopRotation();
            }
        }
    }

    public void Rotate(Direction direction)
    {
        if(direction == Direction.Left)
        {
            destination = transform.localEulerAngles.z + 90;
            if(destination >= 360)
                destination -= 360;
        }
        else
        {
            destination = transform.localEulerAngles.z - 90;
            if(destination < 0)
                destination += 360;
        }
        
        rotateDirection = direction;
        rotation = 0;
        Rotating = true;
    }

    void StopRotation()
    {
        Vector3 rot = transform.localEulerAngles;
        rot.z = destination;
        transform.localEulerAngles = rot;
        Rotating = false;
    }

    public enum Direction
    {
        Left,
        Right
    }
}
