using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponWheelButtonController : MonoBehaviour
{
    public int ID;
    public string itemName;
    public TextMeshProUGUI itemText;
    public Image selectedImage;
    public Sprite icon;

    private bool selected = false;

    private void Update()
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
        Object.FindAnyObjectByType<WeaponWheelController>().selectedID = ID;


    }

    public void DeSelected()
    {
        selected = false;
        Object.FindAnyObjectByType<WeaponWheelController>().selectedID = ID;


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
