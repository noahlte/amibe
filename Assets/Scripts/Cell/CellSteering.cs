// TODO: Refactor du système de mouvement, besoins de faire en sorte que la cellule ne sort pas de l'écran

using UnityEngine;

public class CellSteering : MonoBehaviour
{
    private float cameraHeight;
    private float cameraWidth;

    [Header("Movement")]
    [SerializeField] private float topSpeed = 0.2f;
    [SerializeField] private float xIncrement = 0.2f;
    [SerializeField] private float yIncrement = 0.1f;
    private float minSeekingDistance = 1;
    private float maxSeekingDistance = 3;
    private Vector3 seekingTarget;
    
    private Vector3 velocity;

    private ICellState cellState;
    private CellCore cellCore;

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
        cellCore = gameObject.GetComponent<CellCore>();
    }

    private void Start()
    {
        xoff1 = Random.Range(0, 10000);
        yoff1 = Random.Range(0, 10000);

        xoff2 = Random.Range(0, 10000);
        yoff2 = Random.Range(0, 10000);

        velocity = Vector3.zero;

        (cameraWidth, cameraHeight) = Utils.GetCameraBounds();

        cellState.OnTargetFound += CellState_OnTargetFound;
        cellState.OnTargetLost += CellState_OnTargetLost;
        cellState.OnTargetEat += CellState_OnTargetEat;

        ChooseNewSeekingTarget();

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

    private (float, float) IncrementOffset(float xoff, float yoff)
    {
        xoff += xIncrement;
        yoff += yIncrement;

        return (xoff, yoff);
    }

    private void SeekSteering()
    {
        if (Vector3.Distance(transform.position, seekingTarget) < cellCore.GetRadius())
        {
            ChooseNewSeekingTarget();
        } 

        Vector3 normalizeSeekingTarget = seekingTarget;
        normalizeSeekingTarget.Normalize();

        float offsetX = Utils.Map(Mathf.PerlinNoise(xoff1, yoff2), 0f, 1f, -0.01f, 0.01f);
        float offsetY = Utils.Map(Mathf.PerlinNoise(xoff2, yoff2), 0f, 1f, -0.01f, 0.01f);

        transform.position += new Vector3(offsetX, offsetY, 0);
        
        SteerTo(seekingTarget);
    }

    private void ChooseNewSeekingTarget()
    {
        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        float distance = Random.Range(minSeekingDistance, maxSeekingDistance);

        float targetX = transform.position.x + distance * Mathf.Cos(angle);
        float targetY = transform.position.y + distance * Mathf.Sin(angle);

        targetX = Mathf.Clamp(targetX, -cameraWidth, cameraWidth);
        targetY = Mathf.Clamp(targetY, -cameraHeight, cameraHeight);

        seekingTarget = new Vector3(targetX, targetY, 0);
    }
}
