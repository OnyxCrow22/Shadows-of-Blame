using UnityEngine;
using UnityEngine.InputSystem;

public class VehicleRadioInput : MonoBehaviour
{
    public RadioManager radio;
    public VehicleState vehicleState;

    public void OnRadioToggle(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed || !vehicleState.IsPlayerInside) return;

        if (radio.radioSource.isPlaying)
            radio.Stop();
        else
            radio.Play();
    }

    public void OnRadioNext(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed || !vehicleState.IsPlayerInside) return;

        radio.NextTrack();
    }
}
