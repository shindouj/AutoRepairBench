using Il2Cpp;

namespace AutoRepairBench
{
    public class MiscHandler
    {
        private readonly Inventory _inventory;

        public MiscHandler(Inventory inventory)
        {
            _inventory = inventory;
        }

        public int LatheBrakes()
        {
            var brakeDiscs = _inventory.GetItemsForBrakeLathe();

            foreach (var brakeDisc in brakeDiscs)
            {
                _inventory.GetItem(brakeDisc.UID).FixItem();
            }

            return brakeDiscs.Count;
        }

        public int BalanceWheels()
        {
            var unbalancedWheels = _inventory.GetUnbalancedWheels();

            foreach (var baseItem in unbalancedWheels)
            {
                var groupItem = _inventory.GetGroup(baseItem.UID);
                LogHelper.Debug(groupItem.ToPrettyString());
                BalanceWheel(groupItem);
            }

            return unbalancedWheels.Count;
        }

        private static void BalanceWheel(GroupItem wheel)
        {
            foreach (var itemListItem in wheel.ItemList)
            {
                LogHelper.Debug(itemListItem.ToPrettyString());
                    
                var data = itemListItem.WheelData;
                data.IsBalanced = true;
                itemListItem.WheelData = data;
            }
        }
    }
}