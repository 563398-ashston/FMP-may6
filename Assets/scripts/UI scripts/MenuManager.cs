using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Menu Objects")]
    [SerializeField] private GameObject mainMenuCanvasGO;
    [SerializeField] private GameObject settingsCanvasGO;
    [SerializeField] private GameObject returnToMenuCanvasGo;
    [SerializeField] private GameObject controllerCanvasGO;
    [SerializeField] private GameObject keyboardCanvasGO;
    [SerializeField] private GameObject audioCanvasGO;

    [Header("Player Scripts to Deactivate on Pause")]
    [SerializeField] private PlayerController playerCon;

    [Header("first selected options")]
    [SerializeField] private GameObject mainMenuFirst;
    [SerializeField] private GameObject settingsMenuFirst;
    [SerializeField] private GameObject returnToMenuFirst;
    [SerializeField] private GameObject controllerMenuFirst;
    [SerializeField] private GameObject keyboardMenuFirst;
    [SerializeField] private GameObject audioCanvasFirst;

    private bool ispaused;

    private void Start()
    {
        //AudioManager.instance.PlayMusic("background music");

        mainMenuCanvasGO.SetActive(false);
        settingsCanvasGO.SetActive(false);
        returnToMenuCanvasGo.SetActive(false);
        controllerCanvasGO.SetActive(false);
        keyboardCanvasGO.SetActive(false);
        audioCanvasGO.SetActive(false);

        Time.timeScale = 1.0f;
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

    //open canvas code
    private void OpenMainMenu()
    {
        mainMenuCanvasGO.SetActive(true);
        settingsCanvasGO.SetActive(false);
        returnToMenuCanvasGo.SetActive(false);
        controllerCanvasGO.SetActive(false);
        keyboardCanvasGO.SetActive(false);
        audioCanvasGO.SetActive(false);

        EventSystem.current.SetSelectedGameObject(mainMenuFirst);
    }

    public void OpenSettingsMenu()
    {
        mainMenuCanvasGO.SetActive(false);
        settingsCanvasGO.SetActive(true);
        returnToMenuCanvasGo.SetActive(false);
        controllerCanvasGO.SetActive(false);
        keyboardCanvasGO.SetActive(false);
        audioCanvasGO.SetActive(false);

        EventSystem.current.SetSelectedGameObject(settingsMenuFirst);
    }

    private void OpenReturnToMenu()
    {
        mainMenuCanvasGO.SetActive(false);
        settingsCanvasGO.SetActive(false);
        returnToMenuCanvasGo.SetActive(true);
        controllerCanvasGO.SetActive(false);
        keyboardCanvasGO.SetActive(false);
        audioCanvasGO.SetActive(false);

        EventSystem.current.SetSelectedGameObject(returnToMenuFirst);
    }

    private void OpenControllerMenu()
    {
        mainMenuCanvasGO.SetActive(false);
        settingsCanvasGO.SetActive(false);
        returnToMenuCanvasGo.SetActive(false);
        controllerCanvasGO.SetActive(true);
        keyboardCanvasGO.SetActive(false);
        audioCanvasGO.SetActive(false);

        EventSystem.current.SetSelectedGameObject(controllerMenuFirst);
    }

    private void OpenKeyboardMenu()
    {
        mainMenuCanvasGO.SetActive(false);
        settingsCanvasGO.SetActive(false);
        returnToMenuCanvasGo.SetActive(false);
        controllerCanvasGO.SetActive(false);
        keyboardCanvasGO.SetActive(true);
        audioCanvasGO.SetActive(false);

        EventSystem.current.SetSelectedGameObject(keyboardMenuFirst);
    }

    private void OpenAudioMenu()
    {
        mainMenuCanvasGO.SetActive(false);
        settingsCanvasGO.SetActive(false);
        returnToMenuCanvasGo.SetActive(false);
        controllerCanvasGO.SetActive(false);
        keyboardCanvasGO.SetActive(false);
        audioCanvasGO.SetActive(true);

        Debug.Log("first selected =", audioCanvasFirst);

        EventSystem.current.SetSelectedGameObject(audioCanvasFirst);
    }

    //close canvas code
    private void CloseAllMenus()
    {
        mainMenuCanvasGO.SetActive(false);
        settingsCanvasGO.SetActive(false);
        returnToMenuCanvasGo.SetActive(false);
        controllerCanvasGO.SetActive(false);
        keyboardCanvasGO.SetActive(false);
        audioCanvasGO.SetActive(false);

        EventSystem.current.SetSelectedGameObject(null);
    }

    public void OnSettingsPress()
    {
        OpenSettingsMenu();
    }
    public void OnSettingBackPress()
    {
        OpenMainMenu();
    }


    public void OnreturnToMenuPress()
    {
        OpenReturnToMenu();
    }
    public void OnreturnToMenuBackPress()
    {
        OpenMainMenu();
    }


    public void OnConrollerMenuPress()
    {
        OpenControllerMenu();
    }

    public void OnConrollerMenuBackPress()
    {
        OpenSettingsMenu();
    }


    public void OnKeyboardMenuPress()
    {
        OpenKeyboardMenu();
    }

    public void OnKeyboardMenuBackPress()
    {
        OpenSettingsMenu();
    }


    public void OnAudioMenuPress()
    {
        OpenAudioMenu();
    }

    public void OnAudioMenuBackPress()
    {
        OpenSettingsMenu();
    }


    public void OnResumePress()
    {
        Unpause();
    }


    public void MuteMusic(bool mute)
    {
        AudioManager.instance.musicMute = mute;
        //print("music mute=" + AudioManager.instance.musicMute);
    }

    public void playButtonSFX()
    {
        AudioManager.instance.PlaySFX("buttonsfx");
    }

    public void QuitToFrontend()
    {
        SceneManager.LoadScene("Frontend");
    }
}