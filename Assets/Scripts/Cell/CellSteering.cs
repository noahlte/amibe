using UnityEngine;

public class CellSteering : MonoBehaviour
{
    private float cameraHeight;
    private float cameraWidth;

    [Header("Movement")]
    [SerializeField] private float topSpeed = 0.2f;
    [SerializeField] private float xIncrement = 0.2f;
    [SerializeField] private float yIncrement = 0.1f;

    private Vector3 velocity;
    private Vector3 acceleration;

    private ICellState cellState;

    private float xoff1;
    private float yoff1;
    private float xoff2;
    private float yoff2;

    private enum MovementState
    {
        Seeking,
        Eating
    }

    private MovementState movementState;

    private Vector3 targetPosition;

    private void Awake()
    {
        cellState = gameObject.GetComponent<ICellState>();
    }

    private void Start()
    {
        xoff1 = Random.Range(0, 10000);
        yoff1 = Random.Range(0, 10000);

        xoff2 = Random.Range(0, 10000);
        yoff2 = Random.Range(0, 10000);

        velocity = Vector3.zero;
        acceleration = Vector3.zero;

        (cameraWidth, cameraHeight) = Utils.GetCameraBounds();

        cellState.OnTargetFound += CellState_OnTargetFound;
        cellState.OnTargetLost += CellState_OnTargetLost;
        cellState.OnTargetEat += CellState_OnTargetEat;

        movementState = MovementState.Seeking;
    }

    private void CellState_OnTargetFound(object sender, BaseState.OnTargetFoundArgs e)
    {
        movementState = MovementState.Eating;
        targetPosition = e.targetPosition;
    }
    private void CellState_OnTargetLost(object sender, System.EventArgs e)
    {
        movementState = MovementState.Seeking;
    }

    private void CellState_OnTargetEat(object sender, System.EventArgs e)
    {
        movementState = MovementState.Seeking;
    }

    private void Update()
    {
        CheckEdges();

        switch (movementState)
        {
            case MovementState.Seeking:
                SeekSteering();

                (xoff1, yoff1) = IncrementOffset(xoff1, yoff1);
                (xoff2, yoff2) = IncrementOffset(xoff2, yoff2);

                break;
            case MovementState.Eating:
                SteerTo(targetPosition); 
                break;
        }
    }

    public void SteerTo(Vector3 target)
    {
        Vector3 direction = target - transform.position;
        direction.Normalize();
        MoveCell(direction);
    }

    private void MoveCell(Vector3 direction)
    {
        velocity += direction;
        
        if (velocity.magnitude > topSpeed)
        {
            velocity.Normalize();
            velocity *= topSpeed;
        }

        transform.position += velocity * Time.deltaTime;
    }

    private void CheckEdges()
    {
        if (transform.position.x > cameraWidth) transform.position = new Vector3(-cameraWidth, transform.position.y, 0);
        if (transform.position.x < -cameraWidth) transform.position = new Vector3(cameraWidth, transform.position.y, 0);
        if (transform.position.y > cameraHeight) transform.position = new Vector3(transform.position.x, -cameraHeight, 0);
        if (transform.position.y < -cameraHeight) transform.position = new Vector3(transform.position.x, cameraHeight, 0);
    }

    private (float, float) IncrementOffset(float xoff, float yoff)
    {
        xoff += xIncrement;
        yoff += yIncrement;

        return (xoff, yoff);
    }

    private void SeekSteering()
    {
        float xDirection = Mathf.PerlinNoise(xoff1, yoff1);
        float yDirection = Mathf.PerlinNoise(xoff2, yoff2);

        xDirection = Utils.Map(xDirection, 0f, 1f, -1f, 1f);
        yDirection = Utils.Map(yDirection, 0f, 1f, -1f, 1f);

        acceleration = new Vector3(xDirection, yDirection, 0);
        
        MoveCell(acceleration);
    }
}
