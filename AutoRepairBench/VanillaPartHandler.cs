using CMS.Containers;
using MelonLoader;

namespace AutoRepairBench
{
    public class VanillaPartHandler: IPartHandler
    {
        public VanillaPartHandler(Inventory inventory, Config config, MoneyCalculator moneyCalculator) : base(inventory, config, moneyCalculator)
        {
        }
        
        public override int FixCarParts()
        {
            return FixParts(false);
        }

        public override int FixBodyParts()
        {
            return FixParts(true);
        }

        private int FixParts(bool bodyParts)
        {
            var count = 0;
            foreach (var choosePartDownItem in _inventory.GetItemsForRepairTable(bodyParts))
            {
                if (!FixChoosePartDownItem(choosePartDownItem))
                {
                    throw new CannotAffordException(count);
                }
                count++;
            }

            return count;
        }

        private bool FixChoosePartDownItem(ChoosePartDownItem choosePartDownItem)
        {
            var item = _inventory.GetItem(choosePartDownItem.BaseItem.UID);

            if (!_moneyCalculator.HandleRepairCost(item))
            {
                return false;
            }

            Debug(item.ToPrettyString());
            item.FixItem();
            return true;
        }
        
        private void Debug(string msg)
        {
            if (_config.DebugOutput)
            {
                MelonLogger.Msg($"[DEBUG] [ItemHandler] {msg}");
            }
        }
    }
}