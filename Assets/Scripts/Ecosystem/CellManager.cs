using UnityEngine;

public class CellManager : MonoBehaviour
{
    [Header("Cell Spawner")]
    [SerializeField] private int baseNumberOfCell = 10;
    [SerializeField] private GameObject cell;
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

    public void SpawnCell(Vector3 position, float hungerToSet = 0f)
    {
        GameObject newCell = Instantiate(cell, position, Quaternion.identity);
        newCell.AddComponent<HerbivorState>();
        newCell.transform.parent = gameObject.transform;

        if (hungerToSet != 0)
        {
            newCell.GetComponent<CellCore>().SetHunger(hungerToSet);
        }

        cellCount++;
        Debug.Log($"Spawn new cell : {newCell.name} | Number : {cellCount}");
    }

    public void ChangeCellCount(int amount)
    {
        cellCount += amount;
        Debug.Log($"Cell count is now {cellCount}");
    }
}
