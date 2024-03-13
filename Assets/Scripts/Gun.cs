using TMPro;
using UnityEngine;

public class Gun : PlayerMovementSM
{
    // Gun statistics
    public int damage;
    public float timeBetweenFire, spread, range, reloadDelay, timeBetweenShot;
    public int magSize, bulletsPerTap;
    public bool allowHold;
    public int bulletsLeft, bulletsShot;

    // bools
    public bool shooting, readyToFire, reloading;

    // Text
    public TextMeshProUGUI ammoText;

    // Reference
    public Camera playerCam;
    public Transform attackPoint;
    public RaycastHit weaponHit;
    public LayerMask WhatisEnemy, WhatisNPC;
}
