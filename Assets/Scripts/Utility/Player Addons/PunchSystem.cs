using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchSystem : MonoBehaviour
{
    [Header("Punching statistics")]
    public int damage;

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
        if (Input.GetKeyDown(KeyCode.Mouse0) && !playsm.weapon.gunEquipped)
        {
            PunchSomething();
        }
    }

    private void PunchSomething()
    {
        float punchRange = 8;
        Ray punchRay = new Ray(FOV.transform.position, FOV.transform.forward);
        Debug.DrawRay(FOV.transform.position, FOV.transform.forward, Color.cyan);
        if (Physics.Raycast(punchRay, out punchHit, punchRange, Enemy) || (Physics.Raycast(punchRay, out punchHit, punchRange, NPC)))
        {
            Debug.Log(punchHit.collider.name);

            if (punchHit.collider.CompareTag("SaintMarysGangMember") || punchHit.collider.CompareTag("SaintMarysGangLeader") || punchHit.collider.CompareTag("NorthbyGangMember") || punchHit.collider.CompareTag("NorthbyGangLeader") || punchHit.collider.CompareTag("NorthBeachGangMember"))
                punchHit.collider.GetComponent<EnemyHealth>().LoseHealth(damage);

            if (punchHit.collider.CompareTag("FemaleNPC") || (punchHit.collider.CompareTag("MaleNPC")))
                Debug.Log("WHACK!");
                punchHit.collider.GetComponent<NPCHealth>().LoseHealth(damage);

            if (punchHit.collider.CompareTag("Police"))
                punchHit.collider.GetComponent<PoliceHealth>().LoseHealth(damage);

            AudioManager.manager.Play("Punch");
        } 
    }
}
