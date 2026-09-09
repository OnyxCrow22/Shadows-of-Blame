using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GraphicsController : MonoBehaviour
{
    private MotionBlur motion;
    public Volume masterVol;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (masterVol != null && masterVol.profile != null)
        {
            if (masterVol.profile.TryGet(out motion))
            {
                motion.active = true;
                float savedIntensity = PlayerPrefs.GetFloat("Motion Intensity", 1); // Get the saved value, or default to 0.
                SetMotionIntensity(savedIntensity);
            }
        }
    }

    public void SetMotionIntensity(float val)
    {
        if (motion != null)
        {
            motion.intensity.value = val;
            PlayerPrefs.SetFloat("Motion Intensity", val); // Save the exact value of the intensity.
            PlayerPrefs.Save(); // Save the value to the PlayerPrefs.
        }
    }
}
