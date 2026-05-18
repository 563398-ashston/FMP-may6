using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    public bool MenuOpenCloseInput {  get; private set; }

    //private PlayerInput playerInput;

    private InputAction menuOpenCloseAction;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        //InputSystem.playerInput = GetComponent<PlayerInput>();

        menuOpenCloseAction = InputSystem.actions.FindAction("MenuOpenClose");
        //menuOpenCloseAction = playerInput.actions["MenuOpenClose"];
    }

    private void Update()
    {
        MenuOpenCloseInput = menuOpenCloseAction.WasPressedThisFrame();
    }
}
