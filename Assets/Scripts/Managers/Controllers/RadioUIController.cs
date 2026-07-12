using UnityEngine;
using TMPro;

public class RadioUIController : MonoBehaviour
{
    public TextMeshProUGUI songName;

    public void UpdateSongName(AudioClip clip)
    {
        songName.text = clip != null ? clip.name : "";
    }
}
