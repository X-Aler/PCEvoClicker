using YG;

namespace Assets.Scripts
{
    public class CoinsSaveHandler
    {
        private readonly CoinsManager coinsManager;

        public CoinsSaveHandler(CoinsManager manager)
        {
            coinsManager = manager;
        }

        public void Save()
        {
            if (coinsManager == null || YG2.saves == null)
                return;

            YG2.saves.Coins = coinsManager.Coins;
            YG2.SaveProgress();
        }
    }
}