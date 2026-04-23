using UnityEngine;

public class CellManager : MonoBehaviour
{
    [Header("Cell Spawner")]
    [SerializeField] private int baseNumberOfCell = 10;
    [SerializeField] private GameObject cell;
    private int cellCount;

    private void Start()
    {
        for (int i = 0; i < baseNumberOfCell; i++)
        {
            SpawnCell(new Vector3(Random.Range(-9, 9), Random.Range(-5, 5), 0));
        }
    }

    private void SpawnCell(Vector3 position)
    {
        GameObject newCell = Instantiate(cell, position, Quaternion.identity);
        newCell.AddComponent<NeutralState>();
        newCell.transform.parent = gameObject.transform;
        cellCount++;
        Debug.Log($"Spawn new cell : {newCell.name} | Number : {cellCount}");
    }

    public void ChangeCellCount(int amount)
    {
        cellCount += amount;
        Debug.Log($"Cell count is now {cellCount}");
    }
}
