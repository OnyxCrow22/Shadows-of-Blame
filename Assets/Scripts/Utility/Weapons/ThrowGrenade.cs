using UnityEngine;
using UnityEngine.InputSystem;

public class ThrowGrenade : MonoBehaviour
{
    public float throwForce = 40;
    public GameObject grenade;
    public PlayerMovementSM playsm;
    public float throwCooldown = 20;

    float currentThrowCooldown = 0;

    // Private field exposed to the inspector for assigning the grenadeAction.
    [SerializeField]
    private PlayerInput grenadeAction;


    public void OnGrenade(InputAction.CallbackContext context)
    {
        // Is the weapon wheel open?
        if (WeaponWheelSystem.isWheelOpen) return;

        // Is a gun equipped or is the throw cooldown active?
        if (playsm.weapon.gunEquipped) return;
        if (currentThrowCooldown > 0) return;

        if (context.started)
        {
            Throw();
        }
    }

    private void Update()
    {
        // Is the currentThrowCooldown greater than zero?
        if (currentThrowCooldown > 0)
        {
            // Decrease the cooldown timer by Time.deltaTime
            currentThrowCooldown -= Time.deltaTime;
        }
        else
        {
            // Player has not yet thrown the grenade.
            playsm.hasThrownGrenade = false;
        }
    }

    void Throw()
    {
        // Both the throwingGrenade and hasThrownGrenade flags are set to true
        playsm.throwingGrenade = true;
        playsm.hasThrownGrenade = true;

        // Spawn a new grenade object, apply a rigidbody to it, and apply force to throw it forward
        GameObject newGrenade = Instantiate(grenade, transform.position, transform.rotation);
        Rigidbody rb = newGrenade.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);

        // Destroy the grenade object after two seconds.
        Destroy(newGrenade, 2);

        // Reset the grenade throw action, and enforce a cooldown after a 0.5 second delay
        Invoke(nameof(ResetThrow), 0.5f);
    }

    void ResetThrow()
    {
        // Set the throwingGrenade boolean to false.
        playsm.throwingGrenade = false;
    }
}
