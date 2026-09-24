using Assets.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;
using YG;

public class TabItem : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text description;
    [SerializeField] private TMP_Text buyText;
    [SerializeField] private Button buyButton;

    private UpgradeType upgradeType;
    private UpgradesController upgradeController;

    public void Setup(Upgrade upgrade, UpgradeType type, UpgradesController controller)
    {
        image.sprite = upgrade.Image;

        int currentLvl = 1;

        switch (type)
        {
            case UpgradeType.Click:
                description.text = YG2.lang == "ru" ? $"+{upgrade.Value} за клик" : $"+{upgrade.Value} per click";
                currentLvl = controller.PerClickLvl;
                break;
            case UpgradeType.Multipler:
                description.text = YG2.lang == "ru" ? $"x{upgrade.Value} множитель" : $"x{upgrade.Value} multiplier";
                currentLvl = controller.MultiplerLvl;
                break;
        }

        upgradeType = type;
        upgradeController = controller;

        buyButton.onClick.AddListener(BuyClick);

        if (currentLvl >= upgrade.Level)
        {
            ChangeUI();
        }
        else
            buyText.text = $"{upgrade.Cost}";
    }

    private void BuyClick()
    {
        switch (upgradeType)
        {
            case UpgradeType.Click:
                if (!upgradeController.PerClickUpgrade()) return;
                break;
            case UpgradeType.Multipler:
                if (!upgradeController.MultiplerUpgrade()) return;
                break;
        }

        AudioManager.Instance.PlaySFX(AudioManager.Instance.buySound);

        ChangeUI();
    }

    private void ChangeUI()
    {
        buyText.text = YG2.lang == "ru" ? "Куплено" : "Purchased";
        buyButton.enabled = false;
    }
}
