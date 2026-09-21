using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class CoinsUIHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private Transform textSpawn;

    private TextMeshProUGUI coinsChangedText;

    private CoinsManager CoinsManager;

    public void Initialzie(CoinsManager coinsManager)
    {
        CoinsManager = coinsManager;

        CoinsManager.OnCoinsChanged += UpdateUI;

        UpdateUI();
    }

    private void UpdateUI(double changedCount = 0)
    {
        coinsText.text = FormatNumber(CoinsManager.Coins);
    }

    private string FormatNumber(double number)
    {
        if (number < 1000)
            return number.ToString("F1");

        string[] suffixes = { "", "K", "M", "B", "T" };
        int k;

        if (number == 0) k = 0;
        else k = (int)Mathf.Floor((float)Math.Log10(number) / 3);

        k = Mathf.Min(k, suffixes.Length - 1);

        var shortNumber = number / Math.Pow(10, k * 3);

        return shortNumber.ToString("F1") + suffixes[k];
    }
}
