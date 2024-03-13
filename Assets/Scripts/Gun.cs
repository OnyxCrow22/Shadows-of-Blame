using System.Collections;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage;
    public float range;
    public int ammo;
    public int maxAmmo;
    public int spentAmmo;
    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI maxAmmoText;
    public Camera playerCam;
    public bool Rifle;
    public bool Pistol;
    public bool LMG;
    public bool Sniper;


    private void Update()
    {
        ammoText.text = ammo.ToString();
        maxAmmoText.text = maxAmmo.ToString();
        UpdateAmmo();
    }

    public void UpdateAmmo()
    {
        ammo--;
    }
}
