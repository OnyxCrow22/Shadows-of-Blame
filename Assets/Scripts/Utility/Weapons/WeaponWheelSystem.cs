using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponWheelSystem : MonoBehaviour
{
    public WeaponWheelController weapons;
    public GameObject MainUI;
    public GameObject WeaponWheelPanel;

    // This checks for the input every frame to open/close the weapon wheel
    private void Update()
    {
        CheckWeaponWheelInput();
    }

    void OnDisable()
    {
        weapons.CloseWheel();
        MainUI.SetActive(true);
        WeaponWheelPanel.SetActive(false);
    }

    void OnEnable()
    {
        MainUI.SetActive(true);
        WeaponWheelPanel.SetActive(false);
    }

    // Now I need to check the key input in a new method
    void CheckWeaponWheelInput()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (!weapons.weaponWheelSelected)
            {
                weapons.WeaponWheel();
                MainUI.SetActive(false);
                WeaponWheelPanel.SetActive(true);
            }
            else
            {
                weapons.CloseWheel();
                MainUI.SetActive(true);
                WeaponWheelPanel.SetActive(false);
            }
        }
    }
}
