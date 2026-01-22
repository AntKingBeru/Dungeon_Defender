using UnityEngine;

[CreateAssetMenu(fileName = "PlacementRule", menuName = "Scriptable Objects/PlacementRule")]
public abstract class PlacementRule : ScriptableObject
{
    public abstract bool IsValid(
        BuildPreview preview,
        GridManager grid
    );
}