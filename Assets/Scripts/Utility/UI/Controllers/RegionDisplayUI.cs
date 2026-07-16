using UnityEngine;
using System.Collections;
using TMPro;

public class RegionDisplayUI : MonoBehaviour
{
    public TextMeshProUGUI cityNameText; // Reference to the TextMeshProUGUI component for displaying the city name
    public TextMeshProUGUI streetNameText; // Reference to the TextMeshProUGUI component for displaying the street name
    public float displayDuration = 3f; // Duration to display the region name
    public float fadeDuration = 1f; // Duration for the fade-out effect
    public Coroutine activeFade;
    private RegionData currentActiveRegion; // Track the currently active region to prevent redundant displays
    private string currentStreetName; // Track the currently active street name to prevent redundant displays

    public void DisplayRegion(RegionData region, string street = "")
    {
        currentActiveRegion = region; // Update the currently active region

        currentStreetName = !string.IsNullOrEmpty(street) ? street : ""; // Update the currently active street name, default to empty if null or empty

        if (activeFade != null)
        {
            StopCoroutine(activeFade);
        }

        if (cityNameText != null || streetNameText != null)
        {
            activeFade = StartCoroutine(NameDisplay());
            cityNameText.text = region.cityName;
            streetNameText.text = currentStreetName; // Display the current street name
        }
    }

    public void HandleRequest()
    {
        if (currentActiveRegion != null)
        {
            DisplayRegion(currentActiveRegion, currentStreetName);
        }
        else
        {
            Debug.LogWarning("No active region to display.");
        }
    }

    public void OnEnable()
    {
        PlayerMovementSM.OnShowRegionPressed += HandleRequest; // Subscribe to the event when the script is enabled
    }

    public void OnDisable()
    {
        PlayerMovementSM.OnShowRegionPressed -= HandleRequest; // Unsubscribe from the event when the script is disabled
    }

    public IEnumerator NameDisplay()
    {
        float elapsedTime = 0f;

        cityNameText.alpha = 0f; // Reset alpha to fully invisible
        streetNameText.alpha = 0f; // Reset alpha to fully invisible

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float percentageComplete = elapsedTime / fadeDuration;

            cityNameText.alpha = Mathf.Lerp(0f, 1f, percentageComplete);
            streetNameText.alpha = Mathf.Lerp(0f, 1f, percentageComplete);
            yield return null;
        }
        cityNameText.alpha = 1f; // Ensure it's fully visible after fade-in
        streetNameText.alpha = 1f; // Ensure it's fully visible after fade-in

        yield return new WaitForSeconds(displayDuration);

        elapsedTime = 0f; // Reset elapsed time for fade-out

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float percentageComplete = elapsedTime / fadeDuration;

            cityNameText.alpha = Mathf.Lerp(1f, 0f, percentageComplete);
            streetNameText.alpha = Mathf.Lerp(1f, 0f, percentageComplete);
            yield return null;
        }
        cityNameText.alpha = 0f; // Ensure it's fully invisible after fade-out
        streetNameText.alpha = 0f; // Ensure it's fully invisible after fade-out
    }

    public void ClearRegionDisplay()
    {
        if (activeFade != null)
        {
            StopCoroutine(activeFade);
        }

        if (cityNameText != null)
        {
            cityNameText.text = string.Empty;
            cityNameText.alpha = 0f; // Ensure it's fully invisible after fade-out
            currentActiveRegion = null; // Clear the currently active region
        }

        if (streetNameText != null)
        {
            streetNameText.text = string.Empty;
            streetNameText.alpha = 0f; // Ensure it's fully invisible after fade-out
            currentStreetName = string.Empty; // Clear the currently active street name
        }
    }
}