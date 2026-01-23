using UnityEngine;

public class CoreInteractionPoint : InteractionPoint
{
    public override void Interact(GameObject interactor)
    {
        if (!interactor.CompareTag("Enemy")) return;

        // For now: placeholder
        Debug.Log("Interacted with Dungeon Core");

        // Later:
        // - Open dungeon UI
        // - Show stats
        // - Trigger story beats
        // - Upgrade dungeon
    }
}
