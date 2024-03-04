using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Object : MonoBehaviour
{
    GravityController gravityController;
    public bool moving = false;

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
            GetComponent<Image>().raycastTarget = false;
            Vector2 gravityDir = gravityController.gravityDirection.normalized;
            //Vector2 rayCastPos = transformPos + (gameObject.GetComponent<RectTransform>().sizeDelta.x/2) * gravityDir;
            Debug.Log(transform.position);
            Debug.Log(transform.TransformPoint(transform.localPosition));
            Vector3[] v = new Vector3[4];
            GetComponent<RectTransform>().GetWorldCorners(v);

            float size = v[2].x - v[0].x; // width and height should be the same

            RaycastHit2D hit = Physics2D.Raycast(transform.position, gravityDir, size/2 + 1);

            Debug.Log(" ");
            Debug.Log(v[0]);
            Debug.Log(v[1]);
            Debug.Log(v[2]);
            Debug.Log(v[3]);
            if(hit.collider != null /*&& (hit.collider.CompareTag("ClosedTile") || hit.collider.CompareTag("Box") || hit.collider.CompareTag("Player")*/)
            {
                moving = false;
                gameObject.GetComponent<Rigidbody2D>().constraints &= RigidbodyConstraints2D.FreezePosition;
            }
            gameObject.layer = LayerMask.NameToLayer("Default");
            GetComponent<Image>().raycastTarget = true;
        }
    }
    private void OnDrawGizmos()
    {
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        GetComponent<Image>().raycastTarget = false;
        Vector2 gravityDir = gravityController.gravityDirection.normalized;
        //Vector2 rayCastPos = transformPos + (gameObject.GetComponent<RectTransform>().sizeDelta.x/2) * gravityDir;
        Debug.Log(transform.position);
        Debug.Log(transform.TransformPoint(transform.localPosition));
        Vector3[] v = new Vector3[4];
        GetComponent<RectTransform>().GetWorldCorners(v);

        float size = v[2].x - v[0].x; // width and height should be the same

        Gizmos.DrawRay(transform.position, gravityDir * (size/2+1));
    }
}
