using System.Collections.Generic;
using MelonLoader;
using UnityEngine;

namespace AutoRepairBench
{
    public class Config
    {
        public const string ModName = "AutoRepairBench";

        private const string ConfigCategoryName = "Config";
        private const string RepairCategoryName = "Repair";
        private const string ScrappingCategoryName = "Scrapping";
        private const string UpgradeCategoryName = "Upgrade";
        private const string MiscellaneousCategoryName = "Misc";

        public bool ShouldFixDentBug => _shouldFixDentBug.Value;

        /* Repairing */ 
        public KeyCode RepairBindKey => _repairBindKey.Value;
        public bool ShouldFixItems => _shouldFixItems.Value;
        public bool ShouldFixBodyParts => _shouldFixBodyParts.Value;
        public bool ShouldLatheBrakeDiscs => _shouldLatheBrakeDiscs.Value;
        public bool ShouldBalanceWheels => _shouldBalanceWheels.Value;
        public bool ShouldTakeMoney => _shouldTakeMoney.Value;

        public bool VanillaBenchRules => _vanillaBenchRules.Value;
        public float MinFixCarPartCondition => _minFixCarPartCondition.Value * 0.01f;
        public float MaxFixCarPartCondition => _maxFixCarPartCondition.Value * 0.01f;
        public List<string> FixCarPartBlacklist => _fixCarPartBlacklist.Value;
        public float MinFixBodyPartCondition => _minFixBodyPartCondition.Value * 0.01f;
        public float MaxFixBodyPartCondition => _maxFixBodyPartCondition.Value * 0.01f;
        public List<string> FixBodyPartBlacklist => _fixBodyPartBlacklist.Value;

        /* Scrapping */
        public KeyCode ScrappingBindKey => _scrappingBindKey.Value;
        public bool ShouldScrapParts => _shouldScrapParts.Value;
        public bool ShouldScrapRepairables => _shouldScrapRepairables.Value;
        public float MinScrapPartCondition => _minScrapPartCondition.Value * 0.01f;
        public float MaxScrapPartCondition => _maxScrapPartCondition.Value * 0.01f;
        
        /* Upgrade */
        public KeyCode UpgradeBindKey => _upgradeBindKey.Value;
        public bool ShouldUpgradeParts => _shouldUpgradeParts.Value;
        public bool ShouldTakeScraps => _shouldTakeScrap.Value;

        public bool DebugOutput => _debugOutput.Value;

        private readonly MelonPreferences_Category _config;
        private readonly MelonPreferences_Entry<bool> _shouldFixDentBug;

        private readonly MelonPreferences_Category _repair;
        private readonly MelonPreferences_Entry<KeyCode> _repairBindKey;
        private readonly MelonPreferences_Entry<bool> _shouldFixItems;
        private readonly MelonPreferences_Entry<bool> _shouldFixBodyParts;
        private readonly MelonPreferences_Entry<bool> _shouldLatheBrakeDiscs;
        private readonly MelonPreferences_Entry<bool> _shouldBalanceWheels;
        private readonly MelonPreferences_Entry<bool> _shouldTakeMoney;
        
        private readonly MelonPreferences_Entry<bool> _vanillaBenchRules;
        private readonly MelonPreferences_Entry<int> _minFixCarPartCondition;
        private readonly MelonPreferences_Entry<int> _maxFixCarPartCondition;
        private readonly MelonPreferences_Entry<List<string>> _fixCarPartBlacklist;
        private readonly MelonPreferences_Entry<int> _minFixBodyPartCondition;
        private readonly MelonPreferences_Entry<int> _maxFixBodyPartCondition;
        private readonly MelonPreferences_Entry<List<string>> _fixBodyPartBlacklist;

        private readonly MelonPreferences_Category _scrapping;
        private readonly MelonPreferences_Entry<KeyCode> _scrappingBindKey;
        private readonly MelonPreferences_Entry<bool> _shouldScrapParts;
        private readonly MelonPreferences_Entry<bool> _shouldScrapRepairables;
        private readonly MelonPreferences_Entry<int> _minScrapPartCondition;
        private readonly MelonPreferences_Entry<int> _maxScrapPartCondition;

