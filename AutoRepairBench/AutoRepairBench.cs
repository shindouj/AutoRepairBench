using Il2Cpp;
using Il2CppCMS.UI.Windows;
using MelonLoader;
using UnityEngine;

[assembly: MelonInfo(typeof(AutoRepairBench.AutoRepairBench), "AutoRepairBench", "0.2.1", "jeikobu__")]
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
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(_config.RepairBindKey))
            {
                _config.Reload();
                UIManager.Get().ShowInfoWindow("[DEBUG] Config has been reloaded.");
            }
            
            if (!Input.GetKeyDown(_config.RepairBindKey) && 
                !Input.GetKeyDown(_config.ScrappingBindKey) &&
                !Input.GetKeyDown(_config.UpgradeBindKey)) return;
            
            var inventory = Singleton<Inventory>.Instance;
            var gameManager = Singleton<GameManager>.Instance;
            var repairPartWindow = Singleton<RepairPartWindow>.Instance;
            var currentDifficulty = gameManager.ProfileManager.GetSelectedProfileDifficulty();
            var uiManager = UIManager.Get();

            var moneyCalculator = new MoneyCalculator(_config, gameManager, repairPartWindow);
            var scrapHandler = new ScrapHandler(inventory, _config);
            var miscHandler = new MiscHandler(inventory);
            var upgradeHandler = new UpgradeHandler(inventory, moneyCalculator);
            var partHandler = _config.VanillaBenchRules ? 
                (IPartHandler) new VanillaPartHandler(inventory, _config, moneyCalculator) : 
                new UniversalPartHandler(inventory, _config, moneyCalculator);

            if (_config.ShouldFixDentBug &&
                Input.GetKeyDown(_config.RepairBindKey))
            {
                partHandler.FixDentLessThan1();
                uiManager.ShowPopup(Config.ModName, "Fixed bugged items!", PopupType.Normal);
            }

            if (_config.ShouldFixItems && 
                Input.GetKeyDown(_config.RepairBindKey))
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

            if (_config.ShouldFixBodyParts && 
                Input.GetKeyDown(_config.RepairBindKey))
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

            if (_config.ShouldLatheBrakeDiscs && 
                Input.GetKeyDown(_config.RepairBindKey))
            {
                uiManager.ShowPopup(Config.ModName, 
                    $"Lathed {miscHandler.LatheBrakes()} brake discs", 
                    PopupType.Normal);
            }
            
            if (_config.ShouldBalanceWheels && 
                Input.GetKeyDown(_config.RepairBindKey))
            {
                uiManager.ShowPopup(Config.ModName, 
                    $"Balanced {miscHandler.BalanceWheels()} wheels", 
                    PopupType.Normal);
            }

            if (_config.ShouldScrapParts && 
                currentDifficulty != DifficultyLevel.Sandbox && 
                Input.GetKeyDown(_config.ScrappingBindKey))
            {
                uiManager.ShowPopup(Config.ModName, 
                    $"Turned {scrapHandler.ScrapParts()} parts into scraps", 
                    PopupType.Normal);
            }

            if (_config.ShouldUpgradeParts && 
                Input.GetKeyDown(_config.UpgradeBindKey))
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