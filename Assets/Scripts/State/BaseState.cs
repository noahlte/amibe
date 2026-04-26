using System;
using UnityEngine;

public abstract class BaseState : MonoBehaviour
{
    public event EventHandler<OnTargetFoundArgs> OnTargetFound;
    public class OnTargetFoundArgs : EventArgs
    {
        public Vector3 targetPosition;
    }
    public event EventHandler OnTargetLost;
    public event EventHandler OnTargetEat;

    protected CellCore cellCore;

    private Vector3 targetPosition;

    protected abstract void Eat(GameObject target);
    protected abstract bool CanTarget(GameObject target);

    private void Awake()
    {
        cellCore = gameObject.GetComponent<CellCore>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (CanTarget(collision.gameObject))
        {
            targetPosition = collision.transform.position;
            OnTargetFound?.Invoke(this, new OnTargetFoundArgs
            {
                targetPosition = targetPosition,
            });
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if (CanTarget(other.gameObject))
        {
            OnTargetLost?.Invoke(this, EventArgs.Empty);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (CanTarget(collision.gameObject))
        {
            Eat(collision.gameObject);
            OnTargetEat?.Invoke(this, EventArgs.Empty);
        }
    }
}
