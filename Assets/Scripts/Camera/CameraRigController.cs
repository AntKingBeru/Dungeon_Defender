using UnityEngine;

public class CameraRigController : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform target;
    
    [Header("Rotation")]
    [SerializeField] private float dragRotationSpeed = 0.15f;
    
    [Header("Follow")]
    [SerializeField] private Vector3 followOffset = Vector3.zero;
    [SerializeField] private float followSmoothTime = 0.1f;

    private float _currentYaw;
    private Vector3 _velocity;

    private void LateUpdate()
    {
        FollowTarget();
    }

    public void RotateByDrag(float mouseDeltaX)
    {
        if (Mathf.Abs(mouseDeltaX) < 0.01f) return;
        
        _currentYaw += mouseDeltaX * dragRotationSpeed;
        transform.rotation = Quaternion.Euler(0f, _currentYaw, 0f);
    }
    
    private void FollowTarget()
    {
        var desiredPos = target.position + followOffset;
        
        transform.position = Vector3.SmoothDamp(
            transform.position, 
            desiredPos,
            ref _velocity,
            followSmoothTime
            );
    }
}