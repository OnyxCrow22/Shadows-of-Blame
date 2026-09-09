using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Pool;

public class ThrowableWeapon : MonoBehaviour
{
    [Header("Throw Settings")]
    public GameObject grenadePrefab;
    public Transform throwPoint;
    public float throwForce = 40f;
    public float cooldown = 20f;

    private float cooldownTimer = 0f;

    [Header("References")]
    public PlayerInput playerInput;
    public WeaponManager weaponManager;   // NEW: central weapon system

    private IObjectPool<GameObject> GrenadePool;

    public float CooldownRemaining => cooldownTimer;
    public float CooldownNormalise => Mathf.Clamp01(cooldownTimer / cooldown);

    private void Awake()
    {
        InitialisePool();
    }

    public void InitialisePool()
    {
        GrenadePool = new ObjectPool<GameObject>(createFunc: () => Instantiate(grenadePrefab),
            actionOnGet: (g) => g.SetActive(true),
            actionOnRelease: (g) => g.SetActive(false),
            actionOnDestroy: (g) => Destroy(g),
            collectionCheck: false,
            defaultCapacity: 5,
            maxSize: 15);
    }    

    private void Update()
    {
        if (cooldownTimer > 0f)
            cooldownTimer -= Time.deltaTime;
    }

    public void OnThrow(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;

        // Block if weapon wheel open
        if (weaponManager.IsWheelOpen) return;

        // Block if holding a gun
        if (weaponManager.CurrentWeaponType == WeaponType.Gun) return;

        // Block if on cooldown
        if (cooldownTimer > 0f) return;

        ThrowGrenade();
    }

    private void ThrowGrenade()
    {
        // Spawn grenade
        GameObject g = GrenadePool.Get();

        g.transform.SetPositionAndRotation(throwPoint.position, throwPoint.rotation);

        // Apply force
        if (g.TryGetComponent(out Rigidbody rb))
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            rb.AddForce(throwPoint.forward * throwForce, ForceMode.VelocityChange);
        }

        // Start cooldown
        cooldownTimer = cooldown;

        // Notify animation system
        weaponManager.TriggerGrenadeThrowAnimation();
    }

    public void ReleaseGrenade(GameObject grenade)
    {
        GrenadePool.Release(grenade);
    }
}
