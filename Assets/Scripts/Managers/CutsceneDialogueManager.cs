using UnityEngine;
using TMPro;
using System.Collections;

public class CutsceneDialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public DialogueSequence sequence;

    public void Play()
    {
        StartCoroutine(PlaySequence());
    }

    private IEnumerator PlaySequence()
    {
        foreach (var line in sequence.lines)
        {
            dialogueText.text = line.text;
            yield return new WaitForSeconds(line.duration);
        }

        // Raise event: cutscene finished
        CutsceneEvents.RaiseCutsceneFinished();
    }
}
