using System;
using Battleship;
using Battleship.Helpers;
using Menu;

namespace InitMenu
{
    public class ChangeGameTouchRulesUiProvider : Menu.Menu
    {
        private GameSettings _gameSettings;

        public ChangeGameTouchRulesUiProvider() : base(MenuLevel.LevelPlus, "Choose a game rule")
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
            foreach (EBoatCanTouch rule in Enum.GetValues(typeof(EBoatCanTouch)))
            {
                var uiName = _gameSettings.BoatsCanTouch == rule ?
                    GameRuleProvider.GetUiName(rule) + " *" : 
                    GameRuleProvider.GetUiName(rule);
                AddMenuItem(new MenuItem(i, uiName, () =>
                {
                    _gameSettings.BoatsCanTouch = rule;
                    GameSettingsController.SaveGameSettings();
                    RefreshMenuItems(AddGameSettingsToMenuItems);
                    return "";
                }));
                i++;
            }
        }
    }
}
