using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float delay = 3f;
    public float radius = 15f;
    public float force = 700;
    public float grenadeDamage = 100;
    public GameObject explosionVFX;
    public GameObject grenade;
    public bool hasExploded = false;

    private float countdown;

    void Start()
    {
        countdown = delay;
    }

    private void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0 && !hasExploded)
        {
            Explode();
            hasExploded = true;
        }
    }

    public void Explode()
    {
        // Spawn the grenade explosion VFX, along with the grenade itself
        Instantiate(explosionVFX, transform.position, transform.rotation);

        Collider[] cols = Physics.OverlapSphere(transform.position, radius);

        if (AudioManager.manager != null) AudioManager.manager.Play("GrenadeExplosion");

        foreach (Collider nearbyObject in cols)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Add explosion force to nearby rigidbodies
                rb.AddExplosionForce(force, transform.position, radius);
            }
            EnemyHealth damage = nearbyObject.GetComponent<EnemyHealth>();
            if (damage != null)
            {
                damage.LoseHealth(damage.healthLoss + grenadeDamage);
            }

            NPCHealth NPCS = nearbyObject.GetComponent<NPCHealth>();
            if (NPCS != null)
            {
                NPCS.LoseHealth(NPCS.healthLoss + grenadeDamage);
            }
        }

        if (GetComponent<MeshRenderer>()) GetComponent<MeshRenderer>().enabled = false;

        Destroy(gameObject);
    }
}

