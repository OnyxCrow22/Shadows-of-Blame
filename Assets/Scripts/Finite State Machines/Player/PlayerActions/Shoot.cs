using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        gun.bulletsLeft = gun.magSize;
        gun.readyToFire = true;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        gun.ammoText.text = gun.bulletsLeft + (" / " + gun.magSize).ToString();
        InputCheck();
        Reload();
        FireWeapon();
        ResetShot();
        ReloadFinished();
    }

    void InputCheck()
    {
        if (gun.allowHold)
        {
            gun.shooting = Input.GetKey(KeyCode.Mouse0);
        }
        else
        {
            shooting = Input.GetKeyDown(KeyCode.Mouse0);
        }

        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magSize && !reloading)
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
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        // Calculate spread
        Vector3 direction = playerCam.transform.forward + new Vector3(x, y, 0);

        // Raycast Check
        if (Physics.Raycast(playerCam.transform.position, direction, out weaponHit, range, WhatisEnemy))
        {
            Debug.Log(weaponHit.collider.name);
        }

        bulletsLeft--;
        bulletsShot--;

        Invoke("ResetShot", timeBetweenFire);

        if (bulletsShot > 0 && bulletsLeft > 0)
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
        reloading = true;
        Invoke("ReloadFinished", reloadDelay);
    }

    void ReloadFinished()
    {
        gun.bulletsLeft = gun.magSize;
        gun.reloading = false;
    }
}
