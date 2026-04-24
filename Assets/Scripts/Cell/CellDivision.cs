using UnityEngine;

public class CellDivision : MonoBehaviour
{
    [Header("Division Info")]
    [SerializeField] private float minTimeBeforeDivision = 10f;
    [SerializeField] private float maxTimeBeforeDivision = 20f;
    [SerializeField] private float hungerForDivision = 50f;
    private float timer;

    private CellCore cellCore;
    private CellRenderer cellRenderer;
    private CellManager cellManager;

    private void Awake()
    {
        cellCore = gameObject.GetComponent<CellCore>();
        cellRenderer = gameObject.GetComponent<CellRenderer>();
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

        cellManager.SpawnCell(firstChildPosition, childHunger);
        cellManager.SpawnCell(secondChildPosition, childHunger);
        Destroy(gameObject);
    }
}
