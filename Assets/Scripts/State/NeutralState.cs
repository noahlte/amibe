using UnityEngine;

public class NeutralState : MonoBehaviour
{
    private CellSteering cs;
    private CellCore cc;

    void Start()
    {
        cs = gameObject.GetComponent<CellSteering>();
        cc = gameObject.GetComponent<CellCore>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Food"))
        {
            cs.StopSeeking();
            cs.SteerTo(collision.transform.position);
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if (other.CompareTag("Food"))
        {
            cs.StartSeeking();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Food"))
        {
            cc.Eat(10);
            Destroy(collision.gameObject);
        }
    }
}
