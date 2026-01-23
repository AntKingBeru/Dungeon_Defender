using UnityEngine;

public static class PreviewUtility
{
    public static void DisableGameplayComponents(GameObject root)
    {
        foreach (var col in root.GetComponentsInChildren<Collider>(true)) col.enabled = false;
        
        foreach (var rb in root.GetComponentsInChildren<Rigidbody>(true)) rb.isKinematic = true;
        
        foreach (var obstacle in root.GetComponentsInChildren<UnityEngine.AI.NavMeshObstacle>(true))
            obstacle.enabled = false;

        foreach (var mono in root.GetComponentsInChildren<MonoBehaviour>(true))
        {
            if (mono is BuildPreviewVisual) continue;
            
            mono.enabled = false;
        }
    }
}