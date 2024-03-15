using UnityEngine;
using TMPro;

public class Gun : MonoBehaviour
{
    // Gun statistics
    public int damage;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap, totalAmmo;
    public bool allowButtonHold;
    int bulletsLeft, bulletsShot, bullet;

    // bools
    bool shooting, readyToShoot, reloading, aiming;

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
        if (allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading || Input.GetMouseButton(0) && bulletsLeft == 0 && !reloading)
        {
            ReloadGun();
        }

        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            bulletsShot = bulletsPerTap;
            ShootGun();
        }

        if (Input.GetMouseButton(1) && !aiming)
        {
            Aiming();
            aiming = true;
        }

        if (Input.GetMouseButton(0) && aiming)
        {
            shooting = Input.GetMouseButton(0);
            playsm.anim.SetBool("shoot", true);
            shooting = true;
        }

        if (!Input.GetMouseButton(1) && aiming && Input.GetMouseButton(0))
        {
            aiming = false;
            fpsCam.gameObject.SetActive(true);
            aimCam.gameObject.SetActive(false);
            playsm.anim.SetBool("aiming", false);
        }

        else if (!Input.GetMouseButton(1) && aiming)
        {
            fpsCam.gameObject.SetActive(true);
            aimCam.gameObject.SetActive(false);
            aiming = false;
            playsm.anim.SetBool("aiming", false);
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

        bulletsShot = bulletsPerTap;
        bulletsShot++;
        bulletsPerTap = bulletsShot;


        Invoke("ResetShot", timeBetweenShooting);

        if(bulletsShot > 0 && bulletsLeft > 0)
        {
            Invoke("Shoot", timeBetweenShots);
        }
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
        totalAmmo -= bulletsShot;
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