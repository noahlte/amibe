using UnityEngine;

public class HerbivorState : BaseState
{
    protected override bool CanTarget(GameObject target)
    {
        return target.TryGetComponent(out FoodCore fc);
    }

    protected override void Eat(GameObject target)
    {
        FoodCore foodCore = target.GetComponent<FoodCore>();
        cellCore.Eat(foodCore.GetFoodToAdd());
        foodCore.DestroySelf();
    }
}
