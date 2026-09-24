using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class TabsController : MonoBehaviour
    {
        public static TabsController Instance;

        [SerializeField] private GameObject tabPrefab;
        [SerializeField] private Transform tabsContainer;

        private UpgradeType currentTab = UpgradeType.Multipler;

        private UpgradesController UpgradesCotroller;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
                Destroy(gameObject);
        }

        public void Initialize(UpgradesController upgradesController)
        {
            UpgradesCotroller = upgradesController;

            ChangeTab(UpgradeType.Click);
        }

        public void ChangeTab(UpgradeType tabType)
        {
            if (currentTab == tabType) return;

            switch (tabType)
            {
                case UpgradeType.Click:
                    SetupTab(UpgradesCotroller.PerClickData, tabType);
                    break;
                case UpgradeType.Multipler:
                    SetupTab(UpgradesCotroller.MultiplerData, tabType);
                    break;
            }

            currentTab = tabType;
        }

        private void SetupTab(UpgradesLevelData data, UpgradeType tabType)
        {
            for (int i = tabsContainer.childCount - 1; i >= 0; i--)
            {
                Destroy(tabsContainer.GetChild(i).gameObject);
            }

            foreach (var upgrade in data.Upgrades)
            {
                var tabObject = Instantiate(tabPrefab, tabsContainer);
                var tabItem = tabObject.GetComponent<TabItem>();
                tabItem.Setup(upgrade, tabType, UpgradesCotroller);
            }
        }

    }
}
