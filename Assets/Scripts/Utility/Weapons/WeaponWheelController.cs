using UnityEngine;
using UnityEngine.UI;

public class WeaponWheelController : MonoBehaviour
{
    public bool weaponWheelSelected = false;
    public Image selectedItem;
    public Sprite[] icons;
    public static int weaponID;

    public Gun gun;

    public void WeaponWheel()
    {
        weaponWheelSelected = true;
        Time.timeScale = 0f;
        AudioListener.pause = true;

        switch (weaponID)
        {
            case 0:
                selectedItem.sprite = icons[0];
                break;
            case 1:
                selectedItem.sprite = icons[1];
                gun.gun.SetActive(true);
                break;
            case 2:
                selectedItem.sprite = icons[2];
                gun.gun.SetActive(false);
                break;
        }
    }

    public void CloseWheel()
    {
        weaponWheelSelected = false;
        Time.timeScale = 1f;
        AudioListener.pause = false;
    }
}
