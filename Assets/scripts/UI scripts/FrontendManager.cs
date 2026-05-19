using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class FrontendManager : MonoBehaviour
{
    [Header("Menu Objects")]
    [SerializeField] private GameObject frontendCanvasGO;
    [SerializeField] private GameObject settingsCanvasGO;
    [SerializeField] private GameObject returnToOsCanvasGo;
    [SerializeField] private GameObject gamepadCanvasGO;
    [SerializeField] private GameObject keyboardCanvasGO;
    [SerializeField] private GameObject howToPlayCanvasGO;
    [SerializeField] private GameObject audioCanvasGO;

    [Header("Player Scripts to Deactivate on Pause")]
    [SerializeField] private PlayerController playerCon;

    [Header("first selected options")]
    [SerializeField] private GameObject frontendFirst;
    [SerializeField] private GameObject settingsMenuFirst;
    [SerializeField] private GameObject returnToOsFirst;
    [SerializeField] private GameObject gamepadMenuFirst;
    [SerializeField] private GameObject keyboardMenuFirst;
    [SerializeField] private GameObject howToPlayMenuFirst;
    [SerializeField] private GameObject audioCanvasFirst;

    private bool ispaused;

    private void Start()
    {
        frontendCanvasGO.SetActive(true);
        settingsCanvasGO.SetActive(false);
        returnToOsCanvasGo.SetActive(false);
        gamepadCanvasGO.SetActive(false);
        keyboardCanvasGO.SetActive(false);
        howToPlayCanvasGO.SetActive(false);
        audioCanvasGO.SetActive(false);
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

        OpenFrontend();
    }


    public void Unpause()
    {

        ispaused = false;
        Time.timeScale = 1f;

        playerCon.enabled = true;

        CloseAllMenus();
    }

    //open canvas code
    private void OpenFrontend()
    {
        frontendCanvasGO.SetActive(true);
        settingsCanvasGO.SetActive(false);
        returnToOsCanvasGo.SetActive(false);
        gamepadCanvasGO.SetActive(false);
        keyboardCanvasGO.SetActive(false);
        howToPlayCanvasGO.SetActive(false);
        audioCanvasGO.SetActive(false);

        EventSystem.current.SetSelectedGameObject(frontendFirst);
    }

    public void OpenSettingsMenu()
    {
        frontendCanvasGO.SetActive(false);
        settingsCanvasGO.SetActive(true);
        returnToOsCanvasGo.SetActive(false);
        gamepadCanvasGO.SetActive(false);
        keyboardCanvasGO.SetActive(false);
        howToPlayCanvasGO.SetActive(false);
        audioCanvasGO.SetActive(false);

        EventSystem.current.SetSelectedGameObject(settingsMenuFirst);
    }

    private void OpenReturnToOs()
    {
        frontendCanvasGO.SetActive(false);
        settingsCanvasGO.SetActive(false);
        returnToOsCanvasGo.SetActive(true);
        gamepadCanvasGO.SetActive(false);
        keyboardCanvasGO.SetActive(false);
        howToPlayCanvasGO.SetActive(false);
        audioCanvasGO.SetActive(false);

        EventSystem.current.SetSelectedGameObject(returnToOsFirst);
    }

    private void OpenGamepadMenu()
    {
        frontendCanvasGO.SetActive(false);
        settingsCanvasGO.SetActive(false);
        returnToOsCanvasGo.SetActive(false);
        gamepadCanvasGO.SetActive(true);
        keyboardCanvasGO.SetActive(false);
        howToPlayCanvasGO.SetActive(false);
        audioCanvasGO.SetActive(false);

        EventSystem.current.SetSelectedGameObject(gamepadMenuFirst);
    }

    private void OpenKeyboardMenu()
    {
        frontendCanvasGO.SetActive(false);
        settingsCanvasGO.SetActive(false);
        returnToOsCanvasGo.SetActive(false);
        gamepadCanvasGO.SetActive(false);
        keyboardCanvasGO.SetActive(true);
        howToPlayCanvasGO.SetActive(false);
        audioCanvasGO.SetActive(false);

        EventSystem.current.SetSelectedGameObject(keyboardMenuFirst);
    }

    private void OpenHowToPlayMenu()
    {
        frontendCanvasGO.SetActive(false);
        settingsCanvasGO.SetActive(false);
        returnToOsCanvasGo.SetActive(false);
        gamepadCanvasGO.SetActive(false);
        keyboardCanvasGO.SetActive(false);
        howToPlayCanvasGO.SetActive(true);
        audioCanvasGO.SetActive(false);

        EventSystem.current.SetSelectedGameObject(howToPlayMenuFirst);
    }

    private void OpenAudioMenu()
    {
        frontendCanvasGO.SetActive(false);
        settingsCanvasGO.SetActive(false);
        returnToOsCanvasGo.SetActive(false);
        gamepadCanvasGO.SetActive(false);
        keyboardCanvasGO.SetActive(false);
        howToPlayCanvasGO.SetActive(false);
        audioCanvasGO.SetActive(true);

        EventSystem.current.SetSelectedGameObject(audioCanvasFirst);
    }

    //close canvas code
    private void CloseAllMenus()
    {
        frontendCanvasGO.SetActive(false);
        settingsCanvasGO.SetActive(false);
        returnToOsCanvasGo.SetActive(false);
        gamepadCanvasGO.SetActive(false);
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
        OpenFrontend();
    }


    public void OnreturnToOsPress()
    {
        OpenReturnToOs();
    }
    public void OnreturnToOsBackPress()
    {
        OpenFrontend();
    }


    public void OnGamepadMenuPress()
    {
        OpenGamepadMenu();
    }

    public void OnGamepadMenuBackPress()
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


    public void OnHowToPlayMenuPress()
    {
        OpenHowToPlayMenu();
    }

    public void OnHowToPlayMenuBackPress()
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
    }
    public void LoadGame()
    {
        SceneManager.LoadScene("MainGame");
    }
    public void QuitGame()
    {
        Application.Quit();
    }

}
