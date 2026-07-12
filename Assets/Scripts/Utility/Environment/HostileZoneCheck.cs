using TMPro;
using UnityEngine;

public class HostileZoneCheck : MonoBehaviour
{
    public TextMeshProUGUI hostileZone;
    public GameObject panel;
    public Animator hZAnim;

    private void Start()
    {
        // Ensure the panel is disabled by default
        if (panel != null) panel.SetActive(false);
        if (hostileZone != null) hostileZone.text = "";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hostileZone.text = "HOSTILE ZONE";
            panel.SetActive(true);
            hZAnim.SetBool("InZone", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hostileZone.text = "";
            panel.SetActive(false);
            hZAnim.SetBool("InZone", false);
        }
    }
}