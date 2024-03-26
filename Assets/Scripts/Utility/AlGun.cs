using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlGun : MonoBehaviour
{
    // AI Gun Statistics
    public float damage;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap, totalAmmo, bullet;
    int bulletsLeft;

    // Gun Bools
    bool readyToShoot, reloading;

    // Reference
    public GameObject eGun;
    public GameObject target;
    public RaycastHit eHit;
    public LayerMask Player;
    public EnemyMovementSM esm;

    private void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }

    private void Update()
    {
        if (readyToShoot && !reloading && bulletsLeft > 0)
        {
            ShootGun();
        }
    }

    private void ShootGun()
    {
        readyToShoot = false;
        Invoke("ResetShot", timeBetweenShooting);
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        Vector3 direction = target.transform.forward + new Vector3(x, y, 0);

        if (Physics.Raycast(target.transform.position, direction, out eHit, range, Player))
        {
            Debug.Log(eHit.collider.name);

            if (eHit.collider.CompareTag("Player"))
            {
                PlayerHealth pHealth = eHit.collider.GetComponent<PlayerHealth>();
                pHealth.LoseHealth(esm.health.healthLoss);
                Debug.Log($"You was hit by {esm.enemy}");
            }
        }
        bulletsLeft--;

        if (bulletsLeft <= 0)
        {
            ReloadGun();
        }
    }

    private void ResetShot()
    {
        readyToShoot = true;
    }

    private void ReloadGun()
    {
        if (totalAmmo > 0 && !reloading)
        {
            esm.eAnim.SetBool("reloading", true);
            reloading = true;
            readyToShoot = false;
            Invoke("ReloadFinished", reloadTime);
        }
    }

    private void ReloadFinished()
    {
        int bulletsReloaded = Mathf.Min(magazineSize, totalAmmo);
        bulletsLeft = bulletsReloaded;
        totalAmmo -= bulletsReloaded;
        esm.eAnim.SetBool("reloading", false);
        reloading = false;
        readyToShoot = true;
    }
}
