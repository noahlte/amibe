using UnityEngine;

public class NeutralState : MonoBehaviour
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
        fm = GameObject.FindGameObjectWithTag("FoodManager").GetComponent<FoodManager>();
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
        if (collision.CompareTag("Food") && !hasTarget)
        {
            Debug.Log($"{gameObject.name} has found food");
            cs.StopSeeking();
            hasTarget = true;
            targetPosition = collision.transform.position;
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if (other.CompareTag("Food") && hasTarget)
        {
            Debug.Log($"{gameObject.name} has lost food");
            hasTarget = false;
            cs.StartSeeking();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Food"))
        {
            cc.Eat(2);
            Destroy(collision.gameObject);
            fm.ChangeCurrentFood(-1);
            hasTarget = false;
            Debug.Log($"{gameObject.name} has eat food");
        }
    }
}
