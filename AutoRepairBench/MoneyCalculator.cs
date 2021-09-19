using CMS.UI.Windows;

namespace AutoRepairBench
{
    public class MoneyCalculator
    {
        private readonly Config _config;
        private readonly DifficultyLevel _difficulty;
        private readonly RepairPartWindow _repairPartWindow;

        public MoneyCalculator(Config config, GameManager gameManager, RepairPartWindow repairPartWindow)
        {
            _config = config;
            _difficulty = gameManager.ProfileManager.GetSelectedProfileDifficulty();
            _repairPartWindow = repairPartWindow;
        }

        public bool HandleRepairCost(Item item)
        {
            if (_difficulty == DifficultyLevel.Sandbox || !_config.ShouldTakeMoney) return true;

            var repairPrice = _repairPartWindow.CalculateRepairPrice(item);
            if (GlobalData.PlayerMoney < repairPrice) return false;
            GlobalData.SetPlayerMoney(GlobalData.PlayerMoney - repairPrice);
            return true;
        }

        public bool HandleUpgradeCost(Item item)
        {
            if (_difficulty == DifficultyLevel.Sandbox || !_config.ShouldTakeScraps) return true;
            
            var upgradePrice = GlobalData.GetCostForScrapUpgrade(3, Helper.GetPrice(item));
            if (GlobalData.PlayerScraps < upgradePrice) return false;
            GlobalData.SetPlayerScraps(GlobalData.PlayerScraps - upgradePrice);
            return true;
        }
    }
}