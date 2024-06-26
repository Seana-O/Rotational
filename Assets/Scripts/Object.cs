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
                if (gameObject.CompareTag("Player") && hit.collider.gameObject.CompareTag("Spike")) // if player hit spike
                    FindObjectOfType<GameController>().LevelFailed();
                else if(gameObject.CompareTag("Player") && currentGridPoint.isEndGridPoint) // if player reached end
                    FindObjectOfType<GameController>().FinishLevel();
                else if(hit.collider.gameObject.CompareTag("Box") || hit.collider.gameObject.CompareTag("Player"))
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
}
