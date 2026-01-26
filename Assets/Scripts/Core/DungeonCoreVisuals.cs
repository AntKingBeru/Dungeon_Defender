using UnityEngine;

public class DungeonCoreVisuals : MonoBehaviour
{
    [Header("Renderers")]
    [SerializeField] private Renderer glowRenderer;
    [SerializeField] private Renderer crackRenderer;
    [SerializeField] private Light coreLight;

    [Header("Glow")]
    [SerializeField] private float baseEmission = 0.6f;
    [SerializeField] private float maxFlicker = 0.6f;

    [Header("Cracks")]
    [SerializeField] private float maxCrackEmission = 2.5f;
    
    [Header("Destruction")]
    [SerializeField] private CoreDestructionSequence destructionSequence;
    
    private MaterialPropertyBlock _glowPropertyBlock;
    private MaterialPropertyBlock _crackPropertyBlock;
    private float _initialLightIntensity;

    private void Awake()
    {
        _glowPropertyBlock = new MaterialPropertyBlock();
        _crackPropertyBlock = new MaterialPropertyBlock();
        
        if (coreLight != null)
        {
            _initialLightIntensity = coreLight.intensity;
        }
    }

    public void UpdateDamageVisuals(float healthPercent)
    {
        var damage = 1f - healthPercent;
        
        // Flicker increases as health drops
        var flickerStrength = Mathf.Lerp(0f, maxFlicker, damage);
        var flicker = Mathf.PerlinNoise(Time.time * 4f, 0f) * flickerStrength;

        var glowIntensity = baseEmission + flicker;
        
        glowRenderer.GetPropertyBlock(_glowPropertyBlock);
        var glowColor = _glowPropertyBlock.GetColor("_EmissionColor");
        _glowPropertyBlock.SetColor("_EmissionColor", glowColor * glowIntensity);
        glowRenderer.SetPropertyBlock(_glowPropertyBlock);
        
        // Crack visibility after 60% HP
        var crackLevel = Mathf.InverseLerp(0.6f, 0f, healthPercent);
        crackLevel = Mathf.Clamp01(crackLevel);
        
        crackRenderer.gameObject.SetActive(crackLevel > 0f);
        
        crackRenderer.GetPropertyBlock(_crackPropertyBlock);
        _crackPropertyBlock.SetColor("_EmissionColor", Color.white * crackLevel * maxCrackEmission);
        crackRenderer.SetPropertyBlock(_crackPropertyBlock);

        if (coreLight)
        {
            var intensityMultiplier = Mathf.Lerp(1f, 1.4f, damage);
            coreLight.intensity = _initialLightIntensity * intensityMultiplier * (1f + flicker);
        }
    }
    
    public void PlayDestructionSequence()
    {
        // Disable ongoing visual updates
        enabled = false;
        
        // Turn off standard visuals
        glowRenderer.enabled = false;
        crackRenderer.enabled = false;
        if (coreLight) coreLight.enabled = false;
        
        // Start the destruction sequence
        if (destructionSequence)
        {
            StartCoroutine(destructionSequence.Play());
        }
        else
        {
            Debug.LogError("CoreDestructionSequence not assigned!");
        }
    }
}