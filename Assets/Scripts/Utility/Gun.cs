
using UnityEngine;
using TMPro;

public class Gun : MonoBehaviour
{
    // Gun statistics
    public int damage;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap, totalAmmo, bullet;
    public bool allowButtonHold, aiming;
    int bulletsLeft, bulletsShot;

    // Gun allowed actions
    public int pressCount;
    float aimSens = 100f;
    float xRot = 0f;

    // bools
    bool shooting, readyToShoot, reloading;
    public bool gunEquipped;
    bool pistol, rifle, shotgun;

    // Reference
    public GameObject fpsCam;
    public GameObject aimCam;
    public GameObject gun;
    public GameObject reticle;
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

        if (Input.GetMouseButton(1) && !aiming && gunEquipped)
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

        if (aiming && gunEquipped)
        {
            float mouseX = Input.GetAxis("Mouse X") * aimSens * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * aimSens * Time.deltaTime;

            xRot -= mouseY;
            xRot = Mathf.Clamp(xRot, -90, 90);

            aimCam.transform.localRotation = Quaternion.Euler(xRot, 0, 0);
            playsm.transform.Rotate(Vector3.up * mouseX);


        }
    }

    private void ShootGun()
    {
        readyToShoot = false;

        // Spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        // Direction of spread
        Vector3 direction = aimCam.transform.forward + new Vector3(x, y, 0);

        if (Physics.Raycast(aimCam.transform.position, direction, out hit, range, Enemy))
        {
            Debug.Log(hit.collider.name);

            if (hit.collider.CompareTag("Enemy"))
            {
                Debug.Log("HIT!");
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
