using UnityEngine;

public class InteractionPoint : MonoBehaviour
{
    [SerializeField] private InteractionPointDefinition definition;
    
    public InteractionPointDefinition Definition => definition;

    public void Initialize(InteractionPointDefinition def)
    {
        definition = def;
        transform.localPosition = def.localPosition;
    }
}