using UnityEngine;
using System.Collections.Generic;

public class PlayerInteractionDetector : MonoBehaviour
{
    private readonly List<InteractionPoint> _nearbyPoints;

    public InteractionPoint GetBestInteractionPoint()
    {
        if (_nearbyPoints.Count == 0) return null;
        
        InteractionPoint best = null;

        var bestDist = float.MaxValue;

        foreach (var point in _nearbyPoints)
        {
            if (point == null) continue;
            
            var dist = Vector3.Distance(transform.position, point.transform.position);
            if (!(dist < bestDist)) continue;
            bestDist = dist;
            best = point;
        }

        return best;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<InteractionPoint>(out var point))
        {
            _nearbyPoints.Add(point);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out InteractionPoint point))
        {
            _nearbyPoints.Remove(point);
        }
    }
}