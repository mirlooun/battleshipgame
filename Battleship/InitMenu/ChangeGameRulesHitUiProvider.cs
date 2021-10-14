using System;
using Battleship;
using Battleship.Helpers;
using Menu;

namespace InitMenu
{
    public class ChangeGameRulesHitUiProvider : Menu.Menu
    {
        private readonly GameSettingsController _gsc;
        private GameSettings GameSettings => _gsc.GetSettings();
        
        public ChangeGameRulesHitUiProvider(GameSettingsController gsc) : base(MenuLevel.LevelPlus, "Choose a game rule")
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
            foreach (EHitContinuousMove rule in Enum.GetValues(typeof(EHitContinuousMove)))
            {
                var uiName = GameSettings.HitContinuousMove == rule ?
                    GameRuleProvider.GetUiName(rule) + " *" : 
                    GameRuleProvider.GetUiName(rule);
                AddMenuItem(new MenuItem(i, uiName, () =>
                {
                    GameSettings.HitContinuousMove = rule;
                    _gsc.SaveSettings();
                    RefreshMenuItems(AddGameSettingsToMenuItems);
                    return "";
                }));;
                i++;
            }
        }
    }
}