using UnityEngine;

public class CoreBobbing : MonoBehaviour
{
    [Header("Bobbing")]
    [SerializeField] private float bobHeight = 0.5f;
    [SerializeField] private float bobSpeed = 1.5f;
    
    [Header("Emission Pulse")]
    [SerializeField] private Color emissionColor = Color.purple;
    [SerializeField] private float emissionMinIntensity = 1.5f;
    [SerializeField] private float emissionMaxIntensity = 3.5f;
    [SerializeField] private float emissionPulseSpeed = 2f;
    
    private readonly Vector3 _rotationSpeed = new Vector3(0f, 20f, 0f);
    private Vector3 _startPosition;
    private Material _orbMaterial;

    private void Start()
    {
        _startPosition = transform.position;
        
        _orbMaterial = GetComponent<MeshRenderer>().material;
        _orbMaterial.EnableKeyword("_EMISSION");
    }

    private void Update()
    {
        // Bobbing
        var yOffset = Mathf.Sin(Time.time * bobSpeed) * bobHeight;
        transform.position = _startPosition + Vector3.up * yOffset;
        // Rotation
        transform.Rotate(_rotationSpeed * Time.deltaTime, Space.Self);
        // Emission Pulse
        var pulse = Mathf.Sin(Time.time * emissionPulseSpeed) * 0.5f + 0.5f;
        var intensity = Mathf.Lerp(emissionMinIntensity, emissionMaxIntensity, pulse);
        _orbMaterial.SetColor("_EmissionColor", emissionColor * intensity);
    }
}