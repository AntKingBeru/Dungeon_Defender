using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class HarvestInteractionPoint : InteractionPoint
{
    [SerializeField] private RoomQuarryController quarry;
    
    private bool _onCooldown;

    private void Awake()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    public override void Interact(GameObject interactor)
    {
        if (_onCooldown) return;
        
        if (!interactor.CompareTag("Player")) return;

        var stone = quarry.GetStoneAmount();
        
        PlayerInventory.Instance.AddResource(ResourceType.Stone, stone);
        
        StartCoroutine(Cooldown());
    }
    
    private IEnumerator Cooldown()
    {
        _onCooldown = true;
        yield return new WaitForSeconds(quarry.GetCooldown());
        _onCooldown = false;
    }
}