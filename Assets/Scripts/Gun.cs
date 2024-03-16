using UnityEngine;
using TMPro;

public class Gun : MonoBehaviour
{
    // Gun statistics
    public int damage;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap, totalAmmo, bullet;
    public bool allowButtonHold;
    int bulletsLeft, bulletsShot;

    // Gun allowed actions
    public int pressCount;

    // bools
    bool shooting, readyToShoot, reloading, aiming;
    public bool gunEquipped;
    bool pistol, rifle, shotgun;

    // Reference
    public Camera fpsCam;
    public Camera aimCam;
    public GameObject gun;
    public Transform attackPoint;
    public RaycastHit hit;
    public LayerMask Enemy;
    public PlayerMovementSM playsm;
    public TextMeshProUGUI ammoText;

    private void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
        aiming = false;
    }

    private void Update()
    {
        InputCheck();

        ammoText.SetText(bulletsLeft + " / " + totalAmmo);
    }

    private void InputCheck()
    {
        if (allowButtonHold && gunEquipped) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading || Input.GetMouseButton(0) && bulletsLeft == 0 && !reloading)
        {
            // Reloads the gun, takes the totalAmmo away from how many shots were fired, and resets the bullet and bulletsShot count to zero.
            ReloadGun();
            totalAmmo -= bulletsShot;
            bulletsShot = 0;
            bullet = 0;
        }

        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            bulletsShot = bulletsPerTap;
            ShootGun();
        }

        if (Input.GetMouseButton(1) && !aiming)
        {
            // Aims the gun.
            Aiming();
            aiming = true;
        }

        if (Input.GetMouseButton(0) && aiming && gunEquipped)
        {
            shooting = Input.GetMouseButton(0);
            playsm.anim.SetBool("shoot", true);
            shooting = true;
        }

        if (!Input.GetMouseButton(1) && aiming && gunEquipped && Input.GetMouseButton(0))
        {
            aiming = false;
            fpsCam.gameObject.SetActive(true);
            aimCam.gameObject.SetActive(false);
            playsm.anim.SetBool("aiming", false);
        }

        else if (!Input.GetMouseButton(1) && aiming && gunEquipped)
        {
            fpsCam.gameObject.SetActive(true);
            aimCam.gameObject.SetActive(false);
            aiming = false;
            playsm.anim.SetBool("aiming", false);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && gunEquipped && pressCount == 1)
        {
            gun.SetActive(false);
            gunEquipped = false;
            pressCount = 0;
            ammoText.gameObject.SetActive(false);
        }
    }

    private void ShootGun()
    {
        readyToShoot = false;

        // Spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        // Direction of spread
        Vector3 direction = fpsCam.transform.forward + new Vector3(x, y, 0);

        if (Physics.Raycast(fpsCam.transform.position, direction, out hit, range, Enemy))
        {
            Debug.Log(hit.collider.name);

            if (hit.collider.CompareTag("Enemy"))
            {
                
            }
        }
        bulletsLeft--;

        bulletsShot = bullet;
        bulletsShot++;
        bullet = bulletsShot;


        Invoke("ResetShot", timeBetweenShooting);
    }

    private void ResetShot()
    {
        readyToShoot = true;
    }

    private void ReloadGun()
    {
        playsm.anim.SetBool("reloading", true);
        reloading = true;
        readyToShoot = false;
        Invoke("ReloadFinished", reloadTime);
    }

    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        playsm.anim.SetBool("reloading", false);
        reloading = false;
        readyToShoot = true;
    }

    private void Aiming()
    {
        fpsCam.gameObject.SetActive(false);
        aimCam.gameObject.SetActive(true);
        playsm.anim.SetBool("aiming", true);
    }
}