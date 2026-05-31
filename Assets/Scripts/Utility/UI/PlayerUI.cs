using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [Header("External References")]
    [SerializeField] private PlayerMovementSM playerSM;
    [SerializeField] private Slider staminaSlider;

    [Header("Visuals")]
    [SerializeField] private bool hideBar = false;
    [SerializeField] private Canvas sliderCanvas;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!playerSM) playerSM = FindAnyObjectByType<PlayerMovementSM>();
        if (!staminaSlider) staminaSlider = GetComponentInChildren<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (staminaSlider == null || playerSM == null) return;

        staminaSlider.value = playerSM.currentStaminaLevel;
    }
}
