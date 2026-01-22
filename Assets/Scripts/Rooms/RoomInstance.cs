using UnityEngine;
using System.Collections.Generic;

public class RoomInstance : MonoBehaviour
{
    [SerializeField] private RoomDefinition definition;
    
    public RoomDefinition Definition => definition;
    public GridPosition Origin { get; private set; }
    public int Rotation { get; private set; } // 0, 90, 180, 270

    private List<GridPosition> _occupiedTiles = new List<GridPosition>();
    public IReadOnlyList<GridPosition> OccupiedTiles => _occupiedTiles;

    public void Initialize(
        RoomDefinition def,
        GridPosition origin,
        int rotation,
        IEnumerable<GridPosition> tiles
    )
    {
        definition = def;
        Origin = origin;
        Rotation = rotation;
        
        _occupiedTiles.Clear();
        _occupiedTiles.AddRange(tiles);
    }
}