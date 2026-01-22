using UnityEngine;
using System.Collections.Generic;

public class GridManager : MonoBehaviour
{
    public const int TileSize = 12;

    private Dictionary<GridPosition, RoomInstance> _occupiedTiles = new Dictionary<GridPosition, RoomInstance>();
    
    #region Conversion
    public GridPosition WorldToGrid(Vector3 worldPosition)
    {
        return new GridPosition(
            Mathf.FloorToInt(worldPosition.x / TileSize),
            Mathf.FloorToInt(worldPosition.z / TileSize)
        );
    }

    public Vector3 GridToWorld(GridPosition gridPosition)
    {
        return new Vector3(
            gridPosition.x * TileSize,
            0,
            gridPosition.y * TileSize
        );
    }
    #endregion
    
    #region Occupancy

    public bool IsOccupied(GridPosition position)
    {
        return _occupiedTiles.ContainsKey(position);
    }

    public RoomInstance GetRoomAt(GridPosition position)
    {
        _occupiedTiles.TryGetValue(position, out var room);
        return room;
    }

    public void OccupyTiles(RoomInstance room, IEnumerable<GridPosition> tiles)
    {
        foreach (var tile in tiles) _occupiedTiles[tile] = room;
    }

    public void FreeTiles(IEnumerable<GridPosition> tiles)
    {
        foreach (var tile in tiles) _occupiedTiles.Remove(tile);
    }
    #endregion
    
    #region Neighbors
    public IEnumerable<GridPosition> GetNeighbors(GridPosition position)
    {
        yield return new GridPosition(position.x + 1, position.y);
        yield return new GridPosition(position.x - 1, position.y);
        yield return new GridPosition(position.x, position.y + 1);
        yield return new GridPosition(position.x, position.y - 1);
    }
    #endregion
}