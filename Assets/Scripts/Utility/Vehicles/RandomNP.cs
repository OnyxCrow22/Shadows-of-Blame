using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RandomNP : MonoBehaviour
{
    [Header("Number Plate References")]
    public TextMeshPro FrontPlate, BackPlate;
    public int letters = 5;
    public int numbers = 2;
    private const string npLetters = "ABCDEFGHIJKLMNOPQRSTUVWYXZ";
    private const string npNumbers = "1234567890";
    // Start is called before the first frame update
    void Start()
    {
        string RandomNumberPlate = GeneratePlate();
        FrontPlate.text = RandomNumberPlate;
        BackPlate.text = RandomNumberPlate;
    }

    string GeneratePlate()
    {
        string blankPlate = "";
        for (int i = 0; i < 2; i++)
        {
            int randIndex = Random.Range(0, npLetters.Length);
            blankPlate += npLetters[randIndex];
        }
        for (int i = 0; i < 2; i++)
        {
            int randIndex = Random.Range(0, npNumbers.Length);
            blankPlate += npNumbers[randIndex];
        }

        blankPlate += " ";

        for (int i = 0; i < 3; i++)
        {
            int randIndex = Random.Range(0, npLetters.Length);
            blankPlate += npLetters[randIndex];
        }

        return blankPlate;
    }
}
