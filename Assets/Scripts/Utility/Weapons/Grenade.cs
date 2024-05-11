using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
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
    public AudioSource explosion;

    [SerializeField] float countdown;

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
        Instantiate(explosionVFX, grenade.transform.position, grenade.transform.rotation);

        Collider[] cols = Physics.OverlapSphere(transform.position, radius);

        explosion.Play();

        foreach (Collider nearbyObject in cols)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(force, transform.position, radius);
            }

            EnemyHealth damage = nearbyObject.GetComponent<EnemyHealth>();
            damage.LoseHealth(damage.healthLoss + grenadeDamage);

            NPCHealth NPCS = nearbyObject.GetComponent<NPCHealth>();
            NPCS.LoseHealth(NPCS.healthLoss + grenadeDamage);
        }
        Destroy(grenade, 2);
    }
}

