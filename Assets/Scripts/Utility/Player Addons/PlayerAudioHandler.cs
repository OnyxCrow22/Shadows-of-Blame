using UnityEngine;
using UnityEngine.Events;

public class PlayerAudioHandler : MonoBehaviour
{
    public string sprintSound;
    public string walkSound;
    public string shootSound;
    public string jumpSound;
    public string landSound;
    public string punchSound;

    void OnEnable()
    {
        PlayerMovementSM.OnJumped += HandleJumpSound;
        PlayerMovementSM.OnSprintEnded += HandleSprintStop;
        PlayerMovementSM.OnSprintPressed += HandleSprintSound;
        PlayerMovementSM.OnLanded += HandleLandSound;
        PlayerMovementSM.OnPunch += HandlePunchSound;
        PlayerMovementSM.OnShot += HandleShootSound;
    }   

    void OnDisable()
    {
        PlayerMovementSM.OnJumped -= HandleJumpSound;
        PlayerMovementSM.OnSprintEnded -= HandleSprintStop;
        PlayerMovementSM.OnSprintPressed -= HandleSprintSound;
        PlayerMovementSM.OnLanded -= HandleLandSound;
        PlayerMovementSM.OnPunch -= HandlePunchSound;
        PlayerMovementSM.OnShot -= HandleShootSound;
    }

    public void HandleSprintSound()
    {
        AudioManager.manager.Play(sprintSound);
    }

    public void HandleSprintStop()
    {
        AudioManager.manager.Stop(sprintSound);
    }

    public void HandleJumpSound()
    {
        AudioManager.manager.Play(jumpSound);
    }

    public void HandleLandSound()
    {
        AudioManager.manager.Stop(jumpSound);
    }

    public void HandleWalkSound()
    {
        AudioManager.manager.Play(walkSound);
    }

    public void HandleWalkEnd()
    {
        AudioManager.manager.Stop(walkSound);
    }

    public void HandleShootSound()
    {
        AudioManager.manager.Play(shootSound);
    }

    public void HandlePunchSound()
    {
        AudioManager.manager.Play(punchSound);
    }
}