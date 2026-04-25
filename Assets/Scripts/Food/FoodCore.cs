using System;
using UnityEngine;

public class FoodCore : MonoBehaviour
{
    public event EventHandler OnFoodDestroy;

    [Header("Food Info")]
    [SerializeField] private int foodToAdd = 2;

    public int GetFoodToAdd()
    {
        return foodToAdd;
    }

    public void DestroySelf()
    {
        OnFoodDestroy?.Invoke(this, EventArgs.Empty);
        Destroy(gameObject);
    }
}
