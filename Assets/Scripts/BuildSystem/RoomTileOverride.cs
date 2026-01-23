using UnityEngine;
using System.Collections.Generic;

public class RoomTileOverride : MonoBehaviour
{
    [Header("Tile Settings")]
    [SerializeField] private string tileTag = "FloorTile";
    [SerializeField] private LayerMask tileLayer;
    
    [Header("Detection")]
    [SerializeField] private Vector3 overlapBoxSize = new Vector3(12f, 5f, 12f);
    [SerializeField] private Vector3 overlapBoxOffset = Vector3.zero;
    
    private readonly List<GameObject> _disabledTiles = new();

    public void Apply()
    {
        var center = transform.position + overlapBoxOffset;

        var hits = Physics.OverlapBox(
            center,
            overlapBoxSize * 0.5f,
            Quaternion.identity,
            tileLayer
        );

        foreach (var hit in hits)
        {
            var go = hit.gameObject;

            if (!go.CompareTag(tileTag))
                continue;

            if (!go.activeSelf)
                continue;

            go.SetActive(false);
            _disabledTiles.Add(go);
        }
    }

    public void Restore()
    {
        foreach (var tile in _disabledTiles) tile.SetActive(true);
        _disabledTiles.Clear();
    }
    
#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(
            transform.position + overlapBoxOffset,
            overlapBoxSize
        );
    }
#endif
}