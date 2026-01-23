using UnityEngine;

[CreateAssetMenu(fileName = "QuarryRoomDefinition", menuName = "Scriptable Objects/QuarryRoomDefinition")]
public class QuarryRoomDefinition : RoomDefinition
{
    [Header("Quarry Data")]
    public int baseStoneAmount;
    public float harvestCooldown;
    public int upgradeStoneBonus;
}
