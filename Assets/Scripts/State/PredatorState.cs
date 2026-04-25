using System;
using UnityEngine;

public class PredatorState : BaseState
{
    [Header("Predator Info")]
    [SerializeField] private float hungerToEat = 50f;
    [SerializeField] private float foodToAddDivider = 6f;

    protected override bool CanTarget(GameObject target)
    {
        return target.TryGetComponent(out Prey prey) && cellCore.GetHunger() < hungerToEat;
    }

    protected override void Eat(GameObject target)
    {
        CellCore preyCore = target.GetComponent<CellCore>();
        int foodToAdd = Mathf.RoundToInt(preyCore.GetRadius() * preyCore.GetHunger() / foodToAddDivider);

        cellCore.Eat(foodToAdd);
        preyCore.DestroySelf();
    }
}
