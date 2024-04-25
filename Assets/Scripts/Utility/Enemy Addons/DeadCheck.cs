using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadCheck : MonoBehaviour
{
    public EnemyHealth eHealth;
    public bool isDead = false;

    public void Dead()
    {
        if (eHealth.health == 0)
        {
            isDead = true;
        }
    }
}
