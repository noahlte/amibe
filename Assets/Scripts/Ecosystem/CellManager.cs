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

        if (hasMutate)
        {
            newCell = Instantiate(predatorCellPrefab, position, Quaternion.identity);
        }
        else
        {
            newCell = Instantiate(herbivorCellPrefab, position, Quaternion.identity);
        }

        newCell.transform.parent = gameObject.transform;

        if (hungerToSet != 0)
        {
            newCell.GetComponent<CellCore>().SetHunger(hungerToSet);
        }

        cellCount++;
    }

    public void ChangeCellCount(int amount)
    {
        cellCount += amount;
        Debug.Log($"Cell count is now {cellCount}");
    }
}
