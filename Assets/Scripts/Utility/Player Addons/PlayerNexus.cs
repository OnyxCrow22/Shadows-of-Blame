using UnityEngine;

public class PlayerNexus : MonoBehaviour
{
    // The home of Harrison's nexus system. This talks to every script connected to Harrison.
    public PlayerState playerState; // The reference towards the PlayerState script, which holds all the player's current states and flags.
    public PlayerMovementSM playerMovementSM; // The reference towards the PlayerMovementSM script, which handles the player's movement and state machine.
    public HealthSystem playerHealth; // The reference towards the HealthSystem script, which manages the player's health and damage.
    public Gun activeGun; // The reference towards the Gun script, which manages the player's currently equipped gun and its functionality.
    public PunchSystem playerPunch; // The reference towards the PunchSystem script, which manages the player's punching functionality.
    public PlayerAudioHandler audioHandler; // For the player's audio.

    void Awake()
    {
        // Initialize the references to the scripts on the player object.
        playerState = GetComponent<PlayerState>();
        playerMovementSM = GetComponent<PlayerMovementSM>();
        playerHealth = GetComponent<HealthSystem>();
        activeGun = GetComponentInChildren<Gun>();
        playerPunch = GetComponent<PunchSystem>();
    }

    public void SaveGame()
    {
        float currentHealthVal = playerHealth != null ? playerHealth.health : 100f;

        SaveSystem.SavePlayer(transform.position, currentHealthVal);
        Debug.Log("Game Saved!");
    }

    public void LoadGame()
    {
        PlayerData loadData = SaveSystem.LoadPlayer();

        if (loadData != null)
        {
            CharacterController controller = GetComponent<CharacterController>();
            if (controller != null) controller.enabled = false;

            transform.position = loadData.ConvertPosition();

            if (playerHealth != null)
            {
                playerHealth.health = loadData.playerHealth;
                // Refresh UI at some point.
            }

            if (controller != null) controller.enabled = true;

            Debug.Log("Game loaded!");
        }
        else
        {
            Debug.Log("No save game found!");
        }
    }
}
