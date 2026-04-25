using System;
using UnityEngine;

public class CellCore : MonoBehaviour
{
    public event EventHandler OnCellDestroy;

    [Header("Cell Visual")]
    [SerializeField] private float maxRadius = 0.3f;
    [SerializeField] private float minRadius = 0.2f;
    private float radius;

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

        radius = UnityEngine.Random.Range(minRadius, maxRadius);

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

    public float GetRadius()
    {
        return radius;
    }

    public void DestroySelf()
    {
        OnCellDestroy?.Invoke(this, EventArgs.Empty);
        Destroy(gameObject);
    }
}
