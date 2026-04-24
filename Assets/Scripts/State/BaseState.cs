using UnityEngine;

public abstract class BaseState : MonoBehaviour
{
    // protected signifie accessible uniquement dans la classe et classe enfant
    protected FoodManager fm;
    private CellSteering cs;
    protected CellCore cc;

    private bool hasTarget;
    private Vector3 targetPosition;

    protected abstract void Eat(GameObject target);
    protected abstract bool CanTarget(GameObject target);

    private void Awake()
    {
        cs = gameObject.GetComponent<CellSteering>();
        cc = gameObject.GetComponent<CellCore>();
    }

    protected virtual void Start()
    {
        fm = FindAnyObjectByType<FoodManager>();
    }

    private void Update()
    {
        if (hasTarget)
        {
            cs.SteerTo(targetPosition);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (CanTarget(collision.gameObject) && !hasTarget)
        {
            cs.StopSeeking();
            hasTarget = true;
            targetPosition = collision.transform.position;
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if (CanTarget(other.gameObject)  && hasTarget)
        {
            hasTarget = false;
            cs.StartSeeking();
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
