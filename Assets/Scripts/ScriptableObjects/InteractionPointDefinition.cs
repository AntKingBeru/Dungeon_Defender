using UnityEngine;

[CreateAssetMenu(menuName = "Interaction/Point")]
public abstract class InteractionPointDefinition : ScriptableObject
{
    public string id;
    public Vector3 localPosition;
    public abstract System.Type ComponentType { get; }
}