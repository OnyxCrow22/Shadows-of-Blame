using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ThrowGrenade : MonoBehaviour
{
    public float throwForce = 40;
    public GameObject grenade;
    public PlayerMovementSM playsm;
    public float delay = 20;

    private void Update()
    {
        InputCheck();   
    }

    void InputCheck()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Throw();
            playsm.throwingGrenade = true;
            playsm.hasThrownGrenade = true;
        }

        if (!Input.GetMouseButtonDown(1))
        {
            playsm.throwingGrenade = false;
        }

        delay -= Time.deltaTime;

        if (delay <= 0)
        {
            playsm.hasThrownGrenade = false;
            delay = 20;
        }
    }

    void Throw()
    {
        GameObject newGrenade = Instantiate(grenade, transform.position, transform.rotation);
        Rigidbody rb = newGrenade.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);
    }
}
