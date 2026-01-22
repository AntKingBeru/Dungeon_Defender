using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private NavMeshAgent agent;

    public void MoveToScreenPoint(Vector2 screenPoint)
    {
        var ray = mainCamera.ScreenPointToRay(screenPoint);
        var plane = new Plane(Vector3.up, Vector3.zero);

        if (plane.Raycast(ray, out var distance))
        {
            var worldPos = ray.GetPoint(distance);
            agent.SetDestination(worldPos);
        }
    }

    public bool IsMoving()
    {
        return agent.hasPath && agent.remainingDistance > agent.stoppingDistance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IDoor>(out var door))
        {
            door.Open();
        }
    }
}