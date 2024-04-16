using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IBind<PlayerData>
{
    public PlayerHealth pHealth;
    public PlayerMovementSM playsm;
    public Gun pGun;
    public string ID { get; set; }

    [SerializeField] PlayerData data;

    public void Bind(PlayerData data)
    {
        this.data = data;
        this.data.ID = ID;
        transform.rotation = playsm.player.transform.rotation;
        transform.position = playsm.player.transform.position;
        data.health = pHealth.health;
        data.maxHealth = pHealth.maxHealth;
        data.magSize = pGun.magazineSize;
        data.totalAmmo = pGun.totalAmmo;
        data.bulletShot = pGun.bulletsShot;
        data.bulletsLeft = pGun.bulletsLeft;
   
    }

    void Update()
    {
        playsm.player.transform.position = transform.position;
        playsm.player.transform.rotation = transform.rotation;
        pHealth.health = data.health;
        pHealth.maxHealth = data.maxHealth;
        pGun.magazineSize = data.magSize;
        pGun.totalAmmo = data.totalAmmo;
        pGun.bulletsShot = data.bulletShot;
        pGun.bulletsLeft = data.bulletsLeft;
    }
}

[Serializable]
public class PlayerData : ISavable
{
    public string ID { get; set; }
    public float health;
    public float maxHealth;
    public Vector3 position;
    public Quaternion rotation;
    public int magSize;
    public int totalAmmo;
    public int bulletsLeft;
    public int bulletShot;
}
