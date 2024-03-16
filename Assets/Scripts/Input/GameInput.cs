using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

    private InputActions inputActions;

    public event EventHandler OnChangeCam;
    public event EventHandler OnOpenedOrClosedPanel;
    private void Awake()
    {
        if (!Instance)
            Instance = this;

        inputActions = new InputActions();

        inputActions.Player.Enable();

        inputActions.Player.ChangeCam.performed += ChangeCam_performed;
        inputActions.Player.OpenClosePanel.performed += OpenClosePanel_performed;
    }

    private void OpenClosePanel_performed(InputAction.CallbackContext obj)
    {
        OnOpenedOrClosedPanel?.Invoke(this, EventArgs.Empty);
    }

    private void ChangeCam_performed(InputAction.CallbackContext obj)
    {
        OnChangeCam?.Invoke(this, EventArgs.Empty);
    }

    private void OnDestroy()
    {
        inputActions.Player.ChangeCam.performed -= ChangeCam_performed;
        inputActions.Player.OpenClosePanel.performed -= OpenClosePanel_performed;
        inputActions.Dispose();
    }
    public InputActions GetInputActions()
    {
        return inputActions;
    }
    public bool IsJumpButtonPressed()
    {
        return inputActions.Player.Jump.triggered;
    }
    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = inputActions.Player.Movement.ReadValue<Vector2>();

        inputVector = inputVector.normalized;

        return inputVector;
    }
    public Vector2 GetMouseLook()
    {
        Vector2 lookVector = inputActions.Player.MouseLook.ReadValue<Vector2>();
        return lookVector;
    }
}
