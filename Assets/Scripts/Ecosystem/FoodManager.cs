using UnityEngine;

public class FoodManager : MonoBehaviour
{
    [Header("Food Spawner")]
    [SerializeField] private int maxFood = 100;
    [SerializeField] private float spawnRate = 1f;
    [SerializeField] private GameObject foodPrefab;
    [SerializeField] private FoodCore foodCore;

    private float timer = 0f;
    private int currentFood;
    private float cameraWidth, cameraHeight;

    private void Start()
    {
        (cameraWidth, cameraHeight) = Utils.GetCameraBounds();
        
        for (int i = 0; i < maxFood; i++)
        {
            SpawnFood();
        }
    }

    private void FoodCore_OnFoodDestroy(object sender, System.EventArgs e)
    {
        ChangeCurrentFood(-1);
    }

    private void Update()
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
    }

    private void SpawnFood()
    {
        Vector3 foodToSpawnPosition = new Vector3(Random.Range(-cameraWidth, cameraWidth), Random.Range(-cameraHeight, cameraHeight), 0);
        GameObject food = Instantiate(foodPrefab, foodToSpawnPosition, Quaternion.identity);
        food.GetComponent<FoodCore>().OnFoodDestroy += FoodCore_OnFoodDestroy;
        food.transform.parent = gameObject.transform;
        currentFood++;
    }

    public void ChangeCurrentFood(int amount)
    {
        currentFood += amount;
        Debug.Log(currentFood);
    }
}
