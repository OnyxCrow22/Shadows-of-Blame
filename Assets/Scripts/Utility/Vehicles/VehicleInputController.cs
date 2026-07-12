using UnityEngine;
using UnityEngine.InputSystem;

public class VehicleInputController : MonoBehaviour
{
    public float Throttle { get; private set; }
    public float Steering { get; private set; }
    public float Brake { get; private set; }
    public bool Reverse { get; private set; }

    public void OnAccelerate(InputAction.CallbackContext ctx)
        => Throttle = ctx.ReadValue<float>();

    public void OnBrake(InputAction.CallbackContext ctx)
        => Brake = ctx.ReadValue<float>();

    public void OnSteer(InputAction.CallbackContext ctx)
        => Steering = ctx.ReadValue<float>();

    public void OnReverse(InputAction.CallbackContext ctx)
        => Reverse = ctx.performed;
}
