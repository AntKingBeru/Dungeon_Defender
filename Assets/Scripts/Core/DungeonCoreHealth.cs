using UnityEngine;
using UnityEngine.Events;

public class DungeonCoreHealth : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private int maxHealth = 1000;
    private int _currentHealth;
    
    [Header("Feedback")]
    [SerializeField] private DungeonCoreVisuals visuals;
    
    [Header("Events")]
    public UnityEvent onCoreDestroyed;

    private bool _isDestroyed = false;

    private void Awake()
    {
        _currentHealth = maxHealth;
        visuals.UpdateDamageVisuals(1f);
    }
    
    private void TakeDamage(int amount)
    {
        if (_isDestroyed) return;

        _currentHealth -= amount;
        _currentHealth = Mathf.Max(_currentHealth, 0);
        
        var healthPercent = (float)_currentHealth / maxHealth;
        visuals.UpdateDamageVisuals(healthPercent);

        if (_currentHealth <= 0) DestroyCore();
    }

    private void DestroyCore()
    {
        _isDestroyed = true;
        
        visuals.PlayDestructionSequence();
        
        onCoreDestroyed?.Invoke();

        Debug.Log("Dungeon Core Destroyed – Game Over");
    }
}