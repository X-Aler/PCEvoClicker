using Assets.Scripts;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using YG;

public class UpgradesController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer PCSprite;
    [SerializeField] private SpriteRenderer RoomSprite;

    [SerializeField] private UpgradesLevelData perClickData;
    [SerializeField] private UpgradesLevelData multiplerData;

    [SerializeField] private ClickController clickController;

    private int perClickLvl = 1;
    private int multiplerLvl = 1;
    public UpgradesLevelData PerClickData => perClickData;
    public UpgradesLevelData MultiplerData => multiplerData;
    public int PerClickLvl => perClickLvl;
    public int MultiplerLvl => multiplerLvl;

    private CoinsManager CoinsManager;

    public void Initialize(CoinsManager coinsManager)
    {
        CoinsManager = coinsManager;

        perClickLvl = YG2.saves.perClickLvl;
        multiplerLvl = YG2.saves.multiplerLvl;

        SetupUpgrades();
    }

    private void SetupUpgrades()
    {
        ApplyUpgrade(perClickData, perClickLvl, clickController.SetPerClick, PCSprite);
        ApplyUpgrade(multiplerData, multiplerLvl, clickController.SetMultipler, RoomSprite);
    }

    private void ApplyUpgrade(UpgradesLevelData data, int level, Action<double> setAction, SpriteRenderer spriteContainer)
    {
        var upgrade = data.Upgrades.FirstOrDefault(u => u.Level == level);
        if (upgrade != null)
        {
            setAction(upgrade.Value);
            spriteContainer.sprite = upgrade.Image;
        }
    }

    public bool PerClickUpgrade() => Upgrade(ref perClickLvl, PerClickData, PCSprite);

    public bool MultiplerUpgrade() => Upgrade(ref multiplerLvl, multiplerData, RoomSprite);

    private bool Upgrade(ref int lvl, UpgradesLevelData data, SpriteRenderer renderer)
    {
        YG2.InterstitialAdvShow();

        var nextLevel = lvl + 1;

        var upgrade = data.Upgrades.FirstOrDefault(u => u.Level == nextLevel);

        if (upgrade == null) return false;

        if (CoinsManager.TrySpend(upgrade.Cost))
        {
            lvl++;
            renderer.sprite = upgrade.Image;

            switch (data.UpgradeType)
            {
                case UpgradeType.Click:
                    clickController.SetPerClick(upgrade.Value);
                    YG2.saves.perClickLvl = perClickLvl;
                    break;
                case UpgradeType.Multipler:
                    clickController.SetMultipler(upgrade.Value);
                    YG2.saves.multiplerLvl = multiplerLvl;
                    break;
            }

            YG2.SaveProgress();

            return true;
        }

        return false;
    }
}
