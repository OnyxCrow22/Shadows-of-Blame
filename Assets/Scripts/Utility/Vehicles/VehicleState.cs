using UnityEngine;

public class VehicleState : MonoBehaviour
{
    public Rigidbody rb;

    // Core vehicle states
    public bool IsBraking { get; private set; }
    public bool IsReversing { get; private set; }
    public bool IsOnSide { get; private set; }

    // Player occupancy state
    public bool IsPlayerInside { get; private set; }

    private void Update()
    {
        // Detect if the car has tipped over
        IsOnSide = Mathf.Abs(transform.up.y) < 0.2f;
    }

    // Called by your vehicle entry system
    public void SetPlayerInside(bool inside)
    {
        IsPlayerInside = inside;
    }

    public void SetBraking(bool braking)
    {
        IsBraking = braking;
    }

    public void SetReversing(bool reversing)
    {
        IsReversing = reversing;
    }

    public void FlipCar()
    {
        if (!IsOnSide) return;

        Vector3 pos = transform.position;
        float yaw = transform.eulerAngles.y;

        // Lift the car slightly and reset rotation
        transform.position = new Vector3(pos.x, pos.y + 1f, pos.z);
        transform.rotation = Quaternion.Euler(0f, yaw, 0f);

        // Reset physics using new API
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}
