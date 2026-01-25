using UnityEngine;

[CreateAssetMenu(fileName = "QuarryRoomDefinition", menuName = "Scriptable Objects/QuarryRoomDefinition")]
public class QuarryRoomDefinition : RoomDefinition
{
    [Header("Quarry Data")]
    public int baseResourceAmount;
    public float harvestCooldown;
    public int upgradeResourceBonus;
    public ResourceType resourceType;
}