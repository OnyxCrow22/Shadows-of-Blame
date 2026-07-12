using UnityEngine;

public class PoliceChase : PoliceBaseState
{
    private PoliceMovementSM police;

    public PoliceChase(PoliceMovementSM policeMachine) : base("Chase", policeMachine)
    {
        police = policeMachine;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        // Is the player dead?
        if (police.pHealth.health <= 0)
        {
            police.pHealth.StartCoroutine(police.pHealth.PoliceDeath());
            return;
        }

        // 2. Is the wanted level gone, or is the player dead?
        if (PoliceLevel.policeLevels == 0 || police.playsm.health.health <= 0)
        {
            police.PoliceAnim.SetBool("chase", false);
            policeMachine.ChangeState(police.patrolState);
            return;
        }

        // Player is armed near an officer
        if (police.playsm.weapon.gunEquipped)
        {
            police.PoliceAnim.SetBool("shoot", true);
            police.pGun.gameObject.SetActive(true);
            policeMachine.ChangeState(police.fireState);
            return;
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        police.PoliceAI.SetDestination(police.playsm.player.position);

        Vector3 direction = police.playsm.player.position - police.PoliceAI.transform.position;
        direction.y = 0f;
        if (direction != Vector3.zero)
            police.PoliceAI.transform.rotation = Quaternion.Slerp(police.PoliceAI.transform.rotation, Quaternion.LookRotation(direction), 0.1f);
    }
}