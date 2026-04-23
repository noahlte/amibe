using UnityEngine;

public class CellCore : MonoBehaviour
{
    [Header("Cell Size")]
    [SerializeField] public float maxRadius = 0.3f;
    [SerializeField] public float minRadius = 0.2f;

    [Header("Cell Life")]
    [SerializeField] private float hungerLossPerSecond = 1f;
    [SerializeField] private float maxHunger = 100f;
    private float hunger;

    [Header("Cell Seeking")]
    [SerializeField] private float seekRadius = 0.6f;
    [SerializeField] private CircleCollider2D seekRadiusTrigger;

    [Header("Cell Collision")]
    [SerializeField] private CircleCollider2D collision;

    private bool isDead;

    private void Start()
    {
        collision.radius = minRadius;
        seekRadiusTrigger.radius = seekRadius;

        hunger = maxHunger;
    }

    private void Update()
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
        if (hunger <= 0) isDead = true;
    }

    public void Eat(int amount)
    {
        hunger += amount;
    }

    public float GetHunger()
    {
        return hunger;
    }

    public float GetMaxHunger()
    {
        return maxHunger;
    }

    public void SetHunger(float amount)
    {
        hunger = amount;
    }
}
