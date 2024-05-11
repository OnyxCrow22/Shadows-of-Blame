using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponWheelButtonController : MonoBehaviour
{
    public int ID;
    public string itemName;
    public TextMeshProUGUI itemText;
    public Image selectedImage;
    bool selected = false;
    public Sprite icon;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
       if (selected)
       {
            selectedImage.sprite = icon;
            itemText.text = itemName;
       }
    }

    public void Selected()
    {
        selected = true;
        WeaponWheelController.weaponID = ID;
    }

    public void DeSelected()
    {
        selected = false;
        WeaponWheelController.weaponID = 0;
    }

    public void HoverEnter()
    {
        itemText.text = itemName;
    }

    public void HoverExit()
    {
        itemText.text = "";
    }
}
