using UnityEngine;
using System.Collections.Generic;

public class BuildPreview
{
    public RoomDefinition Room { get; private set; }
    public GridPosition Origin { get; private set; }
    public int Rotation { get; private set; }

    public BuildPreview(RoomDefinition room)
    {
        Room = room;
        Rotation = 0;
    }

    public void SetOrigin(GridPosition origin)
    {
        Origin = origin;
    }

    public void Rotate(int direction)
    {
        if (!Room.allowedRotation) return;

        Rotation = (Rotation + direction * 90) % 360;
        if (Rotation < 0) Rotation += 360;
    }

    public IEnumerable<GridPosition> GetOccupiedTiles()
    {
        foreach (var offset in RoomFootprintUtility.GetFootprint(Room, Rotation))
        {
            yield return new GridPosition(
                Origin.x + offset.x,
                Origin.y + offset.y
            );
        }
    }
}