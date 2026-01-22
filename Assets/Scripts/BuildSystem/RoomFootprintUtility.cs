using UnityEngine;
using System.Collections.Generic;

public static class RoomFootprintUtility
{
    public static IEnumerable<Vector2Int> GetFootprint(
        RoomDefinition room,
        int rotation
    )
    {
        var width = room.size.x;
        var height = room.size.y;

        for (var x = 0; x < width; x++)
        {
            for (var y = 0; y < height; y++)
            {
                var pos = new Vector2Int(x, y) - room.offset;
                yield return Rotate(pos, rotation);
            }
        }
    }

    private static Vector2Int Rotate(Vector2Int pos, int rotation)
    {
        switch (rotation)
        {
            case 90: return new Vector2Int(-pos.y, pos.x);
            case 180: return new Vector2Int(-pos.x, -pos.y);
            case 270: return new Vector2Int(pos.y, -pos.x);
            default: return pos;
        }
    }
}