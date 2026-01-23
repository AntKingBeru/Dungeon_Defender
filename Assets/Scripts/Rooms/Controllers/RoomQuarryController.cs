using UnityEngine;

public class RoomQuarryController : MonoBehaviour
{
    [SerializeField] private QuarryRoomDefinition definition;
    [SerializeField] private HarvestInteractionPoint harvestPoint;
    
    private int _upgradeLevel = 0;

    public int GetStoneAmount()
    {
        return definition.baseStoneAmount + (_upgradeLevel * definition.upgradeStoneBonus);
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