using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "NoBlockRoomsRule", menuName = "Scriptable Objects/PlacementRules/NoBlockRoomsRule")]
public class NoBlockRoomsRule : PlacementRule
{
    public override bool IsValid(BuildPreview preview, GridManager grid)
    {
        return preview.GetOccupiedTiles().All(tile => !grid.IsOccupied(tile));
    }
}
