using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchSystem : MonoBehaviour
{
    [Header("Punching statistics")]
    public int damage;
    public bool allowButtonHold;

    [Header("Booleans")]
    bool punching;

    [Header("Punch References")]
    public GameObject FOV;
    RaycastHit punchHit;
    public LayerMask Enemy, NPC;
    public PlayerMovementSM playsm;
    private void Update()
    {
        InputCheck();
    }

    public void InputCheck()
    {
        if (allowButtonHold) punching = Input.GetKey(KeyCode.Mouse0);
        else punching = Input.GetKeyDown(KeyCode.Mouse0);

        if (punching)
        {
            AudioManager.manager.Play("Punch");
            PunchSomething();
        }
    }

    public void PunchSomething()
    {
        float punchRange = 2;
        Ray punchRay = new Ray(FOV.transform.position, FOV.transform.forward);
        Debug.DrawRay(FOV.transform.position, FOV.transform.forward, Color.cyan);
        if (Physics.Raycast(punchRay, out punchHit, punchRange, Enemy) || (Physics.Raycast(punchRay, out punchHit, punchRange, NPC)))
        {
            Debug.Log(punchHit.collider.name);

            if (punchHit.collider.CompareTag("Enemy") || (punchHit.collider.CompareTag("GangMember") || (punchHit.collider.CompareTag("GangLeader"))))
                punchHit.collider.GetComponent<EnemyHealth>().LoseHealth(damage);

            if (punchHit.collider.CompareTag("FemaleNPC") || (punchHit.collider.CompareTag("MaleNPC")))
                punchHit.collider.GetComponent<NPCHealth>().LoseHealth(damage);
        } 
    }
}
