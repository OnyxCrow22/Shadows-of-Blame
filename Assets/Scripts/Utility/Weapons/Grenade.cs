using UnityEngine;

public class Grenade : MonoBehaviour
{
    [Header("Grenade Settings")]
    public float delay = 3f;
    public float radius = 15f;
    public float force = 700f;
    public float grenadeDamage = 100f;

    [Header("Grenade References")]
    public GameObject explosionVFX;

    private static readonly Collider[] hitColliders = new Collider[32];
    private float countdown;
    private bool hasExploded;

    private void OnEnable()
    {
        countdown = delay;
        hasExploded = false;
    }

    private void Update()
    {
        if (hasExploded) return;

        countdown -= Time.deltaTime;

        if (countdown <= 0f)
        {
            hasExploded = true;
            Explode(transform.position, radius);
        }
    }

    private void Explode(Vector3 centre, float radius)
    {
        // VFX
        if (explosionVFX != null)
            Instantiate(explosionVFX, centre, Quaternion.identity);

        // SFX
        if (AudioManager.manager != null)
            AudioManager.manager.Play("GrenadeExplosion");

        // Physics + Damage
        int numHit = Physics.OverlapSphereNonAlloc(centre, radius, hitColliders);

        for (int i = 0; i < numHit; i++)
        {
            Collider col = hitColliders[i];
            if (col == null) continue;

            Rigidbody rb = col.attachedRigidbody;

            if (rb != null)
            {
                rb.AddExplosionForce(force, centre, radius);
            }

            if (col.TryGetComponent(out IDamageable damage))
            {
                damage.TakeDamage(grenadeDamage);
            }
        }
        Destroy(gameObject);
    }
}
