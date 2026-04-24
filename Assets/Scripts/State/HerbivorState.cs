using UnityEngine;

public class HerbivorState : BaseState
{
    protected override void Start()
    {
        base.Start();
        gameObject.AddComponent<Prey>();
    }

    protected override bool CanTarget(GameObject target)
    {
        return target.TryGetComponent(out FoodCore fc);
    }

    protected override void Eat(GameObject target)
    {
        cc.Eat(target.GetComponent<FoodCore>().GetFoodToAdd());
        Destroy(target);
        fm.ChangeCurrentFood(-1);
    }
}
