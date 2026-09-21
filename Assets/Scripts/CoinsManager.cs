using System;
using UnityEngine;

public class CoinsManager
{
    private double coins;
    public double Coins => coins;

    public event Action<double> OnCoinsChanged;

    public void Add(double value)
    {
        if (value <= 0) return;

        coins += value;

        OnCoinsChanged?.Invoke(value);
    }

    public bool TrySpend(double value)
    {
        if (value > coins) return false;

        coins -= value;

        OnCoinsChanged?.Invoke(value);

        return true;
    }

    public void SetCoins(double value)
    {
        if (value <= 0) return;

        coins = value;

        OnCoinsChanged?.Invoke(value);
    }

}
