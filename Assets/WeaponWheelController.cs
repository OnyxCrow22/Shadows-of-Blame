using UnityEngine;
using UnityEngine.UI;

public class WeaponWheelController : MonoBehaviour
{
    public Animator anim;
    public bool weaponWheelSelected = false;
    public Image selectedItem;
    public Sprite[] icons;
    public static int weaponID;

    public Gun gun;

    public void WeaponWheel()
    {
        weaponWheelSelected = true;
        anim.SetBool("OpenWeaponWheel", true);
        Time.timeScale = 1f;
        AudioListener.pause = true;

        switch (weaponID)
        {
            case 0:
                selectedItem.sprite = icons[0];
                break;
            case 1:
                selectedItem.sprite = icons[1];
                break;
            case 2:
                selectedItem.sprite = icons[2];
                break;
            case 3:
                break;
        }
    }

    public void CloseWheel()
    {
        weaponWheelSelected = false;
        anim.SetBool("OpenWeaponWheel", false);
        Time.timeScale = 1f;
        AudioListener.pause = false;
    }
}
