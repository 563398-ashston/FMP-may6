using UnityEngine;

public class HealScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        HealthScript health = collision.GetComponent<HealthScript>();

        if (health != null)
        {
            health.ResetHealth();
            Debug.Log("Player healed to max health!");
        }
    }
}
