using UnityEngine;

public class BuildPreviewVisual : MonoBehaviour
{
    [Header("Preview Materials")]
    [SerializeField] private Material validMaterial;
    [SerializeField] private Material invalidMaterial;
    
    private Renderer[] _renderers;
    private bool _lastValidity;

    private void Awake()
    {
        _renderers = GetComponentsInChildren<Renderer>(true);
    }

    public void Initialize(Material valid, Material invalid)
    {
        validMaterial = valid;
        invalidMaterial = invalid;
    }
    
    public void SetValidity(bool isValid)
    {
        if (_lastValidity == isValid) return;

        _lastValidity = isValid;
        
        var target = isValid ? validMaterial : invalidMaterial;

        foreach (var render in _renderers)
        {
            render.sharedMaterial = target;
        }
    }
}