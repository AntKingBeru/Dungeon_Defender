using UnityEngine;

public class DungeonCoreController : MonoBehaviour
{
    [Header("Bobbing")]
    [SerializeField] private float bobHeight = 0.25f;
    [SerializeField] private float bobSpeed = 1.2f;
    
    [Header("Rotation")]
    [SerializeField] private float rotationSpeed = 10f;
    
    [Header("Glow")]
    [SerializeField] private Renderer glowRenderer;
    [SerializeField] private float baseEmissionIntensity = 0.6f;
    [SerializeField] private float pulseAmplitude = 0.15f;
    [SerializeField] private float pulseSpeed = 0.6f;
    [SerializeField] private float colorCycleSpeed = 0.05f;
    [SerializeField] private Light coreLight;

    private Vector3 _startPos;
    private MaterialPropertyBlock _propertyBlock;
    private float _colorTimer;

    private readonly Color[] _coreColors =
    {
        Color.red,
        Color.magenta,
        Color.blue,
        Color.green,
        Color.yellow,
        Color.orange
    };

    private void Awake()
    {
        _startPos = transform.localPosition;
        _propertyBlock = new MaterialPropertyBlock();
    }

    private void Update()
    {
        HandleBobbing();
        HandleRotation();
        HandleColorCycle();
    }

    private void HandleBobbing()
    {
        var yOffset = Mathf.Sin(Time.time * bobSpeed) * bobHeight;
        transform.localPosition = _startPos + Vector3.up * yOffset;
    }

    private void HandleRotation()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);
    }

    private void HandleColorCycle()
    {
        _colorTimer += Time.deltaTime * colorCycleSpeed;
        
        var time = Mathf.PingPong(_colorTimer, _coreColors.Length);
        var indexA = Mathf.FloorToInt(time);
        var indexB = Mathf.Clamp(indexA + 1, 0, _coreColors.Length - 1);
        var lerp = time - indexA;
        
        var currentColor = Color.Lerp(_coreColors[indexA], _coreColors[indexB], lerp);
        
        var pulse = Mathf.Sin(Time.time * pulseSpeed) * pulseAmplitude;
        var intensity = baseEmissionIntensity + pulse;
        
        var emission = currentColor * Mathf.Max(0f, intensity);
        
        glowRenderer.GetPropertyBlock(_propertyBlock);
        _propertyBlock.SetColor("_EmissionColor", emission);
        glowRenderer.SetPropertyBlock(_propertyBlock);

        if (coreLight) coreLight.color = currentColor;
    }
}