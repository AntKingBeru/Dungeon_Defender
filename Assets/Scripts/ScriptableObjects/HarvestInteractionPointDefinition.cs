using UnityEngine;

[CreateAssetMenu(fileName = "HarvestInteractionPointDefinition", menuName = "Scriptable Objects/HarvestInteractionPointDefinition")]
public class HarvestInteractionPointDefinition : InteractionPointDefinition
{
    public override System.Type ComponentType => typeof(HarvestInteractionPoint);
}
