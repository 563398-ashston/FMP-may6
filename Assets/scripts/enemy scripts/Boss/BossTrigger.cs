using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    public Animator doorAnimator;

    public GameObject boss;
    public BossRoomDoors Door1;
    public BossRoomDoors Door2;

    private bool activated = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!activated && other.CompareTag("Player"))
        {
            activated = true;

            boss.SetActive(true);

            Door1.CloseDoor();
            Door2.CloseDoor();
        }
    }
}