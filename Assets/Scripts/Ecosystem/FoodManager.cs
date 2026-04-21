using UnityEngine;

public class FoodManager : MonoBehaviour
{
    [Header("Food Spawner")]
    [SerializeField] private int maxFood = 100;
    [SerializeField] private float spawnRate = 1f;
    [SerializeField] private GameObject foodPrefab;

    private float timer = 0f;
    private int currentFood;

    void Start()
    {
        for (int i = 0; i < maxFood; i++)
        {
            SpawnFood();
        }
    }

    void Update()
    {
        if (currentFood < maxFood)
        {
            timer += Time.deltaTime;
            if (timer >= spawnRate)
            {
                SpawnFood();
                timer = 0f;
            }
        }

        Debug.Log(currentFood);
    }

    private void SpawnFood()
    {
        Instantiate(foodPrefab, new Vector3(Random.Range(-9f, 9f), Random.Range(-5f, 5f), 0), Quaternion.identity);
        currentFood++;
    }

    public void ChangeCurrentFood(int amount)
    {
        currentFood += amount;
    }
}
