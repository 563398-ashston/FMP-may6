using UnityEngine;

public class BossRoomDoors : MonoBehaviour
{
    public Animator animator;

    public void CloseDoor()
    {
        animator.SetTrigger("Close");
    }

    public void OpenDoor()
    {
        animator.SetTrigger("Open");
    }
}