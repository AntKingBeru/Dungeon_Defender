using UnityEngine;
using System.Collections;

public class CoreDestructionSequence : MonoBehaviour
{
    [SerializeField] private GameObject coreMesh;
    [SerializeField] private ParticleSystem overloadVFX;
    [SerializeField] private ParticleSystem explosionVFX;
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private AudioClip rumble;
    [SerializeField] private AudioClip crack;
    [SerializeField] private AudioClip explosion;

    public IEnumerator Play()
    {
        // Phase 1 - Overload
        audioSource.pitch = 0.8f;
        audioSource.PlayOneShot(rumble);
        if (overloadVFX != null) overloadVFX.Play();

        yield return new WaitForSeconds(1.2f);
        
        // Phase 2 - Fracture
        audioSource.pitch = 1.0f;
        audioSource.PlayOneShot(crack);
        if (coreMesh != null) coreMesh.transform.localScale *= 1.1f;
        
        yield return new WaitForSeconds(1.3f);
        
        // Phase 3 - Detonation
        audioSource.pitch = Random.Range(0.95f, 1.05f);
        audioSource.PlayOneShot(explosion);
        if (explosionVFX != null) explosionVFX.Play();
        if (coreMesh != null) coreMesh.SetActive(false);

        if (Camera.main != null)
        {
            var shake = Camera.main.GetComponent<CameraShake>();
            if (shake != null)
            {
                shake.StartCoroutine(shake.Shake(0.4f, 0.2f));
            }
        }

        Time.timeScale = 0.25f;
        yield return new WaitForSecondsRealtime(0.06f);
        Time.timeScale = 1f;
        
        yield return null;
    }
}