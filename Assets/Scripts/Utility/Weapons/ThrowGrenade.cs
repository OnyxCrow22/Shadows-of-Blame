using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ThrowGrenade : MonoBehaviour
{
    public float throwForce = 40;
    public GameObject grenade;
    public PlayerMovementSM playsm;

    void Update()
    {
        InputCheck();
    }

    void InputCheck()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Throw();
            playsm.throwingGrenade = true;
        }

        if (!Input.GetMouseButtonDown(1))
        {
            playsm.throwingGrenade = false;
        }
    }

    void Throw()
    {
        GameObject newGrenade = Instantiate(grenade, transform.position, transform.rotation);
        Rigidbody rb = newGrenade.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);
    }
}
