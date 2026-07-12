using UnityEngine;
using UnityEngine.UI;

public class WeaponWheelController : MonoBehaviour
{
    public Image selectedItem;
    public Sprite[] icons;

    public int selectedID = 0;
    public WeaponManager weaponManager;

    private bool wheelOpen = false;

    private void Update()
    {
        if (wheelOpen)
            UpdateSelection();
    }

    public void OpenWheel()
    {
        wheelOpen = true;
    }

    public void CloseWheel()
    {
        wheelOpen = false;
        ApplyWeaponSelection();
    }

    private void UpdateSelection()
    {
        Vector2 center = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Vector2 dir = (Vector2)Input.mousePosition - center;

        if (dir.magnitude > 20f)
        {
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            if (angle < 0) angle += 360f;

            selectedID = Mathf.FloorToInt(angle / 120f);
        }

        selectedItem.sprite = icons[selectedID];
    }

    private void ApplyWeaponSelection()
    {
        switch (selectedID)
        {
            case 0:
                weaponManager.UnequipAll();
                break;

            case 1:
                weaponManager.EquipGun();
                break;

            case 2:
                weaponManager.EquipThrowable();
                break;
        }
    }
}
