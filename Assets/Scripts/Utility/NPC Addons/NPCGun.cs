using UnityEngine;

public class NPCGun : MonoBehaviour
{
    public Gun gun;
    public Transform target;

    public float range;

    private void Update()
    {
        if (target == null) return;

        transform.LookAt(target);

        gun.TryShoot();
    }
}
