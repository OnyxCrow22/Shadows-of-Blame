using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OpeningCutsceneDialogue : MonoBehaviour
{
    public TextAsset Newscaster1, Newscaster2, Newscaster3;
    public TextMeshProUGUI dialogueText;

    private void Start()
    {
        StartCoroutine(PlayDialogue());
    }

    public IEnumerator PlayDialogue()
    {
        string[] newscasterLinesA = Newscaster1.text.Split("\n");
        string[] newscasterLinesB = Newscaster2.text.Split("\n");
        string[] newscasterLinesC = Newscaster3.text.Split("\n");

        foreach (string line in newscasterLinesA)
        {
            dialogueText.text = line.Trim();
            yield return new WaitForSeconds(5);
        }

        foreach (string line in newscasterLinesB)
        {
            dialogueText.text = line.Trim();
            yield return new WaitForSeconds(5);
        }

        foreach (string line in newscasterLinesC)
        {
            dialogueText.text = line.Trim();
            yield return new WaitForSeconds(5);
        }
    }
}