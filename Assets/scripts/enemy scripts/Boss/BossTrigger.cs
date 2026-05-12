using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    public GameObject boss;

    private bool activated = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!activated && other.CompareTag("Player"))
        {
            activated = true;

            boss.SetActive(true);
        }
    }
}