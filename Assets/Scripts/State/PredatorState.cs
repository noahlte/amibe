using System;
using UnityEngine;

public class PredatorState : BaseState
{
    [Header("Predator Info")]
    [SerializeField] private float hungerToEat = 50f;
    [SerializeField] private float foodToAddDivider = 6f;
    private CellManager cm;

    protected override void Start()
    {
        base.Start();
        cm = FindAnyObjectByType<CellManager>();
    }

    protected override bool CanTarget(GameObject target)
    {
        return target.TryGetComponent(out Prey prey) && cellCore.GetHunger() < hungerToEat;
    }

    protected override void Eat(GameObject target)
    {
        CellCore preyCore = target.GetComponent<CellCore>();
        int foodToAdd = Mathf.RoundToInt(preyCore.GetRadius() * preyCore.GetHunger() / foodToAddDivider);

        cellCore.Eat(foodToAdd);
        Destroy(target);
        cm.ChangeCellCount(-1);
    }
}
