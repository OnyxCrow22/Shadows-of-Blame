using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponWheelSystem : MonoBehaviour
{
    public WeaponWheelController weapons;
    public GameObject MainUI;
    public GameObject WeaponWheelPanel;

    public static bool isWheelOpen = false;

    [SerializeField]
    private InputActionReference playerControls;

    void OnDisable()
    {
        weapons.CloseWheel();
        playerControls.action.performed -= OnWheelPerformed;
        playerControls.action.canceled -= OnWheelCanceled;
    }

    void OnEnable()
    {
        playerControls.action.performed += OnWheelPerformed;
        playerControls.action.canceled += OnWheelCanceled;
        MainUI.SetActive(true);
        WeaponWheelPanel.SetActive(false);
    }

    public void OnWheelPerformed(InputAction.CallbackContext context) => OpenWheel();
    public void OnWheelCanceled(InputAction.CallbackContext context) => CloseWheel();

    void OpenWheel()
    {
        isWheelOpen = true;
        weapons.WeaponWheel();
        MainUI.SetActive(false);
        WeaponWheelPanel.SetActive(true);
        Time.timeScale = 0.2f; // Slow down time when the wheel is open
    }
    
    void CloseWheel()
    {
        isWheelOpen = false;
        weapons.CloseWheel();
        MainUI.SetActive(true);
        WeaponWheelPanel.SetActive(false);
        Time.timeScale = 1f; // Resume normal time when the wheel is closed
    }
}
