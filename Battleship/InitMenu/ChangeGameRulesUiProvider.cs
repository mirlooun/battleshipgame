using System;
using System.Collections.Generic;
using Battleship;
using Battleship.Helpers;
using Menu;

namespace InitMenu
{
    public class ChangeGameRulesUiProvider : Menu.Menu
    {
        private readonly GameSettingsController _gsc;
        private GameSettings GameSettings => _gsc.GetSettings();

        public ChangeGameRulesUiProvider(GameSettingsController gsc) : base(MenuLevel.LevelPlus, "Choose a game rule")
        {
            _gsc = gsc;
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
                var uiName = GameSettings.BoatsCanTouch == rule ?
                    GameRuleProvider.GetUiName(rule) + " *" : 
                    GameRuleProvider.GetUiName(rule);
                AddMenuItem(new MenuItem(i, uiName, () =>
                {
                    GameSettings.BoatsCanTouch = rule;
                    _gsc.SaveSettings();
                    RefreshMenuItems(AddGameSettingsToMenuItems);
                    return "";
                }));;
                i++;
            }
        }
    }
}
