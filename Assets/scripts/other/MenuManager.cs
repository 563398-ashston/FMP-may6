using UnityEngine;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour
{

    [Header("Menu Objects")]
    [SerializeField] private GameObject mainMenuCanvasGO;
    [SerializeField] private GameObject settingsCanvasGO;
    [SerializeField] private GameObject controlCanvasGO;

    [Header("Player Scripts to Deactivate on Pause")]
    [SerializeField] private PlayerController playerCon;



    [SerializeField] private GameObject mainMenuFirst;
    [SerializeField] private GameObject settingsMenuFirst;
    [SerializeField] private GameObject controlMenuFirst;




    private bool ispaused;

    private void Start()
    {
        mainMenuCanvasGO.SetActive(false);
        settingsCanvasGO.SetActive(false);
        controlCanvasGO.SetActive(false);

    }

    private void Update()
    {
        if (InputManager.instance.MenuOpenCloseInput)
        {
            if (!ispaused)
            {
                Pause();
            }
            else
            {
                Unpause();
            }

        }
    }


    public void Pause()
    {
        ispaused = true;
        Time.timeScale = 0f;

        playerCon.enabled = false;

        OpenMainMenu();
    }


    public void Unpause()
    {
        ispaused = false;
        Time.timeScale = 1f;

        playerCon.enabled = true;

        CloseAllMenus();
    }

    public void OpenSettingsMenuHandle()
    {
        mainMenuCanvasGO.SetActive(false);
        settingsCanvasGO.SetActive(true);
        controlCanvasGO.SetActive(false);


        EventSystem.current.SetSelectedGameObject(settingsMenuFirst);
    }


    private void OpenMainMenu()
    {
        mainMenuCanvasGO.SetActive(true);
        settingsCanvasGO.SetActive(false);
        controlCanvasGO.SetActive(false);


        EventSystem.current.SetSelectedGameObject(mainMenuFirst);
    }

    private void OpenControlMenu()
    {
        mainMenuCanvasGO.SetActive(false);
        settingsCanvasGO.SetActive(false);
        controlCanvasGO.SetActive(true);


        EventSystem.current.SetSelectedGameObject(controlMenuFirst);
    }




    private void CloseAllMenus()
    {
        mainMenuCanvasGO.SetActive(false);
        settingsCanvasGO.SetActive(false);
        controlCanvasGO.SetActive(false);



        EventSystem.current.SetSelectedGameObject(null);
    }

    public void OnSettingsPress()
    {
        OpenSettingsMenuHandle();
    }

    public void OnResumePress()
    {
        Unpause();
    }

    public void OnSettingBackPress()
    {
        OpenMainMenu();
    }

    public void OncontrolPress()
    {
        OpenControlMenu();
    }

    public void OnControlsBackPress()
    {
        OpenSettingsMenuHandle();
    }
}