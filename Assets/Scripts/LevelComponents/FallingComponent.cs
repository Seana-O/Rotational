using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class FallingComponent : LevelComponent
{
    GravityController gravityController;
    public bool IsFalling {get; private set;} = false;
    public GridTile currentGridTile;

    private void Start()
    {
        gravityController = FindObjectOfType<GravityController>();
        gravityController.fallingComponents.Add(this);
    }

    private void Update()
    {
        if (IsFalling)
        {
            RaycastHit2D hit = ShootRay();

            if(hit.collider != null)                                                                // if anything was hit
            {
                if(hit.collider.TryGetComponent<LevelComponent>(out LevelComponent c))              // if the hit object is a levelcomponent
                    HandleComponentCollision(c);                                                    // handle collision between components
                else                                                                                // if any other object was hit
                    StopFalling();                                                                  // stop falling
            }
        }
    }

    private void HandleComponentCollision(LevelComponent c)
    {
        if (type == ComponentType.Player && (c.type == ComponentType.Spike || c.type == ComponentType.SpikeBox) // if player hit spike
         || (type == ComponentType.SpikeBox && c.type == ComponentType.Player))                                 // OR if spikebox hit player
            FindObjectOfType<GameController>().LevelFailed();                                                   // level failed

        else if(c.TryGetComponent<FallingComponent>(out FallingComponent f))                        // if player hit falling component
        {
            if(!f.IsFalling)                                                                        // that has stopped falling
                StopFalling();                                                                      // stop falling
        }

        else                                                                                        // if player hit someting else
            StopFalling();                                                                          // stop falling
    }

    public void TryStartFalling()
    {
        RaycastHit2D hit = ShootRay(true);                                                          // shoot ray upwards
        if(hit.collider == null || !hit.collider.CompareTag("StickySurface"))                       // if player not hanging from sticky surface
            StartFalling();                                                                         // start falling
    }

    private void StartFalling()
    {
        IsFalling = true;

        GetComponent<Rigidbody2D>().constraints &= ~RigidbodyConstraints2D.FreezePositionX;
        GetComponent<Rigidbody2D>().constraints &= ~RigidbodyConstraints2D.FreezePositionY;
    }

    private void StopFalling()
    {
        if(type == ComponentType.Player && currentGridTile.isEndGridTile)                           // if player reached end
            FindObjectOfType<GameController>().FinishLevel();                                       // level completed

        IsFalling = false;
        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        gameObject.transform.position = currentGridTile.transform.position;
    }

    RaycastHit2D ShootRay(bool up = false)
    {
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");                                 // ignore object for raycasting

        Vector2 gravityDir = gravityController.gravityDirection.normalized;                         // set ray direction to gravity direction
        Vector3[] v = new Vector3[4];
        GetComponent<RectTransform>().GetWorldCorners(v);                                           // get world space position of this object's corners
        float size = Mathf.Abs(v[2].x - v[0].x);                                                    // get object size (width and height should be the same)

        RaycastHit2D hit;
        if(up) hit = Physics2D.Raycast(transform.position, -gravityDir, size/2 + 1);                // shoot ray upwards
        else   hit = Physics2D.Raycast(transform.position, gravityDir, size/2 + 1);                 // shoot ray downwards
        
        gameObject.layer = LayerMask.NameToLayer("Default");                                        // make object visible for raycasting

        return hit;
    }

    private void OnDestroy()
    {
        gravityController.fallingComponents.Remove(this);
    }

    private void OnDrawGizmos()
    {
        Vector2 gravityDir = gravityController.gravityDirection.normalized;
        Vector3[] v = new Vector3[4];
        GetComponent<RectTransform>().GetWorldCorners(v);
        float size = Mathf.Abs(v[2].x - v[0].x);

        Gizmos.DrawRay(transform.position, gravityDir * (size/2+1));
        Gizmos.DrawSphere(transform.position, 10);

        Gizmos.DrawRay(Vector3.zero, gravityDir);
    }
}