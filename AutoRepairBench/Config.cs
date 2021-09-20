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

        /* Repairing */ 
        public bool ShouldFixItems => _shouldFixItems.Value;
        public bool ShouldFixBodyParts => _shouldFixBodyParts.Value;
        public bool ShouldLatheBrakeDiscs => _shouldLatheBrakeDiscs.Value;
        public bool ShouldBalanceWheels => _shouldBalanceWheels.Value;
        public bool ShouldTakeMoney => _shouldTakeMoney.Value;

        /* Scrapping */
        public bool ShouldScrapParts => _shouldScrapParts.Value;
        public bool ShouldScrapRepairables => _shouldScrapRepairables.Value;
        public float MinScrapPartCondition => _minScrapPartCondition.Value * 0.01f;
        public float MaxScrapPartCondition => _maxScrapPartCondition.Value * 0.01f;
        
        /* Upgrade */
        public bool ShouldUpgradeParts => _shouldUpgradeParts.Value;
        public bool ShouldTakeScraps => _shouldTakeScrap.Value;

        public bool DebugOutput => _debugOutput.Value;
        public KeyCode BindKey => _bindKey.Value;

        private readonly MelonPreferences_Category _config;
        private readonly MelonPreferences_Entry<KeyCode> _bindKey;

        private readonly MelonPreferences_Category _repair;
        private readonly MelonPreferences_Entry<bool> _shouldFixItems;
        private readonly MelonPreferences_Entry<bool> _shouldFixBodyParts;
        private readonly MelonPreferences_Entry<bool> _shouldLatheBrakeDiscs;
        private readonly MelonPreferences_Entry<bool> _shouldBalanceWheels;
        private readonly MelonPreferences_Entry<bool> _shouldTakeMoney;

        private readonly MelonPreferences_Category _scrapping;
        private readonly MelonPreferences_Entry<bool> _shouldScrapParts;
        private readonly MelonPreferences_Entry<bool> _shouldScrapRepairables;
        private readonly MelonPreferences_Entry<int> _minScrapPartCondition;
        private readonly MelonPreferences_Entry<int> _maxScrapPartCondition;

        private readonly MelonPreferences_Category _upgrade;
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
            _bindKey = _config.CreateEntry(nameof(BindKey), KeyCode.F7);

            // Repair
            _shouldFixItems = _repair.CreateEntry(nameof(ShouldFixItems), true);
            _shouldFixBodyParts = _repair.CreateEntry(nameof(ShouldFixBodyParts), true);
            _shouldLatheBrakeDiscs = _repair.CreateEntry(nameof(ShouldLatheBrakeDiscs), true);
            _shouldBalanceWheels = _repair.CreateEntry(nameof(ShouldBalanceWheels), true);
            _shouldTakeMoney = _repair.CreateEntry(nameof(ShouldTakeMoney), false);
            
            // Scrapping
            _shouldScrapParts = _scrapping.CreateEntry(nameof(ShouldScrapParts), true);
            _shouldScrapRepairables = _scrapping.CreateEntry(nameof(ShouldScrapRepairables), false);
            _minScrapPartCondition = _scrapping.CreateEntry(nameof(MinScrapPartCondition), -1);
            _maxScrapPartCondition = _scrapping.CreateEntry(nameof(MaxScrapPartCondition), 65);
            
            // Upgrading
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
    }
}