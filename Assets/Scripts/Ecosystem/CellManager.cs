using UnityEngine;

public class CellManager : MonoBehaviour
{
    [Header("Cell Spawner")]
    [SerializeField] private int baseNumberOfCell = 10;
    [SerializeField] private GameObject herbivorCellPrefab;
    [SerializeField] private GameObject predatorCellPrefab;

    private int cellCount;

    private void Start()
    {
        float cameraWidth, cameraHeight;

        (cameraWidth, cameraHeight) = Utils.GetCameraBounds();

        for (int i = 0; i < baseNumberOfCell; i++)
        {
            SpawnCell(new Vector3(Random.Range(-cameraWidth, cameraWidth), Random.Range(-cameraHeight, cameraHeight), 0));
        }
    }

    public void SpawnCell(Vector3 position, bool hasMutate = false, float hungerToSet = 0f)
    {
        GameObject newCell;

        newCell = hasMutate ? 
            Instantiate(predatorCellPrefab, position, Quaternion.identity) : 
            Instantiate(herbivorCellPrefab, position, Quaternion.identity);

        CellCore newCellCore = newCell.GetComponent<CellCore>();

        newCellCore.OnCellDestroy += CellCore_OnCellDestroy;
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

    private void CellCore_OnCellDestroy(object sender, System.EventArgs e)
    {
        ChangeCellCount(-1);
    }
}
