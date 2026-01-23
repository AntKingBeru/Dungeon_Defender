using UnityEngine;

[CreateAssetMenu(fileName = "CorridorRoomDefinition", menuName = "Scriptable Objects/CorridorRoomDefinition")]
public class CorridorRoomDefinition : RoomDefinition
{
    private void OnValidate()
    {
        category = RoomCategory.Corridor;
        allowedRotation = true;
    }
}
