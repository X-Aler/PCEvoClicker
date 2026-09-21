using Assets.Scripts;
using System.Linq;
using UnityEngine;

public class UpgradesController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer PCSprite;
    [SerializeField] private SpriteRenderer RoomSprite;

    [SerializeField] private UpgradesLevelData PerClickData;
    [SerializeField] private UpgradesLevelData MultiplerData;

    [SerializeField] private ClickController clickController;

    private int PerClickLvl = 1;
    private int MultiplerLvl = 1;

    private CoinsManager CoinsManager;

    public void Initialize(CoinsManager coinsManager)
    {
        CoinsManager = coinsManager;
    }

    public void PerClickUpgrade() => Upgrade(ref PerClickLvl, PerClickData, PCSprite);

    public void MultiplerUpgrade() => Upgrade(ref MultiplerLvl, MultiplerData, RoomSprite);

    private void Upgrade(ref int lvl, UpgradesLevelData data, SpriteRenderer renderer)
    {
        lvl++;

        var level = lvl;

        var upgrade = data.Upgrades.FirstOrDefault(u => u.Level == level);

        if (upgrade == null) return;

        if (CoinsManager.TrySpend(upgrade.Cost))
        {
            switch (data.UpgradeType)
            {
                case UpgradeType.Click:
                    clickController.SetPerClick(upgrade.Value);
                    break;
                case UpgradeType.Multipler:
                    clickController.SetMultipler(upgrade.Value);
                    break;
            }
        }

        renderer.sprite = upgrade.Image;
    }
}
