using System;
using MelonLoader;
using UnityEngine;

[assembly: MelonInfo(typeof(AutoRepairBench.AutoRepairBench), "AutoRepairBench", "0.0.2", "jeikobu__")]
[assembly: MelonGame("Red Dot Games", "Car Mechanic Simulator 2021")]
namespace AutoRepairBench
{
    public class AutoRepairBench : MelonMod
    {
        private const KeyCode BindKeyCode = KeyCode.F12;
        private Config _config;

        public AutoRepairBench()
        {
            
        }

        public override void OnApplicationStart()
        {
            MelonLogger.Msg("Initializing...");
            _config = new Config();
        }

        public override void OnUpdate()
        {
            if (!Input.GetKeyDown(_config.BindKey)) return;
            
            var inventory = Singleton<Inventory>.Instance;
            var uiManager = UIManager.Get();

            if (_config.ShouldFixItems)
            {
                uiManager.ShowPopup("AutoRepairBench", $"Fixed {FixItems(inventory)} items", PopupType.Normal);
            }

            if (_config.ShouldFixBodyParts)
            {
                uiManager.ShowPopup("AutoRepairBench", $"Fixed {FixBodyParts(inventory)} body parts", PopupType.Normal);
            }

            if (_config.ShouldLatheBrakeDiscs)
            {
                uiManager.ShowPopup("AutoRepairBench", $"Lathed {LatheBrakes(inventory)} brake discs", PopupType.Normal);
            }
            
            if (_config.ShouldBalanceWheels)
            {
                uiManager.ShowPopup("AutoRepairBench", $"Balanced {BalanceWheels(inventory)} wheels", PopupType.Normal);
            }
        }

        private int FixItems(Inventory inventory)
        {
            var repairItems = inventory.GetItemsForRepairTable(false);
            
            foreach (var choosePartDownItem in repairItems)
            {
                inventory.GetItem(choosePartDownItem.BaseItem.UID).FixItem();
            }

            return repairItems._size;
        }

        private int FixBodyParts(Inventory inventory)
        {
            var repairBodyItems = inventory.GetItemsForRepairTable(true);
            
            foreach (var choosePartDownItem in repairBodyItems)
            {
                inventory.GetItem(choosePartDownItem.BaseItem.UID).FixItem();
            }

            return repairBodyItems._size;
        }

        private int LatheBrakes(Inventory inventory)
        {
            var brakeDiscs = inventory.GetItemsForBrakeLathe();

            foreach (var brakeDisc in brakeDiscs)
            {
                inventory.GetItem(brakeDisc.UID).FixItem();
            }

            return brakeDiscs._size;
        }

        private int BalanceWheels(Inventory inventory)
        {
            var unbalancedWheels = inventory.GetUnbalancedWheels();

            foreach (var baseItem in unbalancedWheels)
            {
                var groupItem = inventory.GetGroup(baseItem.UID);
                Debug(groupItem.ToPrettyString());
                
                foreach (var itemListItem in groupItem.ItemList)
                {
                    Debug(itemListItem.ToPrettyString());
                    
                    var data = itemListItem.WheelData;
                    data.IsBalanced = true;
                    itemListItem.WheelData = data;
                }
            }

            return unbalancedWheels._size;
        }

        private void Debug(string msg)
        {
            if (_config.DebugOutput)
            {
                MelonLogger.Msg($"[DEBUG] {msg}");
            }
        }
    }
}