        private readonly MelonPreferences_Category _upgrade;
        private readonly MelonPreferences_Entry<KeyCode> _upgradeBindKey;
        private readonly MelonPreferences_Entry<bool> _shouldUpgradeParts;
        private readonly MelonPreferences_Entry<bool> _shouldTakeScrap;

        private readonly MelonPreferences_Category _miscellaneous;
        private readonly MelonPreferences_Entry<bool> _debugOutput;

        public Config()
        {
            _config = MelonPreferences.CreateCategory(ConfigCategoryName);
            _config.SetFilePath("Mods/AutoRepairBench.cfg");

            _repair = MelonPreferences.CreateCategory(RepairCategoryName);
            _repair.SetFilePath("Mods/AutoRepairBench.cfg");

            _scrapping = MelonPreferences.CreateCategory(ScrappingCategoryName);
            _scrapping.SetFilePath("Mods/AutoRepairBench.cfg");

            _upgrade = MelonPreferences.CreateCategory(UpgradeCategoryName);
            _upgrade.SetFilePath("Mods/AutoRepairBench.cfg");

            _miscellaneous = MelonPreferences.CreateCategory(MiscellaneousCategoryName);
            _miscellaneous.SetFilePath("Mods/AutoRepairBench.cfg");

            // Config
            _shouldFixDentBug = _config.CreateEntry(nameof(ShouldFixDentBug), true);
            
            // Repair
            _repairBindKey = _repair.CreateEntry(nameof(RepairBindKey), KeyCode.F7);
            _shouldFixItems = _repair.CreateEntry(nameof(ShouldFixItems), true);
            _shouldFixBodyParts = _repair.CreateEntry(nameof(ShouldFixBodyParts), true);
            _shouldLatheBrakeDiscs = _repair.CreateEntry(nameof(ShouldLatheBrakeDiscs), true);
            _shouldBalanceWheels = _repair.CreateEntry(nameof(ShouldBalanceWheels), true);
            _shouldTakeMoney = _repair.CreateEntry(nameof(ShouldTakeMoney), false);
            
            _vanillaBenchRules = _repair.CreateEntry(nameof(VanillaBenchRules), true);
            _minFixCarPartCondition = _repair.CreateEntry(nameof(MinFixCarPartCondition), -1);
            _maxFixCarPartCondition = _repair.CreateEntry(nameof(MaxFixCarPartCondition), 99);
            _fixCarPartBlacklist = _repair.CreateEntry(nameof(FixCarPartBlacklist), new List<string>());
            _minFixBodyPartCondition = _repair.CreateEntry(nameof(MinFixBodyPartCondition), -1);
            _maxFixBodyPartCondition = _repair.CreateEntry(nameof(MaxFixBodyPartCondition), 99);
            _fixBodyPartBlacklist = _repair.CreateEntry(nameof(FixBodyPartBlacklist), new List<string>());
            
            // Scrapping
            _scrappingBindKey = _scrapping.CreateEntry(nameof(ScrappingBindKey), KeyCode.F7);
            _shouldScrapParts = _scrapping.CreateEntry(nameof(ShouldScrapParts), true);
            _shouldScrapRepairables = _scrapping.CreateEntry(nameof(ShouldScrapRepairables), false);
            _minScrapPartCondition = _scrapping.CreateEntry(nameof(MinScrapPartCondition), -1);
            _maxScrapPartCondition = _scrapping.CreateEntry(nameof(MaxScrapPartCondition), 65);
            
            // Upgrading
            _upgradeBindKey = _upgrade.CreateEntry(nameof(UpgradeBindKey), KeyCode.F7);
            _shouldUpgradeParts = _upgrade.CreateEntry(nameof(ShouldUpgradeParts), true);
            _shouldTakeScrap = _upgrade.CreateEntry(nameof(ShouldTakeScraps), false);

            // Miscellaneous
            _debugOutput = _miscellaneous.CreateEntry(nameof(DebugOutput), false, dont_save_default: true);
        }

        public void Reload()
        {
            _config.LoadFromFile();
            _repair.LoadFromFile();
            _scrapping.LoadFromFile();
            _upgrade.LoadFromFile();
            _miscellaneous.LoadFromFile();
        }

        public void FixedDentBug()
        {
            _shouldFixDentBug.Value = false;
            _shouldFixDentBug.Save();
            _config.SaveToFile();
        }
    }
}