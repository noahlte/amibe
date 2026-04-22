using UnityEngine;

public class CellManager : MonoBehaviour
{
    [Header("Cell Spawner")]
    [SerializeField] private int baseNumberOfCell = 10;
    [SerializeField] private GameObject cell;
    private int cellCount;

    void Start()
    {
        for (int i = 0; i < baseNumberOfCell; i++)
        {
            SpawnCell();
        }
    }

    private void SpawnCell()
    {
        GameObject newCell = Instantiate(cell, new Vector3(Random.Range(-9, 9), Random.Range(-5, 5), 0), Quaternion.identity);
        newCell.AddComponent<NeutralState>();
        cellCount++;
    }

    public void ChangeCellCount(int amount)
    {
        cellCount += amount;
    }
}
