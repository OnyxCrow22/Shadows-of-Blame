using UnityEngine;
using UnityEngine.InputSystem;

public class VehicleRadioInput : MonoBehaviour
{
    public RadioManager radio;
    public RadioUIController ui;
    public VehicleState vehicleState;

    public void OnRadioToggle(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed || !vehicleState.IsPlayerInside) return;

        if (radio.radioSource.isPlaying)
            radio.Stop();
        else
            radio.Play();

        ui.UpdateSongName(radio.GetCurrentTrack());
    }

    public void OnRadioNext(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed || !vehicleState.IsPlayerInside) return;

        radio.NextTrack();
        ui.UpdateSongName(radio.GetCurrentTrack());
    }
}
