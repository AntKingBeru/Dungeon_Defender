using UnityEngine;

public class RoomQuarryController : MonoBehaviour
{
    [SerializeField] private QuarryRoomDefinition definition;
    [SerializeField] private HarvestInteractionPoint harvestPoint;
    
    private int _upgradeLevel = 0;

    public int GetResourceAmount()
    {
        return definition.baseResourceAmount + (_upgradeLevel * definition.upgradeResourceBonus);
    }

    public ResourceType GetResourceType()
    {
        return definition.resourceType;
    }

    public float GetCooldown()
    {
        return definition.harvestCooldown;
    }

    public void Upgrade()
    {
        _upgradeLevel++;
        // TODO: visual upgrade hook
    }
}