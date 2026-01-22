using UnityEngine;

[CreateAssetMenu(fileName = "QuarryAdjacency", menuName = "Scriptable Objects/PlacementRules/QuarryAdjacency")]
public class QuarryAdjacency : PlacementRule
{
    public override bool IsValid(BuildPreview preview, GridManager grid)
    {
        foreach (var tile in preview.GetOccupiedTiles())
        {
            foreach (var neighbor in grid.GetNeighbors(tile))
            {
                var room = grid.GetRoomAt(neighbor);
                if (room != null && room.Definition.category == RoomCategory.Quarry) return true;
            }
        }

        return false;
    }
}