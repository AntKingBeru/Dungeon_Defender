using UnityEngine;

public class RoomsDoorController : MonoBehaviour
{
    [SerializeField] private Door[] doors;
    
    public void OpenAllDoors()
    {
        foreach (var door in doors) door.Open();
    }

    public Door GetDoorByDirection(DoorDirection direction)
    {
        foreach (var door in doors)
        {
            if (door.TryGetComponent<DoorDirectionTag>(out var tag) && tag.direction == direction) return door;
        }
        return null;
    }
}