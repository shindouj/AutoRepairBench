using MelonLoader;
using UnityEngine;

namespace AutoRepairBench
{
    public class Config
    {
        private const string ConfigCategory = "AutoRepairBench";

        public bool ShouldFixItems => _shouldFixItems.Value;
        public bool ShouldFixBodyParts => _shouldFixBodyParts.Value;
        public bool ShouldLatheBrakeDiscs => _shouldLatheBrakeDiscs.Value;
        public bool ShouldBalanceWheels => _shouldBalanceWheels.Value;
        public bool DebugOutput => _debugOutput.Value;
        public KeyCode BindKey => _bindKey.Value;

        private readonly MelonPreferences_Category _mainCategory;
        private readonly MelonPreferences_Entry<bool> _shouldFixItems;
        private readonly MelonPreferences_Entry<bool> _shouldFixBodyParts;
        private readonly MelonPreferences_Entry<bool> _shouldLatheBrakeDiscs;
        private readonly MelonPreferences_Entry<bool> _shouldBalanceWheels;
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
            _debugOutput = _mainCategory.CreateEntry(nameof(DebugOutput), false, is_hidden: true);
            _bindKey = _mainCategory.CreateEntry(nameof(BindKey), KeyCode.F7);
        }

        public void Reload()
        {
            _mainCategory.LoadFromFile();
        }
    }
}