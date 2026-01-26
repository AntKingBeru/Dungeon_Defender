using UnityEngine;

public class HingedDoor : MonoBehaviour
{
    [SerializeField] private float openAngle = 90f;
    [SerializeField] private float openSpeed = 2f;
    
    private Quaternion _closedRotation;
    private Quaternion _openRotation;
    private bool _isOpen;

    private void Awake()
    {
        _closedRotation = transform.localRotation;
        _openRotation = Quaternion.Euler(0f, openAngle, 0f) * _closedRotation;
        transform.localRotation = Quaternion.identity;
    }

    private void Update()
    {
        var target = _isOpen ? _openRotation : _closedRotation;
        transform.localRotation = Quaternion.Slerp(transform.localRotation, target, Time.deltaTime * openSpeed);
    }
    
    public void Open() => _isOpen = true;
    public void Close() => _isOpen = false;
}