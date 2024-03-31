using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GenerateNP : MonoBehaviour
{
    public TextMeshPro NP;
    public int NPnumbers = 2;
    public int NPletters = 5;

    private const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private const string numbers = "0123456789";

    // Start is called before the first frame update
    void Start()
    {
        string randomNP = GenerateRNP();
        NP.text = randomNP;
    }


    string GenerateRNP()
    {
        string blankPlate = "";

        for (int i = 0; i < 2; i++)
        {
            int randIndex = Random.Range(0, letters.Length);
            blankPlate += letters[randIndex];
        }

        for (int i = 0; i < 2; i++)
        {
            int randIndex = Random.Range(0, numbers.Length);
            blankPlate += numbers[randIndex];
        }

        blankPlate += " ";

        for (int i = 0; i < 3; i++)
        {
            int randIndex = Random.Range(0, letters.Length);
            blankPlate += letters[randIndex];
        }

        return blankPlate;
    }
}
