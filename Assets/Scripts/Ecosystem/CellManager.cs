using UnityEngine;

public class CellManager : MonoBehaviour
{
    [Header("Cell Base Spawner")]
    [SerializeField] private int baseNumberOfCell = 10;
    [SerializeField] private GameObject herbivorCellPrefab;
    [SerializeField] private GameObject predatorCellPrefab;

    [Header("Cell Interface Spawner")]
    [SerializeField] private float timeBeforeSpawn = 10f;
    private float interfaceSpawnTimer = 0f;

    private int cellCount;

    private float cameraWidth, cameraHeight;

    private void Start()
    {
        (cameraWidth, cameraHeight) = Utils.GetCameraBounds();

        for (int i = 0; i < baseNumberOfCell; i++)
        {
            SpawnCell(new Vector3(Random.Range(-cameraWidth, cameraWidth), Random.Range(-cameraHeight, cameraHeight), 0));
        }
    }

    private void Update()
    {
        if (SerialReceiver.Instance.IsTriggerSpawnButton())
        {
            interfaceSpawnTimer += Time.deltaTime;
        }

        if (interfaceSpawnTimer > timeBeforeSpawn)
        {
            Vector3 spawnPosition = new Vector3(0, -cameraHeight, 0);
            SpawnCell(spawnPosition);
            interfaceSpawnTimer = 0f;
        }
    }

    public void SpawnCell(Vector3 position, bool hasMutate = false, float hungerToSet = 0f)
    {
        GameObject newCell;

        newCell = hasMutate ? 
            Instantiate(predatorCellPrefab, position, Quaternion.identity) : 
            Instantiate(herbivorCellPrefab, position, Quaternion.identity);

        CellCore newCellCore = newCell.GetComponent<CellCore>();

        newCellCore.OnCellDestroy += Cell_OnCellDestroy;
        newCell.GetComponent<CellDivision>().OnCellDivide += Cell_OnCellDivide;

        newCell.transform.parent = gameObject.transform;

        if (hungerToSet != 0)
        {
            newCellCore.SetHunger(hungerToSet);
        }

        ChangeCellCount(1);
    }

    public void ChangeCellCount(int amount)
    {
        cellCount += amount;
        Debug.Log($"Cell count is now {cellCount}");
    }

    private void Cell_OnCellDivide(object sender, CellDivision.OnCellDivideArgs e)
    {
        SpawnCell(e.firstChildPosition, e.doesFirstChildMutate, e.childHunger);
        SpawnCell(e.secondChildPosition, e.doesSecondChildMutate, e.childHunger);
    }

    private void Cell_OnCellDestroy(object sender, System.EventArgs e)
    {
        ChangeCellCount(-1);
    }
}
