using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class ClickController : MonoBehaviour
{
    [SerializeField] private double coinsPerClick = 1;

    [SerializeField] private double coinsMultipler = 0.5;

    [SerializeField] private TMP_Text bonusText;

    public event Action<double> OnClick;

    private CoinsManager CoinsManager;

    private bool initialized = false;

    private bool adBonus = false;

    public void Initialize(CoinsManager coinsManager)
    {
        CoinsManager = coinsManager;

        initialized = true;
    }

    public void SetAdBonus() => StartCoroutine(AdBonusCoroutine());

    private IEnumerator AdBonusCoroutine()
    {
        adBonus = true;
        bonusText.gameObject.SetActive(adBonus);

        bonusText.text = "2X\n1:00";

        yield return new WaitForSeconds(1f);

        for (int seconds = 59; seconds > 0; seconds--)
        {
            bonusText.text = $"2X\n00:{seconds:00}";
            yield return new WaitForSeconds(1f);
        }

        adBonus = false;
        bonusText.gameObject.SetActive(adBonus);
    }

    public void Click()
    {
        if (!initialized) return;

        var clickValue = coinsPerClick * coinsMultipler;

        if (adBonus)
            clickValue *= 2;

        CoinsManager.Add(clickValue);

        AudioManager.Instance.PlaySFX(AudioManager.Instance.clickSound);

        OnClick?.Invoke(clickValue);
    }

    public void SetPerClick(double value)
    {
        if (value <= 0) return;

        coinsPerClick = value;
    }
    public void SetMultipler(double value)
    {
        if (value <= 0) return;

        coinsMultipler = value;
    }
}
