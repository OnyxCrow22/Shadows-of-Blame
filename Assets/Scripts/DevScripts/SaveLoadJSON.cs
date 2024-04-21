using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class PlayerData
{
    public int health;
    public int maxHealth;
    public Vector3 position;
    public Quaternion rotation;
    public int magSize;
    public int totalAmmo;
    public int bulletsShot;
    public int bulletsLeft;
}

public class SaveLoadJSON : MonoBehaviour
{
    public static SaveLoadJSON saving;
    PlayerData pData;
    string savePath;
    public float autoSaveTimer = 0;
    public float autoSaveInterval = 120;
    public PlayerMovementSM playsm;

    private void Start()
    {
        pData = new PlayerData();
        playsm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovementSM>();
        pData.health = playsm.health.health;
        pData.maxHealth = playsm.health.maxHealth;
        pData.position = playsm.player.transform.position;
        pData.rotation = playsm.player.transform.rotation;
        pData.magSize = playsm.weapon.magazineSize;
        pData.totalAmmo = playsm.weapon.totalAmmo;
        pData.bulletsShot = playsm.weapon.bulletsShot;
        pData.bulletsLeft = playsm.weapon.bulletsLeft;

        saving.playsm = playsm;

        savePath = Application.persistentDataPath + "/Player.json";
    }

    private void Awake()
    {
        if (SaveLoadJSON.saving == null)
        {
            saving = this;
        }
        else
        {
            Destroy(this);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        pData.health = playsm.health.health;
        pData.maxHealth = playsm.health.maxHealth;
        pData.position = playsm.player.transform.position;
        pData.rotation = playsm.player.transform.rotation;
        pData.magSize = playsm.weapon.magazineSize;
        pData.totalAmmo = playsm.weapon.totalAmmo;
        pData.bulletsShot = playsm.weapon.bulletsShot;
        pData.bulletsLeft = playsm.weapon.bulletsLeft;

        autoSaveTimer += Time.deltaTime;

        if (autoSaveTimer >= autoSaveInterval)
        {
            Autosave();

            autoSaveTimer = 0;
        }
    }

    void Autosave()
    {
        string savePData = JsonUtility.ToJson(pData);
        File.WriteAllText(savePath, savePData);

        Debug.Log("File saved to: " + savePath);
    }

    public void SaveGame()
    {
        string savePData = JsonUtility.ToJson(pData);
        File.WriteAllText(savePath, savePData);

        Debug.Log("File saved to: " + savePath);
    }

    public void LoadGame()
    {
        if (File.Exists(savePath))
        {
            string loadPData = File.ReadAllText(savePath);
            pData = JsonUtility.FromJson<PlayerData>(loadPData);

            Debug.Log("File requested from: " + savePath);

            if (Time.timeScale <= 0)
            {
                Time.timeScale = 1;
                AudioListener.pause = false;
            }

            SceneManager.LoadScene("ShadowsOfBlame");
        }
    }

    public void DeleteGame()
    {
        if (File.Exists(savePath))
        {
            File.Delete(savePath);
        }
    }
}
