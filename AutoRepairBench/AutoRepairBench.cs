using System;
using System.Collections.Generic;
using System.Linq;
using CMS.Containers;
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
        private Config _config;

        public override void OnApplicationStart()
        {
            MelonLogger.Msg("Initializing...");
            _config = new Config();
        }

        public override void OnUpdate()
        {
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(_config.BindKey))
            {
                _config.Reload();
                UIManager.Get().ShowInfoWindow("[DEBUG] Config has been reloaded.");
            }
            
            if (!Input.GetKeyDown(_config.BindKey)) return;
            
            var inventory = Singleton<Inventory>.Instance;
            var gameManager = Singleton<GameManager>.Instance;
            var repairPartWindow = Singleton<RepairPartWindow>.Instance;
            var currentDifficulty = gameManager.ProfileManager.GetSelectedProfileDifficulty();
            var uiManager = UIManager.Get();

            var moneyCalculator = new MoneyCalculator(_config, gameManager, repairPartWindow);
            var scrapHandler = new ScrapHandler(inventory, _config);
            var miscHandler = new MiscHandler(inventory);
            var upgradeHandler = new UpgradeHandler(inventory, moneyCalculator);
            IPartHandler partHandler = new VanillaPartHandler(inventory, _config, moneyCalculator);

            if (_config.ShouldFixItems)
            {
                try
                {
                    uiManager.ShowPopup(Config.ModName,
                        $"Fixed {partHandler.FixCarParts()} items",
                        PopupType.Normal);
                }
                catch (CannotAffordException ex)
                {
                    uiManager.ShowPopup(Config.ModName, 
                        $"Could not afford to fix all of the parts. Fixed {ex.Count} parts.", 
                        PopupType.Normal);
                }
            }

            if (_config.ShouldFixBodyParts)
            {
                try
                {
                    uiManager.ShowPopup(Config.ModName,
                        $"Fixed {partHandler.FixBodyParts()} body parts",
                        PopupType.Normal);
                }
                catch (CannotAffordException ex)
                {
                    uiManager.ShowPopup(Config.ModName, 
                        $"Could not afford to fix all of the body parts. Fixed {ex.Count} parts.", 
                        PopupType.Normal);
                }
            }

            if (_config.ShouldLatheBrakeDiscs)
            {
                uiManager.ShowPopup(Config.ModName, 
                    $"Lathed {miscHandler.LatheBrakes()} brake discs", 
                    PopupType.Normal);
            }
            
            if (_config.ShouldBalanceWheels)
            {
                uiManager.ShowPopup(Config.ModName, 
                    $"Balanced {miscHandler.BalanceWheels()} wheels", 
                    PopupType.Normal);
            }

            if (_config.ShouldScrapParts && currentDifficulty != DifficultyLevel.Sandbox)
            {
                uiManager.ShowPopup(Config.ModName, 
                    $"Turned {scrapHandler.ScrapParts()} parts into scraps", 
                    PopupType.Normal);
            }

            if (_config.ShouldUpgradeParts)
            {
                try
                {
                    uiManager.ShowPopup(Config.ModName,
                        $"Upgraded {upgradeHandler.UpgradeParts()} parts for scraps",
                        PopupType.Normal);
                }
                catch (CannotAffordException ex)
                {
                    uiManager.ShowPopup(Config.ModName, 
                        $"Could not afford to upgrade all of the parts. Upgraded {ex.Count} parts.", 
                        PopupType.Normal);
                }
                
            }
        }

        
    }
}