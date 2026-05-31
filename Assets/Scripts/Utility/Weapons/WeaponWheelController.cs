using UnityEngine;
using UnityEngine.UI;

public class WeaponWheelController : MonoBehaviour
{
    public bool weaponWheelSelected = false;
    public Image selectedItem;
    public Sprite[] icons;
    public static int weaponID;

    public Gun gun;

    private void Update()
    {
        if (weaponWheelSelected)
        {
            UpdateSelection();
        }

    }

    private void UpdateSelection()
    {
        Vector2 screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Vector2 direction = (Vector2)Input.mousePosition - screenCenter;

        if (direction.magnitude > 20f)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            if (angle < 0) angle += 360f;

            weaponID = Mathf.FloorToInt(angle / 120f);
        }
        selectedItem.sprite = icons[weaponID];
    }

    public void WeaponWheel()
    {
        weaponWheelSelected = true;
        AudioListener.pause = true;
    }

    public void CloseWheel()
    {
        weaponWheelSelected = false;

        ApplyWeaponSwitch();
    }

    private void ApplyWeaponSwitch()
    {
        switch (weaponID)
        {
            case 0:
                gun.gun.SetActive(false);
                break;
            case 1:
                gun.gun.SetActive(true);
                break;
        }
    }
}
