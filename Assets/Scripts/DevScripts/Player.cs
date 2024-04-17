using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IBind<PlayerData>
{

    public string ID { get; set; }

    [SerializeField] PlayerMovementSM playsm;
    [SerializeField] PlayerHealth pHealth;
    [SerializeField] Gun pGun;

    [SerializeField] PlayerData data;

    public void Bind(PlayerData data)
    {
        this.data = data;
        this.data.ID = data.ID;
        transform.rotation = playsm.player.transform.rotation;
        transform.position = playsm.player.transform.position;
        this.data.health = pHealth.health;
        this.data.maxHealth = pHealth.maxHealth;
        this.data.magSize = pGun.magazineSize;
        this.data.totalAmmo = pGun.totalAmmo;
        this.data.bulletShot = pGun.bulletsShot;
        this.data.bulletsLeft = pGun.bulletsLeft;

    }

    void Update()
    {
        data.position = playsm.player.transform.position;
        data.rotation = playsm.player.transform.rotation;
        data.health = pHealth.health;
        data.maxHealth = pHealth.maxHealth;
        data.magSize = pGun.magazineSize;
        data.totalAmmo = pGun.totalAmmo;
        data.bulletShot = pGun.bulletsShot;
        data.bulletsLeft = pGun.bulletsLeft;
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