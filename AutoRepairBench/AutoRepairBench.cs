using System;
using CMS.UI.Windows;
using CMS.UI.Windows.Base;
using MelonLoader;
using UnityEngine;

[assembly: MelonInfo(typeof(AutoRepairBench.AutoRepairBench), "AutoRepairBench", "0.1.0", "jeikobu__")]
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
            var gameManager = Singleton<GameManager>.Instance;
            var repairPartWindow = Singleton<RepairPartWindow>.Instance;
            var currentDifficulty = gameManager.ProfileManager.GetSelectedProfileDifficulty();
            var uiManager = UIManager.Get();

            MoneyCalculator moneyCalculator = new MoneyCalculator(_config, gameManager, repairPartWindow);
            IPartHandler partHandler = new VanillaPartHandler(inventory, _config, moneyCalculator);

            if (_config.ShouldFixItems)
            {
                try
                {
                    uiManager.ShowPopup(Config.ConfigCategory,
                        $"Fixed {partHandler.FixCarParts()} items",
                        PopupType.Normal);
                }
                catch (CannotAffordException ex)
                {
                    uiManager.ShowPopup(Config.ConfigCategory, 
                        $"Could not afford to fix all of the parts. Fixed ${ex.Count} parts.", 
                        PopupType.Normal);
                }
            }

            if (_config.ShouldFixBodyParts)
            {
                try
                {
                    uiManager.ShowPopup(Config.ConfigCategory,
                        $"Fixed {partHandler.FixBodyParts()} body parts",
                        PopupType.Normal);
                }
                catch (CannotAffordException ex)
                {
                    uiManager.ShowPopup(Config.ConfigCategory, 
                        $"Could not afford to fix all of the body parts. Fixed ${ex.Count} parts.", 
                        PopupType.Normal);
                }
            }

            if (_config.ShouldLatheBrakeDiscs)
            {
                uiManager.ShowPopup(Config.ConfigCategory, 
                    $"Lathed {LatheBrakes(inventory)} brake discs", 
                    PopupType.Normal);
            }
            
            if (_config.ShouldBalanceWheels)
            {
                uiManager.ShowPopup(Config.ConfigCategory, 
                    $"Balanced {BalanceWheels(inventory)} wheels", 
                    PopupType.Normal);
            }

            if (_config.ShouldScrapParts && currentDifficulty != DifficultyLevel.Sandbox)
            {
                uiManager.ShowPopup(Config.ConfigCategory, 
                    $"Turned {ScrapParts(inventory)} parts into scraps", 
                    PopupType.Normal);
            }
        }

        private int LatheBrakes(Inventory inventory)
        {
            var brakeDiscs = inventory.GetItemsForBrakeLathe();

            foreach (var brakeDisc in brakeDiscs)
            {
                inventory.GetItem(brakeDisc.UID).FixItem();
            }

            return brakeDiscs.Count;
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

            return unbalancedWheels.Count;
        }

        private int ScrapParts(Inventory inventory)
        {
            var scrapItems = inventory.GetItemsForScrapProduction(_config.MinScrapPartCondition, _config.MaxScrapPartCondition);
            foreach (var choosePartDownItem in scrapItems)
            {
                var item = inventory.GetItem(choosePartDownItem.BaseItem.UID);
                var scrapFromItem = GlobalData.GetScrapFromItem(item, ScrapType.BigBonus);
                
                Debug($"(ScrapParts) {item.ToPrettyString()}");
                Debug($"(ScrapParts) {scrapFromItem}");
                
                GlobalData.AddPlayerScraps(scrapFromItem);
                inventory.Delete(item);
            }

            return scrapItems.Count;
        }

        private int UpgradeParts(Inventory inventory)
        {
            var upgradeParts = inventory.GetItemsForScrapUpgrade();
            foreach (var choosePartDownItem in upgradeParts)
            {
                var item = inventory.GetItem(choosePartDownItem.BaseItem.UID);
                var price = GlobalData.GetCostForScrapUpgrade(3, Helper.GetPrice(item));
                item.Quality = 3;
                
            }

            return 0;
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