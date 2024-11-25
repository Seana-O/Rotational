using UnityEngine;

public class GridTile : MonoBehaviour
{
    public bool isEndGridTile = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        FallingComponent f = collision.gameObject.GetComponent<FallingComponent>();
        if(f != null)
            f.currentGridTile = this;
    }
}
