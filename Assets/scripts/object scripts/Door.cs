using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();    
    }

    [ContextMenu(itemName:"Open")]
    public void open()
    {
        anim.SetTrigger(name: "Open");
        Destroy(gameObject, 1f);
    }
}
