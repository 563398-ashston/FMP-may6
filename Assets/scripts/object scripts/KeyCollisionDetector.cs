using UnityEngine;
using UnityEngine.Events;

public class KeyCollisionDetector : MonoBehaviour
{
    [SerializeField] private string keyColliderScript;

    [SerializeField] private UnityEvent collisionEntered;

    [SerializeField] private UnityEvent collisionExit;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent(keyColliderScript))
        {
            collisionEntered?.Invoke();
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent(keyColliderScript))
        {
            collisionExit?.Invoke();
        }
    }
}
