using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class RoomBounds : MonoBehaviour
{
    public RoomInstance Room { get; private set; }
    
    private void Awake()
    {
        Room = GetComponentInParent<RoomInstance>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RoomEvents.RaiseEntered(Room);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RoomEvents.RaiseExited(Room);
        }
    }
}