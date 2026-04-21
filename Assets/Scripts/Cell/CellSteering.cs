using UnityEngine;

public class CellSteering : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float topSpeed = 0.2f;
    [SerializeField] private float xIncrement = 0.2f;
    [SerializeField] private float yIncrement = 0.1f;

    // Vector variable
    private Vector3 velocity;
    private Vector3 acceleration;

    private float xoff1;
    private float yoff1;
    private float xoff2;
    private float yoff2;

    void Start()
    {
        xoff1 = Random.Range(0, 10000);
        yoff1 = Random.Range(0, 10000);

        xoff2 = Random.Range(0, 10000);
        yoff2 = Random.Range(0, 10000);

        velocity = new Vector3(0, 0, 0);
        acceleration = new Vector3(0, 0, 0);
    }

    void Update()
    {
        CheckEdges();

        float xDirection = Mathf.PerlinNoise(xoff1, yoff1);
        float yDirection = Mathf.PerlinNoise(xoff2, yoff2);

        xDirection = Utils.Map(xDirection, 0f, 1f, -1f, 1f);
        yDirection = Utils.Map(yDirection, 0f, 1f, -1f, 1f);

        acceleration = new Vector3(xDirection, yDirection, 0);
        velocity += acceleration;
        
        if (velocity.magnitude > topSpeed)
        {
            velocity.Normalize();
            velocity *= topSpeed;
        }

        transform.position += velocity * Time.deltaTime;

        (xoff1, yoff1) = IncrementOffset(xoff1, yoff1);
        (xoff2, yoff2) = IncrementOffset(xoff2, yoff2);
    }

    private void CheckEdges()
    {
        if (transform.position.x > 9f) transform.position = new Vector3(-9f, transform.position.y, 0);
        if (transform.position.x < -9f) transform.position = new Vector3(9f, transform.position.y, 0);
        if (transform.position.y > 5f) transform.position = new Vector3(transform.position.x, -5f, 0);
        if (transform.position.y < -5f) transform.position = new Vector3(transform.position.x, 5f, 0);
    }

    private (float, float) IncrementOffset(float xoff, float yoff)
    {
        xoff += xIncrement;
        yoff += yIncrement;

        return (xoff, yoff);
    }
}
