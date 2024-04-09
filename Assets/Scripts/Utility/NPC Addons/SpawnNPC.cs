using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNPC : MonoBehaviour
{
    public GameObject[] WalkAI;
    public int AICount;
    public LayerMask pavement;

    void Start()
    {
        StartCoroutine(Spawn());
    }

    public IEnumerator Spawn()
    {
        int AICount = 0;
        while (AICount < 75)
        {
            RaycastHit hitInfo;
            Vector2 mousePos = Input.mousePosition;
            Ray originPoint = Camera.main.ScreenPointToRay(mousePos);
            if(Physics.Raycast(originPoint, out hitInfo, pavement))
            {
                if (hitInfo.collider.CompareTag("Pavement"))
                {
                    int RandomIndex = Random.Range(0, WalkAI.Length);
                    Instantiate(WalkAI[RandomIndex], hitInfo.point, Quaternion.identity);
                    yield return new WaitForSeconds(0.25f);
                    AICount++;
                }
            }
        }
        if (AICount > 75)
        {
            StopCoroutine(Spawn());
        }
    }
}
