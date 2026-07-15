using UnityEngine;
using System.Collections;
using TMPro;

public class RegionDisplayUI : MonoBehaviour
{
    public TextMeshProUGUI cityNameText; // Reference to the TextMeshProUGUI component for displaying the city name
    public float displayDuration = 3f; // Duration to display the region name
    public float fadeDuration = 1f; // Duration for the fade-out effect
    public Coroutine activeFade;

    public void DisplayRegion(RegionData region)
    {
        if (activeFade != null)
        {
            StopCoroutine(activeFade);
        }

        if (cityNameText != null)
        {
            activeFade = StartCoroutine(NameDisplay());
            cityNameText.text = region.cityName;
        }
    }

    public IEnumerator NameDisplay()
    {
        float elapsedTime = 0f;

        cityNameText.alpha = 0f; // Reset alpha to fully invisible

        while (elapsedTime < fadeDuration)
        {
            cityNameText.alpha = Mathf.MoveTowards(cityNameText.alpha, 1f, Time.deltaTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        cityNameText.alpha = 1f; // Ensure it's fully visible after fade-in

        yield return new WaitForSeconds(displayDuration);

        elapsedTime = 0f; // Reset elapsed time for fade-out

        while (elapsedTime < fadeDuration)
        {
            cityNameText.alpha = Mathf.MoveTowards(cityNameText.alpha, 0f, Time.deltaTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        cityNameText.alpha = 0f; // Ensure it's fully invisible after fade-out
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
        }
    }
}