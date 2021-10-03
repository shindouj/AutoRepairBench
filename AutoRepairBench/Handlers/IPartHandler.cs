using System.Linq;

namespace AutoRepairBench
{
    public abstract class IPartHandler
    {
        protected Inventory _inventory;
        protected Config _config;
        protected MoneyCalculator _moneyCalculator;

        protected IPartHandler(Inventory inventory, Config config, MoneyCalculator moneyCalculator)
        {
            _inventory = inventory;
            _config = config;
            _moneyCalculator = moneyCalculator;
        }
        public abstract int FixCarParts();
        public abstract int FixBodyParts();

        public void FixDentLessThan1()
        {
            _inventory.items.ToArray().ToList()
                .FindAll(it => it.Dent < 1f)
                .ForEach(it => it.Dent = 1f);
            _config.FixedDentBug();
        }
    }
}