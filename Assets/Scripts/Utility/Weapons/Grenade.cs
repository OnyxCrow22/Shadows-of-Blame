using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float delay = 3f;
    public float radius = 15f;
    public float force = 700f;
    public float grenadeDamage = 100f;

    public GameObject explosionVFX;

    private float countdown;
    private bool hasExploded = false;

    private void Start()
    {
        countdown = delay;
    }

    private void Update()
    {
        countdown -= Time.deltaTime;

        if (countdown <= 0f && !hasExploded)
        {
            Explode();
            hasExploded = true;
        }
    }

    private void Explode()
    {
        // VFX
        Instantiate(explosionVFX, transform.position, Quaternion.identity);

        // SFX
        if (AudioManager.manager != null)
            AudioManager.manager.Play("GrenadeExplosion");

        // Physics + Damage
        Collider[] hits = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider col in hits)
        {
            // Explosion force
            if (col.attachedRigidbody != null)
            {
                col.attachedRigidbody.AddExplosionForce(force, transform.position, radius);
            }

            // Damage
            if (col.TryGetComponent(out IDamageable dmg))
            {
                dmg.TakeDamage(grenadeDamage);
            }
        }

        // Hide grenade mesh
        MeshRenderer mr = GetComponent<MeshRenderer>();
        if (mr != null)
            mr.enabled = false;

        Destroy(gameObject);
    }
}
