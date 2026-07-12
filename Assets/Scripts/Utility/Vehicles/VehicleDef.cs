using UnityEngine;

public class VehicleDefinition : MonoBehaviour
{
    [Header("Interaction Points")]
    public Transform seat;
    public Transform exitPoint;

    [Header("Camera")]
    public GameObject vehicleCamera;

    [Header("Animation")]
    public Animator doorAnimator;

    [Header("Collision")]
    public Collider vehicleCollider;

    [Header("Vehicle Systems")]
    public VehicleState vehicleState;
    public VehiclePhysicsController physicsController;
    public VehicleUIController uiController;
}
