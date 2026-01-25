using System;
using System.Collections.Generic;

public class PlayerInventory
{
    public static PlayerInventory instance;

    public static PlayerInventory Instance
    {
        get
        {
            instance ??= new PlayerInventory();
            return instance;
        }
    }
    
    private Dictionary<ResourceType, int> _resources = new Dictionary<ResourceType, int>();
    
    public event Action<ResourceType, int> OnResourceChanged;

    private PlayerInventory()
    {
        foreach (ResourceType type in Enum.GetValues(typeof(ResourceType)))
        {
            _resources[type] = 0;
        }
    }
    
    public int Get(ResourceType type) => _resources[type];
    
    public void AddResource(ResourceType type, int amount)
    {
        if (!_resources.ContainsKey(type))
        {
            _resources.Add(type, amount);
        }
        else
        {
            _resources[type] += amount;
        }
        OnResourceChanged?.Invoke(type, _resources[type]);
    }

    public bool Spend(ResourceType type, int amount)
    {
        if (_resources[type] < amount) return false;
        
        _resources[type] -= amount;
        OnResourceChanged?.Invoke(type, _resources[type]);
        return true;
    }
}
