using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Object : MonoBehaviour
{
    GravityController gravityController;
    public bool moving = false;
    public GridPoint currentGridPoint;

    private void Start()
    {
        gravityController = FindObjectOfType<GravityController>();
        gravityController.objects.Add(this);
    }

    private void Update()
    {
        if (moving)
        {
            gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
            Vector2 gravityDir = gravityController.gravityDirection.normalized;
            Vector3[] v = new Vector3[4];
            GetComponent<RectTransform>().GetWorldCorners(v);

            float size = Mathf.Abs(v[2].x - v[0].x); // width and height should be the same

            RaycastHit2D hit = Physics2D.Raycast(transform.position, gravityDir, size/2 + 1);

            if(hit.collider != null /*&& (hit.collider.CompareTag("ClosedTile") || hit.collider.CompareTag("Box") || hit.collider.CompareTag("Player")*/)
            {
                if (IsPlayer() && (ColliderHasTag(hit, "Spike") || ColliderHasTag(hit, "SpikeBox"))
                    || gameObject.CompareTag("SpikeBox") && ColliderHasTag(hit, "Player")) // if player hit spike
                    FindObjectOfType<GameController>().LevelFailed();
                else if(IsPlayer() && currentGridPoint.isEndGridPoint) // if player reached end
                    FindObjectOfType<GameController>().FinishLevel();
                else if(ColliderHasTag(hit, "Box") || ColliderHasTag(hit, "Player") || ColliderHasTag(hit, "SpikeBox"))
                {
                    if(!hit.collider.gameObject.GetComponent<Object>().moving)
                        StopMoving();
                }
                else
                    StopMoving();
            }
            gameObject.layer = LayerMask.NameToLayer("Default");
        }
    }

    private void StopMoving()
    {
        moving = false;
        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        gameObject.transform.position = currentGridPoint.transform.position;
    }

    private void OnDrawGizmos()
    {
        Vector2 gravityDir = gravityController.gravityDirection.normalized;
        Vector3[] v = new Vector3[4];
        GetComponent<RectTransform>().GetWorldCorners(v);

        float size = Mathf.Abs(v[2].x - v[0].x); // width and height should be the same

        Gizmos.DrawRay(transform.position, gravityDir * (size/2+1));
        Gizmos.DrawSphere(transform.position, 10);

        Gizmos.DrawRay(Vector3.zero, gravityDir);
    }

    private bool IsPlayer()
    {
        return gameObject.CompareTag("Player");
    }

    public void StartMoving()
    {
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        Vector2 gravityDir = gravityController.gravityDirection.normalized;
        Vector3[] v = new Vector3[4];
        GetComponent<RectTransform>().GetWorldCorners(v);

        float size = Mathf.Abs(v[2].x - v[0].x); // width and height should be the same

        RaycastHit2D hit = Physics2D.Raycast(transform.position, -gravityDir, size/2 + 1);

        if(hit.collider != null && ColliderHasTag(hit, "StickySurface"))
        {
            gameObject.layer = LayerMask.NameToLayer("Default");
            return;
        }

        moving = true;

        GetComponent<Rigidbody2D>().constraints &= ~RigidbodyConstraints2D.FreezePositionX;
        GetComponent<Rigidbody2D>().constraints &= ~RigidbodyConstraints2D.FreezePositionY;
    }

    bool ColliderHasTag(RaycastHit2D hit, string tag)
    {
        return hit.collider.gameObject.CompareTag(tag);
    }
}
