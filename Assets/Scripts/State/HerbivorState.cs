using UnityEngine;

public class HerbivorState : MonoBehaviour
{
    private FoodManager fm;
    private CellSteering cs;
    private CellCore cc;

    private bool hasTarget;
    private Vector3 targetPosition;

    private void Awake()
    {
        cs = gameObject.GetComponent<CellSteering>();
        cc = gameObject.GetComponent<CellCore>();
    }

    private void Start()
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
        if (collision.TryGetComponent(out FoodCore fc) && !hasTarget)
        {
            cs.StopSeeking();
            hasTarget = true;
            targetPosition = collision.transform.position;
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if (other.TryGetComponent(out FoodCore fc) && hasTarget)
        {
            hasTarget = false;
            cs.StartSeeking();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out FoodCore fc))
        {
            cc.Eat(fc.GetFoodToAdd());
            Destroy(collision.gameObject);
            fm.ChangeCurrentFood(-1);
            //hasTarget = false;
        }
    }
}
