using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using YG;

public class Bootstraper : MonoBehaviour
{
    [SerializeField] private ClickController clickController;
    [SerializeField] private CoinsUIHandler coinsUIHandler;
    [SerializeField] private ClickVFXHandler clickVFXHandler;
    [SerializeField] private UpgradesController upgradeController;
    [SerializeField] private TabsController tabsController;
    [SerializeField] private bool showBannerOnStart = true;

    // Ссылка на уровне класса — GC не удалит объект
    private CoinsSaveHandler coinsSaveHandler;
    private CoinsManager coinsManager;
    private Coroutine saveCoroutine;

    public void Awake()
    {
        coinsManager = new CoinsManager();
        coinsSaveHandler = new CoinsSaveHandler(coinsManager);

        coinsManager.OnCoinsChanged += OnCoinsChanged;

        clickController.Initialize(coinsManager);
        coinsUIHandler.Initialzie(coinsManager);
        clickVFXHandler.Initialize(clickController);
        upgradeController.Initialize(coinsManager);
        tabsController.Initialize(upgradeController);
    }

    private void OnCoinsChanged(double changedCoins)
    {
        if (saveCoroutine != null)
            StopCoroutine(saveCoroutine);

        saveCoroutine = StartCoroutine(SaveCoinsDelayed());
    }

    private IEnumerator SaveCoinsDelayed()
    {
        yield return new WaitForSeconds(1f);

        coinsSaveHandler?.Save();
        saveCoroutine = null;
    }

    // Сохраняем при сворачивании / переключении вкладки игроком
    private void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
        {
            if (saveCoroutine != null)
            {
                StopCoroutine(saveCoroutine);
                saveCoroutine = null;
            }

            coinsSaveHandler?.Save();
        }
    }
  
    private void OnDestroy()
    {
        coinsManager.OnCoinsChanged -= OnCoinsChanged;
    }
    private void Start()
    {
        // Запускаем корутину безопасной инициализации
        StartCoroutine(InitSequenceRoutine());
    }

    private IEnumerator InitSequenceRoutine()
    {
        // 2. Ждем, пока плагин инициализирует SDK Яндекса и получит данные окружения (язык, устройство)
        // YG2.isSDKEnabled становится true, когда JS-библиотека ysdk загружена и ответила
        while (!YG2.isSDKEnabled)
        {
            yield return null;
        }

        YG2.SwitchLanguage(YG2.lang);

        // Даем 1 кадр на завершение работы встроенных подписчиков LanguageYG,
        // чтобы тексты на сцене успели применить полученный язык (YG2.lang)
        yield return null;

        // 3. Прячем загрузочный экран и активируем основной интерфейс

        // 4. Оповещаем Яндекс Игры о полной готовности игры к взаимодействию
        // Вызываем СТРОГО после того, как экран загрузки скрыт и меню уже перед глазами игрока
        YG2.GameReadyAPI();

        // 5. Подрубаем баннерную рекламу (RTB / Sticky-баннер)
        if (showBannerOnStart)
        {
            EnableBanner();
        }

        AudioManager.Instance.PlayLoopingSound(AudioManager.Instance.OST, 0.5f);
    }

    private void EnableBanner()
    {
        YG2.SetBannerPosition(YG2.BannerPosition.Bottom);
        YG2.ShowBanner();
    }
}
