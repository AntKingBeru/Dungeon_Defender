using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerInputHandler))]
public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CameraRigController cameraRig;
    
    private PlayerMovement _movement;
    private PlayerInputHandler _input;
    
    private void Awake()
    {
        _movement = GetComponent<PlayerMovement>();
        _input = GetComponent<PlayerInputHandler>();
    }

    private void Update()
    {
        HandleMovement();
        HandleCamera();
        
        _input.ClearFrameInput();
    }

    private void HandleMovement()
    {
        if (_input.MovePressed && !_input.RotateHeld)
        {
            _movement.MoveToScreenPoint(_input.MousePosition);
        }
    }

    private void HandleCamera()
    {
        if (_input.RotateHeld)
        {
            cameraRig.RotateByDrag(_input.MouseDelta.x);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}