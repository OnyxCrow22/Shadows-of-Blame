using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HostileZoneCheck : MonoBehaviour
{
    public TextMeshProUGUI hostileZone;
    public GameObject player;
    public GameObject panel;
    public Animator hZAnim;
    Collider col;

    private void Start()
    {
        col = GetComponent<Collider>();
        hostileZone.text = "";
    }

    private void Update()
    {
        CheckPlayer();
    }

    void CheckPlayer()
    {
        Vector3 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;

        if (col.bounds.Contains(playerPos))
        {
            hostileZone.text = "HOSTILE ZONE";
            panel.SetActive(true);
            hZAnim.SetBool("InZone", true);
        }
        else if (!col.bounds.Contains(playerPos))
        {
            hostileZone.text = "";
            panel.SetActive(false);
            hZAnim.SetBool("InZone", false);
        }
    }
}
