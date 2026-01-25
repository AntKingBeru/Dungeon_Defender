using System;
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class HarvestInteractionPoint : InteractionPoint
{
    [SerializeField] private RoomQuarryController quarry;

    private ResourceType _resourceType;
    private bool _onCooldown;

    private void Awake()
    {
        GetComponent<Collider>().isTrigger = true;
        _resourceType = quarry.GetResourceType();
    }

    public override void Interact(GameObject interactor)
    {
        if (_onCooldown) return;
        
        if (!interactor.CompareTag("Player")) return;

        var resource = quarry.GetResourceAmount();

        switch (_resourceType)
        {
            case ResourceType.Stone:
                PlayerInventory.Instance.AddResource(ResourceType.Stone, resource);
                break;
            case ResourceType.Wood:
                PlayerInventory.Instance.AddResource(ResourceType.Wood, resource);
                break;
            case ResourceType.Iron:
                PlayerInventory.Instance.AddResource(ResourceType.Iron, resource);
                break;
            case ResourceType.Eggs:
                PlayerInventory.Instance.AddResource(ResourceType.Eggs, resource);
                break;
            default:
                break;
        }
        
        StartCoroutine(Cooldown());
    }
    
    private IEnumerator Cooldown()
    {
        _onCooldown = true;
        yield return new WaitForSeconds(quarry.GetCooldown());
        _onCooldown = false;
    }
}