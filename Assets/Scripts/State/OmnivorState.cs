using UnityEngine;

public class OmnivorState : BaseState, ICellState
{
    protected override bool CanTarget(GameObject target)
    {
        return target.TryGetComponent(out FoodCore fc) || target.TryGetComponent(out Prey prey);
    }

    protected override void Eat(GameObject target)
    {
        if (target.TryGetComponent(out FoodCore fc))
        {
            cellCore.Eat(fc.GetFoodToAdd());
            fc.DestroySelf();
        } else if (target.TryGetComponent(out CellCore cc))
        {
            int foodToAdd = Mathf.RoundToInt(cc.GetRadius() * cc.GetHunger() / 6);

            cellCore.Eat(foodToAdd);
            cc.DestroySelf();
        }
    }
}
