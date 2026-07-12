using UnityEngine;
using TMPro;

public class RandomPlateGenerator : MonoBehaviour
{
    public TextMeshPro frontPlate;
    public TextMeshPro backPlate;

    public RegionalData regionData;

    private const string Letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private const string Numbers = "0123456789";

    private void Awake()
    {
        string plate = GeneratePlateForCountry(regionData.regionalArea);

        if (frontPlate != null)
            frontPlate.text = plate;

        if (backPlate != null)
            backPlate.text = plate;
    }

    private string GeneratePlateForCountry(string country)
    {
        switch (country)
        {
            case "Westral Federation":
                return GenerateWEFPlate();

            case "Rey Del Sur":
                return GenerateRDSPlate();

            case "United Republic of Melasa":
                return GenerateURMPlate();

            default:
                return GenerateGenericPlate();
        }
    }

    private string GenerateWEFPlate()
    {
        string part1 = RandLetters(1) + RandNumbers(1);     // A4
        string part2 = RandAlphaNum(3);                     // NU1
        return $"WEF-{part1}-{part2}";
    }

    private string GenerateRDSPlate()
    {
        string part1 = RandLetters(1) + RandNumbers(1);     // N6
        string part2 = RandNumbers(2) + RandLetters(1);     // 14M
        return $"RDS-{part1}-{part2}";
    }

    private string GenerateURMPlate()
    {
        string part1 = RandLetters(2);                      // MM
        string part2 = RandNumbers(3);                      // 314
        return $"URM-{part1}-{part2}";
    }

    private string GenerateGenericPlate()
    {
        // fallback fictional format
        return $"{RandLetters(3)}-{RandNumbers(3)}";
    }

    private string RandLetters(int count)
    {
        string s = "";
        for (int i = 0; i < count; i++)
            s += Letters[Random.Range(0, Letters.Length)];
        return s;
    }

    private string RandNumbers(int count)
    {
        string s = "";
        for (int i = 0; i < count; i++)
            s += Numbers[Random.Range(0, Numbers.Length)];
        return s;
    }

    private string RandAlphaNum(int count)
    {
        string chars = Letters + Numbers;
        string s = "";
        for (int i = 0; i < count; i++)
            s += chars[Random.Range(0, chars.Length)];
        return s;
    }
}
