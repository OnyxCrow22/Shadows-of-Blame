using UnityEngine;

public class Gun: MonoBehaviour
{
    [Header("Stats")]
    public float damage = 20f;
    public float spread = 0.05f;
    public float range = 50f;
    public float fireRate = 0.2f;
    public float reloadTime = 1.5f;

    public int magazineSize = 12;
    public int totalAmmo = 60;

    [Header("References")]
    public Transform firePoint;
    public LayerMask hitMask;

    private int bulletsLeft;
    private bool readyToShoot = true;
    private bool reloading = false;

    private void Awake()
    {
        bulletsLeft = magazineSize;
    }

    public void TryShoot()
    {
        if (!readyToShoot || reloading || bulletsLeft <= 0)
            return;

        Shoot();
    }

    private void Shoot()
    {
        readyToShoot = false;

        // Spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        Vector3 direction = firePoint.forward + new Vector3(x, y, 0);
        Ray ray = new Ray(firePoint.position, direction);

        if (Physics.Raycast(ray, out RaycastHit hit, range, hitMask))
        {
            if (hit.collider.TryGetComponent(out IDamageable dmg))
            {
                dmg.TakeDamage(damage);
            }
        }

        bulletsLeft--;
        Invoke(nameof(ResetShot), fireRate);

        if (bulletsLeft <= 0)
            Reload();
    }

    private void ResetShot()
    {
        readyToShoot = true;
    }

    public void Reload()
    {
        if (reloading || totalAmmo <= 0)
            return;

        reloading = true;
        Invoke(nameof(FinishReload), reloadTime);
    }

    private void FinishReload()
    {
        int reloadAmount = Mathf.Min(magazineSize, totalAmmo);
        bulletsLeft = reloadAmount;
        totalAmmo -= reloadAmount;

        reloading = false;
        readyToShoot = true;
    }
}
