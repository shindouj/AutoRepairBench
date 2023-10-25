using System.Collections.Generic;
using System.Linq;
using Il2Cpp;
using Il2CppCMS.Containers;

namespace AutoRepairBench
{
    public class ScrapHandler
    {
        private readonly Inventory _inventory;
        private readonly Config _config;

        public ScrapHandler(Inventory inventory, Config config)
        {
            _inventory = inventory;
            _config = config;
        }

        public int ScrapParts()
        {
            var scrapItems = !_config.ShouldScrapRepairables ? GetUnrepairableScrapItems() : GetItemsForScrapProduction();
            
            foreach (var choosePartDownItem in scrapItems)
            {
                var item = _inventory.GetItem(choosePartDownItem.BaseItem.UID);
                var scrapFromItem = GlobalData.GetScrapFromItem(item, ScrapType.BigBonus);

                LogHelper.Debug(choosePartDownItem.BaseItem.ToPrettyString());
                LogHelper.Debug(item.ToPrettyString());
                
                GlobalData.AddPlayerScraps(scrapFromItem);
                _inventory.Delete(item);
            }

            return scrapItems.Count;
        }

        private List<ChoosePartDownItem> GetItemsForScrapProduction()
        {
            return _inventory.GetItemsForScrapProduction(_config.MinScrapPartCondition, _config.MaxScrapPartCondition)
                .ToArray()
                .ToList();
        }

        private List<ChoosePartDownItem> GetUnrepairableScrapItems()
        {
            var scrapItems = GetItemsForScrapProduction();
            var repairables = _inventory.GetItemsForRepairTable(false).ToArray().ToList();
            var bodyRepairables = _inventory.GetItemsForRepairTable(true).ToArray().ToList();
            
            return scrapItems
                .Where(it => repairables.All(part => part.BaseItem.UID != it.BaseItem.UID))
                .Where(it => bodyRepairables.All(part => part.BaseItem.UID != it.BaseItem.UID))
                .ToList();
        }
    }
}