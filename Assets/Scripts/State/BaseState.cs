using UnityEngine;

public abstract class BaseState : MonoBehaviour
{
    // protected signifie accessible uniquement dans la classe et classe enfant
    private CellSteering cellSteering;
    protected CellCore cellCore;

    private bool hasTarget;
    private Vector3 targetPosition;

    protected abstract void Eat(GameObject target);
    protected abstract bool CanTarget(GameObject target);

    private void Awake()
    {
        cellSteering = gameObject.GetComponent<CellSteering>();
        cellCore = gameObject.GetComponent<CellCore>();
    }

    protected virtual void Start()
    {

    }

    private void Update()
    {
        if (hasTarget)
        {
            cellSteering.SteerTo(targetPosition);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (CanTarget(collision.gameObject) && !hasTarget)
        {
            cellSteering.StopSeeking();
            hasTarget = true;
            targetPosition = collision.transform.position;
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if (CanTarget(other.gameObject) && hasTarget)
        {
            hasTarget = false;
            cellSteering.StartSeeking();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (CanTarget(collision.gameObject))
        {
            Eat(collision.gameObject);
        }
    }
}
