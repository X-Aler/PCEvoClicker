using System;
using UnityEngine;

public class ClickController : MonoBehaviour
{
    [SerializeField] private double coinsPerClick = 1;

    [SerializeField] private double coinsMultipler = 0.5;

    public event Action<double> OnClick;

    private CoinsManager CoinsManager;

    private bool initialized = false;

    public void Initialize(CoinsManager coinsManager)
    {
        CoinsManager = coinsManager;

        initialized = true;
    }

    public void Click()
    {
        if (!initialized) return;

        var clickValue = coinsPerClick * coinsMultipler;

        CoinsManager.Add(clickValue);

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
