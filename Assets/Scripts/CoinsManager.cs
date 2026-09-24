using System;
using UnityEngine;
using YG;

public class CoinsManager
{
    private double coins;
    public double Coins => coins;

    public event Action<double> OnCoinsChanged;

    public CoinsManager()
    {
        // Если данные уже готовы (например, при смене сцены)
        if (YG2.isSDKEnabled)
        {
            InitCoins();
        }
        else
        {
            // Если это самый первый старт игры — ждем события от Яндекса
            YG2.onGetSDKData += InitCoins;
        }
    }

    private void InitCoins()
    {
        // Отписываемся, чтобы метод не вызвался повторно
        YG2.onGetSDKData -= InitCoins;

        // Загружаем реальные монеты из облака Яндекса
        coins = YG2.saves.Coins;

        // Оповещаем интерфейс (UI), что монеты загрузились, 
        // передав 0 в качестве изменения баланса (чтобы UI просто обновил текст)
        OnCoinsChanged?.Invoke(0);
    }

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
