using System;
using Battleship;
using Battleship.Helpers;
using Menu;

namespace InitMenu
{
    public class ChangeGameRulesHitUiProvider : Menu.Menu
    {
        private readonly GameSettings _gameSettings;
        public ChangeGameRulesHitUiProvider() : base(MenuLevel.LevelPlus, "Choose a game rule")
        {
            _gameSettings = GameSettingsController.GetGameSettings();
        }
        
        public override string Run()
        {
            AddGameSettingsToMenuItems();
            return base.Run();
        }
        private void AddGameSettingsToMenuItems()
        {
            var i = 1;
            foreach (EHitContinuousMove rule in Enum.GetValues(typeof(EHitContinuousMove)))
            {
                var uiName = _gameSettings.HitContinuousMove == rule ?
                    GameRuleProvider.GetUiName(rule) + " *" : 
                    GameRuleProvider.GetUiName(rule);
                AddMenuItem(new MenuItem(i, uiName, () =>
                {
                    _gameSettings.HitContinuousMove = rule;
                    GameSettingsController.SaveGameSettings();
                    RefreshMenuItems(AddGameSettingsToMenuItems);
                    return "";
                }));;
                i++;
            }
        }
    }
}
