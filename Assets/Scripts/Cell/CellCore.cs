using UnityEngine;

public class CellCore : MonoBehaviour
{
    [Header("Cell Life")]
    [SerializeField] private float hunger = 100f;
    [SerializeField] private float hungerLossPerSecond = 1f;

    [Header("Cell Seeking")]
    [SerializeField] private float seekRadius = 0.6f;
    [SerializeField] private CircleCollider2D seekRadiusTrigger;

    [Header("Cell Collision")]
    [SerializeField] private CircleCollider2D collision;

    private CellRenderer cellRenderer;
    private bool isDead;

    void Start()
    {
        cellRenderer = gameObject.GetComponent<CellRenderer>();

        collision.radius = cellRenderer.GetMinRadius();
        seekRadiusTrigger.radius = seekRadius;
    }

    void Update()
    {   
        HungerLoss();

        if (isDead)
        {
            Destroy(gameObject);
        }
    }

    private void HungerLoss()
    {
        hunger -= hungerLossPerSecond * Time.deltaTime;
        if (hunger <= 0)
        {
            isDead = true;
        }
    }

    public void Eat(int amount)
    {
        hunger += amount;
    }
}
