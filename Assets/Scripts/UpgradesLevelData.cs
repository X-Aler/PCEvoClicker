using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public enum UpgradeType
    {
        Click,
        Multipler
    }
    [Serializable]
    public class Upgrade
    {
        [SerializeField] private int level;

        [SerializeField] private double value;

        [SerializeField] private double cost;

        [SerializeField] private Sprite image;

        public int Level => level;
        public double Value => value;
        public double Cost => cost;
        public Sprite Image => image;
    }

    [CreateAssetMenu(fileName = "UpgradesLevelData", menuName = "UpgradesLevelData")]
    public class UpgradesLevelData : ScriptableObject
    {
        [SerializeField] private List<Upgrade> upgrades;
        public List<Upgrade> Upgrades => upgrades;

        [SerializeField] private UpgradeType upgradeType;
        public UpgradeType UpgradeType => upgradeType;
    }
}
