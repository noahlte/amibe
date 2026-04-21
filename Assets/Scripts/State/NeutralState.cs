using UnityEngine;

public class NeutralState : MonoBehaviour
{
    private FoodManager fm;
    private CellSteering cs;
    private CellCore cc;

    void Start()
    {
        cs = gameObject.GetComponent<CellSteering>();
        cc = gameObject.GetComponent<CellCore>();
        fm = GameObject.FindGameObjectWithTag("FoodManager").GetComponent<FoodManager>();
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
            cc.Eat(2);
            Destroy(collision.gameObject);
            fm.ChangeCurrentFood(-1);
        }
    }
}
