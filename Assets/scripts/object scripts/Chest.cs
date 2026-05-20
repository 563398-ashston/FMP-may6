using UnityEngine;
using UnityEngine.EventSystems;

public class Chest : MonoBehaviour
{
    public GameObject winCanvasGO;
    public GameObject winCanvasFirst;

    private void Start()
    {
        winCanvasGO.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player hit chest");
            winCanvasGO.SetActive(true);

            

            EventSystem.current.SetSelectedGameObject(winCanvasFirst);
        }
    }
}