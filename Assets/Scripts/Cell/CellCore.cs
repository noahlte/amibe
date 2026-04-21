using UnityEngine;

public class CellCore : MonoBehaviour
{
    private CircleCollider2D collision;
    private CellRenderer cellRenderer;

    void Start()
    {
        collision = gameObject.GetComponent<CircleCollider2D>();
        cellRenderer = gameObject.GetComponent<CellRenderer>();

        collision.radius = cellRenderer.GetMinRadius();
    }
}
