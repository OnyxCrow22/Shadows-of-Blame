using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponWheelSystem : MonoBehaviour
{
    public WeaponWheelController weapons;
    public GameObject MainUI;
    public GameObject WeaponWheelPanel;

    private void Update()
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
