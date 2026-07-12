using UnityEngine;
using UnityEngine.InputSystem;

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
        GameObject g = Instantiate(grenadePrefab, throwPoint.position, throwPoint.rotation);

        // Apply force
        if (g.TryGetComponent(out Rigidbody rb))
        {
            rb.AddForce(throwPoint.forward * throwForce, ForceMode.VelocityChange);
        }

        // Start cooldown
        cooldownTimer = cooldown;

        // Notify animation system
        weaponManager.TriggerGrenadeThrowAnimation();
    }
}
