using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class BuildController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GridManager gridManager;
    [SerializeField] private Camera mainCamera;
    
    [Header("Preview Visuals")]
    [SerializeField] private Material validPreviewMaterial;
    [SerializeField] private Material invalidPreviewMaterial;
    
    private BuildState _state = BuildState.None;
    private BuildPreview _preview;
    private GameObject _previewObject;
    private BuildPreviewVisual _previewVisual;
    
    #region Public API
    public void StartPlacing(RoomDefinition room)
    {
        CancelBuild();

        _preview = new BuildPreview(room);
        
        _previewObject = Instantiate(room.prefab);
        _previewObject.name = $"PREVIEW_{room.roomId}";
        
        _previewVisual = _previewObject.AddComponent<BuildPreviewVisual>();
        _previewVisual.Initialize(validPreviewMaterial, invalidPreviewMaterial);
        _previewVisual.SetValidity(false);
        
        PreviewUtility.DisableGameplayComponents( _previewObject);
        
        _state = BuildState.Placing;
    }

    public void CancelBuild()
    {
        if (_previewObject != null) Destroy(_previewObject);

        _previewObject = null;
        _previewVisual = null;
        _preview = null;
        _state = BuildState.None;
    }

    public void EnterDestroyMode()
    {
        CancelBuild();
        _state = BuildState.Destroying;
    }
    #endregion

    private void Update()
    {
        switch (_state)
        {
            case BuildState.Placing:
                UpdatePlacement();
                break;
            
            case BuildState.Destroying:
                UpdateDestroy();
                break;
        }
    }
    
    #region Placement
    private void UpdatePlacement()
    {
        if (_preview == null || _previewObject == null) return;

        var gridPosition = GetMouseGridPosition();
        _preview.SetOrigin(gridPosition);
        
        _previewObject.transform.position = gridManager.GridToWorld(gridPosition);
        
        _previewObject.transform.rotation = Quaternion.Euler(0, _preview.Rotation, 0);

        var valid = ValidatePlacement();
        SetPreviewVisual(valid);
        
        if (Mouse.current.leftButton.wasPressedThisFrame && valid)
        {
            PlaceRoom();
        }
    }

    private bool ValidatePlacement()
    {
        return _preview.Room.placementRules.All(rule => rule.IsValid(_preview, gridManager));
    }

    private void PlaceRoom()
    {
        var roomGo = Instantiate(
            _preview.Room.prefab,
            _previewObject.transform.position,
            _previewObject.transform.rotation
            );
        
        var instance = roomGo.AddComponent<RoomInstance>();

        var tiles = _preview.GetOccupiedTiles();
        instance.Initialize(
            _preview.Room,
            _preview.Origin,
            _preview.Rotation,
            tiles
            );
        
        gridManager.OccupyTiles(instance, tiles);
        SpawnInteractionPoints(instance);
        
        CancelBuild();
    }
    #endregion
    
    #region Destroy
    private void UpdateDestroy()
    {
        if (!Mouse.current.leftButton.wasPressedThisFrame) return;

        var pos = GetMouseGridPosition();
        var room = gridManager.GetRoomAt(pos);

        if (room != null)
        {
            gridManager.FreeTiles(room.OccupiedTiles);
            Destroy(room.gameObject);
        }
    }
    #endregion
    
    #region Utility
    private GridPosition GetMouseGridPosition()
    {
        var ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        var plane = new Plane(Vector3.up, Vector3.zero);

        if (plane.Raycast(ray, out var distance))
        {
            var worldPos = ray.GetPoint(distance);
            return gridManager.WorldToGrid(worldPos);
        }

        return default;
    }

    private void SetPreviewVisual(bool valid)
    {
        _previewObject.SetActive(valid);
    }

    private void SpawnInteractionPoints(RoomInstance room)
    {
        foreach (var def in room.Definition.InteractionPoints)
        {
            var go = new GameObject($"IP_{def.id}"); 
            go.transform.SetParent(room.transform); 
            go.AddComponent<InteractionPoint>().Initialize(def);
        }
    }
    #endregion
}