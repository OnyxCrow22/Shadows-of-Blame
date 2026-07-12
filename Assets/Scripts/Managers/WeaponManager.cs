using UnityEngine;

public enum WeaponType
{
    None,
    Gun,
    Throwable
}

public class WeaponManager : MonoBehaviour
{
    [Header("Current Weapon State")]
    public WeaponType CurrentWeaponType = WeaponType.None;

    [Header("References")]
    public Gun gun;                         // Player gun
    public ThrowableWeapon throwable;       // Grenades / throwables
    public Animator playerAnimator;         // Player animation controller

    [Header("Weapon Wheel")]
    public bool IsWheelOpen = false;

    // Called by UI or input system when weapon wheel opens/closes
    public void SetWheelOpen(bool open)
    {
        IsWheelOpen = open;
    }

    // Called when player equips a gun
    public void EquipGun()
    {
        CurrentWeaponType = WeaponType.Gun;

        if (gun != null)
            gun.gameObject.SetActive(true);

        if (throwable != null)
            throwable.gameObject.SetActive(false);

        TriggerEquipAnimation();
    }

    // Called when player equips a throwable (grenade)
    public void EquipThrowable()
    {
        CurrentWeaponType = WeaponType.Throwable;

        if (gun != null)
            gun.gameObject.SetActive(false);

        if (throwable != null)
            throwable.gameObject.SetActive(true);

        TriggerEquipAnimation();
    }

    // Called when player unequips everything
    public void UnequipAll()
    {
        CurrentWeaponType = WeaponType.None;

        if (gun != null)
            gun.gameObject.SetActive(false);

        if (throwable != null)
            throwable.gameObject.SetActive(false);

        TriggerUnequipAnimation();
    }

    // Animation triggers
    public void TriggerGrenadeThrowAnimation()
    {
        if (playerAnimator != null)
            playerAnimator.SetTrigger("throwGrenade");
    }

    private void TriggerEquipAnimation()
    {
        if (playerAnimator != null)
            playerAnimator.SetTrigger("equipWeapon");
    }

    private void TriggerUnequipAnimation()
    {
        if (playerAnimator != null)
            playerAnimator.SetTrigger("unequipWeapon");
    }
}
