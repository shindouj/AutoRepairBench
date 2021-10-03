using System.Collections.Generic;
using System.Linq;

namespace AutoRepairBench
{
    public class UniversalPartHandler : IPartHandler
    {
        public UniversalPartHandler(Inventory inventory, Config config, MoneyCalculator moneyCalculator) : base(inventory, config, moneyCalculator)
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
            var partList = bodyParts ? GetFixableBodyParts() : GetFixableCarParts();
            var count = 0;
            
            foreach (var item in partList)
            {
                LogHelper.Debug(item.ToPrettyString());
                
                if (!_moneyCalculator.HandleRepairCost(item))
                {
                    throw new CannotAffordException(count);
                }
                
                item.FixItem();
                count++;
            }

            return count;
        } 

        private List<Item> GetFixableCarParts()
        {
            var blacklist = _config.FixCarPartBlacklist;
            var minCondition = _config.MinFixCarPartCondition;
            var maxCondition = _config.MaxFixCarPartCondition;
            
            return _inventory.items.ToArray().ToList().FindAll(it => 
                !blacklist.Contains(it.ID) &&
                it.Condition >= minCondition && 
                it.Condition <= maxCondition && 
                !it.ID.StartsWith("car_"))
                .ToList();
        }

        private List<Item> GetFixableBodyParts()
        {
            var blacklist = _config.FixBodyPartBlacklist;
            var minCondition = _config.MinFixBodyPartCondition;
            var maxCondition = _config.MaxFixBodyPartCondition;

            return _inventory.items.ToArray().ToList().FindAll(it =>
                    !blacklist.Contains(it.ID) &&
                    it.Condition >= minCondition &&
                    it.Condition <= maxCondition &&
                    it.ID.StartsWith("car_"))
                .ToList();
        }
    }
}