using UnityEngine;

public class PunchSystem : MonoBehaviour
{
    public int damage = 10;
    public float punchRange = 8f;
    public float punchCooldown = 0.3f;

    public Transform FOV;
    public LayerMask hittableMask;
    public PlayerMovementSM playsm;

    private float nextPunchTime;

    private void Update()
    {
        if (Time.time >= nextPunchTime)
            CheckInput();
    }

    private void CheckInput()
    {
        if (playsm.attackPressed && !playsm.weapon.gunEquipped)
            Punch();
    }

    private void Punch()
    {
        nextPunchTime = Time.time + punchCooldown;

        Ray ray = new Ray(FOV.position, FOV.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, punchRange, hittableMask))
        {
            IDamageable hittable = hit.collider.GetComponent<IDamageable>();
            if (hittable != null)
            {
                hittable.TakeDamage(damage);
                AudioManager.manager.Play("Punch");
            }
        }
    }
}
