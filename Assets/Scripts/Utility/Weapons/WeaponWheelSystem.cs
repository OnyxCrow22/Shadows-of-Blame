using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponWheelSystem : MonoBehaviour
{
    public WeaponWheelController wheel;
    public WeaponManager weaponManager;

    public GameObject mainUI;
    public GameObject wheelUI;

    public static bool isWheelOpen = false;

    [SerializeField] private InputActionReference wheelAction;

    private void OnEnable()
    {
        wheelAction.action.performed += OnWheelPerformed;
        wheelAction.action.canceled += OnWheelCanceled;

        mainUI.SetActive(true);
        wheelUI.SetActive(false);
    }

    private void OnDisable()
    {
        wheel.CloseWheel();
        wheelAction.action.performed -= OnWheelPerformed;
        wheelAction.action.canceled -= OnWheelCanceled;
    }

    private void OnWheelPerformed(InputAction.CallbackContext ctx)
    {
        OpenWheel();
    }

    private void OnWheelCanceled(InputAction.CallbackContext ctx)
    {
        CloseWheel();
    }

    private void OpenWheel()
    {
        isWheelOpen = true;
        weaponManager.SetWheelOpen(true);

        wheel.OpenWheel();
        mainUI.SetActive(false);
        wheelUI.SetActive(true);

        Time.timeScale = 0.2f;
    }

    private void CloseWheel()
    {
        isWheelOpen = false;
        weaponManager.SetWheelOpen(false);

        wheel.CloseWheel();
        mainUI.SetActive(true);
        wheelUI.SetActive(false);

        Time.timeScale = 1f;
    }
}
