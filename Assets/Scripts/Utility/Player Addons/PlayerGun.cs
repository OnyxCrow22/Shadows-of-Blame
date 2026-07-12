using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGun : MonoBehaviour
{
    public Gun gun;

    public void OnFire(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
            gun.TryShoot();
    }

    public void OnReload(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
            gun.Reload();
    }
}
