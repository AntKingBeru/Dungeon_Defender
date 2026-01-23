using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 MousePosition { get; private set; }
    public Vector2 MouseDelta { get; private set; }
    
    public bool RotateHeld { get; private set; }
    public bool MovePressed { get; private set; }
    public bool InteractPressed { get; private set; }
    
    #region Input Callbacks
    public void OnPoint(InputAction.CallbackContext context)
    {
        MousePosition = context.ReadValue<Vector2>();
    }

    public void OnClickMove(InputAction.CallbackContext context)
    {
        MovePressed = context.performed;
    }
    
    public void OnInteract(InputAction.CallbackContext context)
    {
        InteractPressed = context.performed;
    }

    public void OnCameraRotateDrag(InputAction.CallbackContext context)
    {
        RotateHeld = context.ReadValueAsButton();
    }
    
    public void OnCameraRotateDelta(InputAction.CallbackContext context)
    {
        MouseDelta = context.ReadValue<Vector2>();
    }
    #endregion

    public void ClearFrameInput()
    {
        MovePressed = false;
        InteractPressed = false;
        MouseDelta = Vector2.zero;
    }
}