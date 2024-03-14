using TMPro;
using UnityEngine;

public class Gun : MonoBehaviour
{
    // Gun statistics
    public int damage;
    public float timeBetweenFire, spread, range, reloadDelay, timeBetweenShot;
    public int magSize, bulletsPerTap, totalAmmo;
    public bool allowHold;
    public int bulletsLeft, bulletsShot;

    // bools
    public bool shooting, readyToFire, reloading;

    // Text
    public TextMeshProUGUI ammoText;

    // Reference
    public Camera playerCam;
    private PlayerMovementSM playsm;
    public Transform attackPoint;
    public RaycastHit weaponHit;
    public LayerMask WhatisEnemy, WhatisNPC;

    private void Awake()
    {
        bulletsLeft = magSize;
        readyToFire = true;
    }

    private void Update()
    {
        ammoText.text = magSize + (" / " + totalAmmo);

        InputCheck();
    }

    void InputCheck()
    {
        if (allowHold)
        {
            shooting = Input.GetKey(KeyCode.Mouse0);
        }
        else
        {
            shooting = Input.GetKeyDown(KeyCode.Mouse0);
        }

        if (Input.GetKeyDown(KeyCode.R) && magSize < totalAmmo && !reloading)
        {
            Reload();
        }

        if (readyToFire && shooting && !reloading && bulletsLeft > 0)
        {
            bulletsShot = bulletsPerTap;
            FireWeapon();
        }
    }

    void FireWeapon()
    {
        readyToFire = false;

        // Weapon spread
        float x = Random.Range(spread, spread);
        float y = Random.Range(spread, spread);

        // Calculate spread
        Vector3 direction = playerCam.transform.forward + new Vector3(x, y, 0);

        // Raycast Check
        if (Physics.Raycast(playerCam.transform.position, direction, out weaponHit, range, WhatisEnemy))
        {
            Debug.Log(weaponHit.collider.name);
        }

        magSize--;
        bulletsShot++;
        bulletsLeft--;

        Invoke("ResetShot", timeBetweenFire);

        if (bulletsShot > 1 && bulletsLeft > 1)
        {
            Invoke("Shoot", timeBetweenShot);
        }
    }

    void ResetShot()
    {
        readyToFire = true;
    }

    void Reload()
    {
        totalAmmo -= bulletsShot;
        Invoke("ReloadFinished", reloadDelay);
        reloading = true;
    }

    void ReloadFinished()
    {
        ammoText.text = magSize + (" / " + totalAmmo);
        magSize = 30;
        bulletsLeft = 30;
        reloading = false;
    }
}
