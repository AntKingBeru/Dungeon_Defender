using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class Door : MonoBehaviour, IDoor
{
    [Header("References")]
    [SerializeField] private Transform doorPivot;
    [SerializeField] private NavMeshObstacle navObstacle;
    
    [Header("Settings")]
    [SerializeField] private float openAngle = 90f;
    [SerializeField] private float openDuration = 0.35f;

    private bool _isOpen;
    private Coroutine _animationRoutine;

    private void Awake()
    {
        if (navObstacle != null) navObstacle.enabled = true;
    }
    
    public void Open()
    {
        if (_isOpen) return;
    
        _isOpen = true;
        if (navObstacle != null) navObstacle.enabled = false;

        StartRotation(0f, openAngle);
    }

    public void Close()
    {
        if (!_isOpen) return;

        _isOpen = false;
        if (navObstacle != null) navObstacle.enabled = true;
        
        StartRotation(openAngle, 0f);
    }
    
    private void StartRotation(float from, float to, System.Action onComplete = null)
    {
        if (_animationRoutine != null) StopCoroutine(_animationRoutine);

        _animationRoutine = StartCoroutine(RotateDoor(from, to, onComplete));
    }
    
    private IEnumerator RotateDoor(float from, float to, System.Action onComplete)
    {
        var elapsed = 0f;

        while (elapsed < openDuration)
        {
            elapsed += Time.deltaTime;
            var time = elapsed / openDuration;

            var angle = Mathf.Lerp(from, to, time);
            doorPivot.localRotation = Quaternion.Euler(0f, angle, 0f);

            yield return null;
        }

        doorPivot.localRotation = Quaternion.Euler(0f, to, 0f);
        onComplete?.Invoke();
    }
}