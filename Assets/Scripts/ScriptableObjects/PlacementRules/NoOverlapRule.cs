using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "NoOverlapRule", menuName = "Scriptable Objects/PlacementRules/NoOverlapRule")]
public class NoOverlapRule : PlacementRule
{
    public override bool IsValid(BuildPreview preview, GridManager grid)
    {
        return preview.GetOccupiedTiles().All(tile => !grid.IsOccupied(tile));
    }
}