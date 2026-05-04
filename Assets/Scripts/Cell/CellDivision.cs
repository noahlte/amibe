using System;
using UnityEngine;

public class CellDivision : MonoBehaviour
{
    public event EventHandler<OnCellDivideArgs> OnCellDivide;
    public class OnCellDivideArgs : EventArgs
    {
        public Vector3 firstChildPosition;
        public Vector3 secondChildPosition;
        public bool doesFirstChildMutate;
        public bool doesSecondChildMutate;
        public float childHunger;
    }

    [Header("Division Info")]
    [SerializeField] private float minTimeBeforeDivision = 10f;
    [SerializeField] private float maxTimeBeforeDivision = 20f;
    [SerializeField] private float hungerForDivision = 50f;
    [SerializeField] private float percentageOfMutation = 1f;

    private float timer;
    private bool isPredator;

    private CellCore cellCore;

    private void Awake()
    {
        cellCore = gameObject.GetComponent<CellCore>();
        isPredator = gameObject.TryGetComponent(out PredatorState predatorState);
    }

    private void Start()
    {
        timer = UnityEngine.Random.Range(minTimeBeforeDivision, maxTimeBeforeDivision);
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0 && cellCore.GetHunger() > hungerForDivision)
        {
            DivideCell();
            timer = UnityEngine.Random.Range(minTimeBeforeDivision, maxTimeBeforeDivision);
        }
        else if (timer <= 0 && cellCore.GetHunger() < hungerForDivision)
        {
            timer = UnityEngine.Random.Range(minTimeBeforeDivision, maxTimeBeforeDivision);
        }
    }

    private void DivideCell()
    {
        Vector3 firstChildPosition = transform.position + new Vector3(cellCore.GetRadius(), 0, 0);
        Vector3 secondChildPosition = transform.position - new Vector3(cellCore.GetRadius(), 0, 0);

        float childHunger = cellCore.GetHunger() / 2;

        bool doesFirstChildMutate = isPredator || DoesMutate();
        bool doesSecondChildMutate = isPredator || DoesMutate();

        cellCore.DestroySelf();

        OnCellDivide?.Invoke(this, new OnCellDivideArgs
        {
            firstChildPosition = firstChildPosition,
            secondChildPosition = secondChildPosition,
            childHunger = childHunger,
            doesFirstChildMutate = doesFirstChildMutate,
            doesSecondChildMutate = doesSecondChildMutate
        });
    }

    private bool DoesMutate()
    {
        float rand = UnityEngine.Random.Range(0, 100);
        return rand < percentageOfMutation;
    }
}
