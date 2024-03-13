using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Shoot : PlayerBaseState
{
    private PlayerMovementSM playsm;
    private Gun gun;

    public Shoot(PlayerMovementSM playerStateMachine) : base("Shoot", playerStateMachine)
    {
        playsm = playerStateMachine;
    }

    public override void Enter()
    {
        base.Enter();

        gun.ammo = 50;
        gun.maxAmmo = 250;
        gun.UpdateAmmo();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        gun.UpdateAmmo();
        gun.damage = 50;

        RaycastHit weaponHit;
        if (Physics.Raycast(gun.playerCam.transform.position, gun.playerCam.transform.forward, out weaponHit, gun.range))
        {
            Debug.Log(weaponHit.transform.name);
            gun.ammoText.text = gun.ammo.ToString();
            gun.maxAmmoText.text = gun.maxAmmo.ToString();
        }

        if (gun.ammo <= 0)
        {
            ReloadWeapon();
        }

        void ReloadWeapon()
        {
            gun.ammo = gun.maxAmmo;
            gun.ammoText.text = gun.ammo.ToString();
            gun.maxAmmoText.text = gun.maxAmmo.ToString();
        }

        if (!Input.GetMouseButton(0))
        {
            playerStateMachine.ChangeState(playsm.idleState);
            playsm.anim.SetBool("Shoot", false);
            playsm.isShooting = false;
        }
    }
}
