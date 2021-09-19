using MelonLoader;
using UnityEngine;

namespace AutoRepairBench
{
    public class Config
    {
        public const string ConfigCategory = "AutoRepairBench";

        public bool ShouldFixItems => _shouldFixItems.Value;
        public bool ShouldFixBodyParts => _shouldFixBodyParts.Value;
        public bool ShouldLatheBrakeDiscs => _shouldLatheBrakeDiscs.Value;
        public bool ShouldBalanceWheels => _shouldBalanceWheels.Value;
        public bool ShouldScrapParts => _shouldScrapParts.Value;
        public bool ShouldUpgradeParts => _shouldUpgradeParts.Value;

        public bool ShouldTakeMoney => _shouldTakeMoney.Value;
        public bool ShouldTakeScraps => _shouldTakeScrap.Value;

        public float MinScrapPartCondition => _minScrapPartCondition.Value * 0.01f;
        public float MaxScrapPartCondition => _maxScrapPartCondition.Value * 0.01f;
        
        public bool DebugOutput => _debugOutput.Value;
        public KeyCode BindKey => _bindKey.Value;

        private readonly MelonPreferences_Category _mainCategory;
        
        private readonly MelonPreferences_Entry<bool> _shouldFixItems;
        private readonly MelonPreferences_Entry<bool> _shouldFixBodyParts;
        private readonly MelonPreferences_Entry<bool> _shouldLatheBrakeDiscs;
        private readonly MelonPreferences_Entry<bool> _shouldBalanceWheels;
        private readonly MelonPreferences_Entry<bool> _shouldScrapParts;
        private readonly MelonPreferences_Entry<bool> _shouldUpgradeParts;

        private readonly MelonPreferences_Entry<bool> _shouldTakeMoney;
        private readonly MelonPreferences_Entry<bool> _shouldTakeScrap;

        private readonly MelonPreferences_Entry<int> _minScrapPartCondition;
        private readonly MelonPreferences_Entry<int> _maxScrapPartCondition;

        private readonly MelonPreferences_Entry<bool> _debugOutput;
        private readonly MelonPreferences_Entry<KeyCode> _bindKey;

        public Config()
        {
            _mainCategory = MelonPreferences.CreateCategory(ConfigCategory);
            _mainCategory.SetFilePath("Mods/AutoRepairBench.cfg");

            _shouldFixItems = _mainCategory.CreateEntry(nameof(ShouldFixItems), true);
            _shouldFixBodyParts = _mainCategory.CreateEntry(nameof(ShouldFixBodyParts), true);
            _shouldLatheBrakeDiscs = _mainCategory.CreateEntry(nameof(ShouldLatheBrakeDiscs), true);
            _shouldBalanceWheels = _mainCategory.CreateEntry(nameof(ShouldBalanceWheels), true);
            _shouldScrapParts = _mainCategory.CreateEntry(nameof(ShouldScrapParts), true);
            _shouldUpgradeParts = _mainCategory.CreateEntry(nameof(ShouldUpgradeParts), true);

            _shouldTakeMoney = _mainCategory.CreateEntry(nameof(ShouldTakeMoney), false);
            _shouldTakeScrap = _mainCategory.CreateEntry(nameof(ShouldTakeScraps), false);

            _minScrapPartCondition = _mainCategory.CreateEntry(nameof(MinScrapPartCondition), 0);
            _maxScrapPartCondition = _mainCategory.CreateEntry(nameof(MaxScrapPartCondition), 50);
            
            _debugOutput = _mainCategory.CreateEntry(nameof(DebugOutput), false, is_hidden: true);
            _bindKey = _mainCategory.CreateEntry(nameof(BindKey), KeyCode.F7);
        }

        public void Reload()
        {
            _mainCategory.LoadFromFile();
        }
    }
}