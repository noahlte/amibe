using UnityEngine;

public class FoodCore : MonoBehaviour
{
    [Header("Food Info")]
    [SerializeField] private int foodToAdd = 2;

    public int GetFoodToAdd()
    {
        return foodToAdd;
    }
}
