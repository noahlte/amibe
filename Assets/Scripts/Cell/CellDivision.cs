using NUnit.Framework;
using UnityEngine;

public class CellDivision : MonoBehaviour
{
    [Header("Division Info")]
    [SerializeField] private float minTimeBeforeDivision = 10f;
    [SerializeField] private float maxTimeBeforeDivision = 20f;
    [SerializeField] private float hungerForDivision = 50f;
    [SerializeField] private float percentageOfMutation = 1f;
    private float timer;
    private bool isPredator;

    private CellCore cellCore;
    private CellRenderer cellRenderer;
    private CellManager cellManager;

    private void Awake()
    {
        cellCore = gameObject.GetComponent<CellCore>();
        cellRenderer = gameObject.GetComponent<CellRenderer>();
        isPredator = gameObject.TryGetComponent(out PredatorState predatorState);
    }

    private void Start()
    {
        cellManager = FindAnyObjectByType<CellManager>();
        timer = Random.Range(minTimeBeforeDivision, maxTimeBeforeDivision);
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0 && cellCore.GetHunger() > hungerForDivision)
        {
            DivideCell();
        }
        else if (timer <= 0 && cellCore.GetHunger() < hungerForDivision)
        {
            timer = Random.Range(minTimeBeforeDivision, maxTimeBeforeDivision);
        }
    }

    private void DivideCell()
    {
        Vector3 firstChildPosition = transform.position + new Vector3(cellRenderer.GetRadius(), 0, 0);
        Vector3 secondChildPosition = transform.position - new Vector3(cellRenderer.GetRadius(), 0, 0);

        float childHunger = cellCore.GetHunger() / 2;

        bool doesFirstChildMutate = isPredator || DoesMutate();
        bool doesSecondChildMutate = isPredator || DoesMutate();

        cellManager.SpawnCell(firstChildPosition, doesFirstChildMutate, childHunger);
        cellManager.SpawnCell(secondChildPosition, doesSecondChildMutate, childHunger);
        Destroy(gameObject);
    }

    private bool DoesMutate()
    {
        float rand = Random.Range(0, 100);
        return rand < percentageOfMutation;
    }
}
