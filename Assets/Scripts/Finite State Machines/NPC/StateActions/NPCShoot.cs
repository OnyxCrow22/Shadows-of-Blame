using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCShoot : NPCBaseState
{
    private NPCMovementSM AI;
    private WeaponManager weaponManager;

    public NPCShoot(NPCMovementSM npcStateMachine) : base("Shoot", npcStateMachine)
    {
        AI = npcStateMachine;
        weaponManager = npcStateMachine.GetComponent<WeaponManager>();
    }

    public override void Enter()
    {
        base.Enter();

        // Aggressive NPCs will stand their ground or move tactically while firing
        if (AI.NPC.isOnNavMesh)
        {
            AI.NPC.isStopped = true;
        }
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        // Has the civilian died?
        if (AI.nHealth.health <= 0)
        {
            // Needs to die.
            return;
        }

        // Has the player died?
        if (AI.nHealth.health > 0)
        {
            AI.NPCAnim.SetBool("shoot", false);
            AI.NPCAnim.SetBool("playerDead", true);
            AI.hiddenGun.SetActive(false);

            if (AI.NPC.isOnNavMesh)
            {
                AI.NPC.isStopped = false;
            }

            AudioManager.manager.Stop("shootGun");
            AudioManager.manager.Play("walk");
            AI.isWalking = true;
            AI.isShooting = false;

            npcStateMachine.ChangeState(AI.walkingState);
            return;
        }

        float DistToPlayer = Vector3.Distance(AI.NPC.transform.position, AI.player.transform.position);

        // Has the player put their gun away?
        if (weaponManager.CurrentWeaponType != WeaponType.Gun && DistToPlayer >= 50f)
        {
            AI.NPCAnim.SetBool("shoot", false);
            AI.hiddenGun.SetActive(false);

            if (AI.NPC.isOnNavMesh)
            {
                AI.NPC.isStopped = false;
            }

            AudioManager.manager.Stop("shootGun");
            AudioManager.manager.Play("sprinting");
            AI.isWalking = true;
            AI.isShooting = false;

            npcStateMachine.ChangeState(AI.walkingState);
            return;
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();

        // Safe target orientation tracking vectors locked strictly to horizontal plane
        Vector3 direction = AI.player.transform.position - AI.NPC.transform.position;
        direction.y = 0f;

        if (direction != Vector3.zero)
        {
            AI.NPC.transform.rotation = Quaternion.Slerp(AI.NPC.transform.rotation, Quaternion.LookRotation(direction), 0.1f);
        }
    }
}