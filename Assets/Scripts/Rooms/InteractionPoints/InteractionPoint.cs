using UnityEngine;

public abstract class InteractionPoint : MonoBehaviour
{
    [SerializeField] private InteractionPointDefinition definition;
    
    public InteractionPointDefinition Definition => definition;

    public virtual void Initialize(InteractionPointDefinition def)
    {
        definition = def;
        transform.localPosition = def.localPosition;
    }
    
    public abstract void Interact(GameObject interactor);
}