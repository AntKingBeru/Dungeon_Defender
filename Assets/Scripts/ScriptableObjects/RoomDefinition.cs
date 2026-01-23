using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "RoomDefinition", menuName = "Scriptable Objects/RoomDefinition")]
public class RoomDefinition : ScriptableObject
{
    [Header("Identity")]
    public string roomId;
    
    [Header("Visual")]
    public GameObject prefab;

    [Header("Grid Footprint")]
    public Vector2Int size; // width, height in tiles
    public Vector2Int offset;
    public bool allowedRotation = true;
    
    [Header("Classification")]
    public RoomCategory category; // quarry, monster, corridor, etc
    
    [Header("Placement Rules")]
    public List<PlacementRule> placementRules;
    
    [Header("Interaction Points")]
    public List<InteractionPointDefinition> interactionPoints;
    
    [Header("Neighbors")]
    public List<RoomCategory> allowedNeighbors;
}