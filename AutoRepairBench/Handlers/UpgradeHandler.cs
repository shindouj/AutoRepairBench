using System.Linq;
using Il2Cpp;

namespace AutoRepairBench
{
    public class UpgradeHandler
    {
        private readonly Inventory _inventory;
        private readonly MoneyCalculator _moneyCalculator;

        public UpgradeHandler(Inventory inventory, MoneyCalculator moneyCalculator)
        {
            _inventory = inventory;
            _moneyCalculator = moneyCalculator;
        }

        public int UpgradeParts()
        {
            var upgradeParts = _inventory.GetItemsForScrapUpgrade().ToArray().ToList();
            foreach (var (choosePartDownItem, count) in upgradeParts.Select((value, i) => (value, i)))
            {
                var item = _inventory.GetItem(choosePartDownItem.BaseItem.UID);

                if (!_moneyCalculator.HandleUpgradeCost(item))
                {
                    throw new CannotAffordException(count);
                }
                
                item.Quality = 3;
            }

            return upgradeParts.Count;
        }
    }
}