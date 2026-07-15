using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using TMPro;

public class CutsceneController : MonoBehaviour
{
    public TextAsset[] newsFiles; // Array of TextAssets containing news text for each cutscene
    public TextMeshProUGUI newsText; // Reference to the TextMeshProUGUI component for displaying news text
    public float lineDisplayDuration = 2f; // Duration to display each line of news text
    public float characterDuration = 0.05f; // Duration for each character to appear
    public int currentFileIndex, currentLineIndex; // Track the current file and line being displayed
    public Coroutine currentCutscene; // Reference to the active coroutine for displaying news text
    public CharacterController player; 
    public Camera playerCam;

    void Start()
    {
        player.enabled = false; // Disable player movement during the cutscene
        playerCam.enabled = false; // Disable player camera control during the cutscene

        if (currentCutscene != null)
        {
            StopCoroutine(currentCutscene);
        }

        currentCutscene = StartCoroutine(OpeningDialogue());
    }

    public IEnumerator OpeningDialogue()
    {
            while (currentFileIndex < newsFiles.Length)
            {
                string[] activeFileLines = newsFiles[currentFileIndex].text.Split('\n');

                while (currentLineIndex < activeFileLines.Length)
                {
                    string currentLine = activeFileLines[currentLineIndex];
                    currentLine = currentLine.Trim(); // Remove any leading or trailing whitespace

                    newsText.text = currentLine;

                    float waitTime = lineDisplayDuration + (currentLine.Length * characterDuration);

                    yield return new WaitForSeconds(waitTime);

                    currentLineIndex++;
                }

                currentLineIndex = 0; // Reset line index for the next file
                currentFileIndex++;
                
            yield return null;
        }
            player.enabled = true; // Re-enable player movement after the cutscene
            playerCam.enabled = true; // Re-enable player camera control after the cutscene
    }
}
