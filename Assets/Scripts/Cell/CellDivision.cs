using UnityEngine;

public class CellDivision : MonoBehaviour
{
    [Header("Division Info")]
    [SerializeField] private float minTimeBeforeDivision = 10f;
    [SerializeField] private float maxTimeBeforeDivision = 20f;
    [SerializeField] private float hungerForDivision = 50f;
    private float timer;

    private CellCore cc;
    private CellRenderer cr;
    private CellManager cm;

    private void Awake()
    {
        cc = gameObject.GetComponent<CellCore>();
        cr = gameObject.GetComponent<CellRenderer>();
    }

    private void Start()
    {
        cm = FindAnyObjectByType<CellManager>();
        timer = Random.Range(minTimeBeforeDivision, maxTimeBeforeDivision);
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0 && cc.GetHunger() > hungerForDivision)
        {
            DivideCell();
        }
        else if (timer <= 0 && cc.GetHunger() < hungerForDivision)
        {
            timer = Random.Range(minTimeBeforeDivision, maxTimeBeforeDivision);
        }
    }

    private void DivideCell()
    {
        Vector3 firstChildPosition = transform.position + new Vector3(cr.GetRadius(), 0, 0);
        Vector3 secondChildPosition = transform.position - new Vector3(cr.GetRadius(), 0, 0);

        float childHunger = cc.GetHunger() / 2;

        cm.SpawnCell(firstChildPosition, childHunger);
        cm.SpawnCell(secondChildPosition, childHunger);
        Destroy(gameObject);
    }
}
