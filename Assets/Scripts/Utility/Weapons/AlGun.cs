using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class AlGun : MonoBehaviour
{
    // AI Gun Statistics
    public float damage;
    public float timeBetweenShooting, spread, range = 30f, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap, totalAmmo, bullet;
    int bulletsLeft;

    // Gun Bools
    bool readyToShoot, reloading;

    // Reference
    public GameObject FOV;
    public GameObject eGun;
    public GameObject target;
    public GameObject enemyCam;
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

        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        RaycastHit eHit;
        float rayLength = range;

        Vector3 playerPos = esm.target.transform.position - esm.enemyCam.transform.position;

        playerPos.Normalize();

        Ray shootRay = new Ray(esm.enemyCam.transform.position, playerPos);

        if (Physics.Raycast(shootRay, out eHit, range, Player))
        {
            Debug.Log(eHit.collider.name);

            if (eHit.collider.CompareTag("Player"))

                Gizmos.color = Color.red;
                Gizmos.DrawSphere(eHit.point, 20f);

                eHit.collider.GetComponent<PlayerHealth>().LoseHealth(esm.health.healthLoss);
                Debug.Log($"You was hit by {esm.enemy}");
        }
        bulletsLeft--;

        Invoke("ResetShot", timeBetweenShooting);

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
