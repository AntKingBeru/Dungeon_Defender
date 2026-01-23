using UnityEngine;

[CreateAssetMenu(fileName = "MustConnectToRoomRule", menuName = "Scriptable Objects/PlacementRules/MustConnectToRoomRule")]
public class MustConnectToRoomRule : PlacementRule
{
    public override bool IsValid(BuildPreview preview, GridManager grid)
    {
        foreach (var tile in preview.GetOccupiedTiles())
        {
            foreach (var neighbor in grid.GetNeighbors(tile))
            {
                var room = grid.GetRoomAt(neighbor);
                if (room != null && room.Definition.category != RoomCategory.Quarry)
                    return true;
            }
        }
        return false;
    }
}