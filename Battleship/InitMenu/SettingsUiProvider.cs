using System.Collections.Generic;
using Battleship;
using Battleship.Helpers;
using Contracts.Menu;
using Menu;

namespace InitMenu
{
    public class SettingsUiProvider : Menu.Menu
    {
        private readonly GameSettingsController _gsc;
        private GameSettings GameSettings => _gsc.GetSettings();
        public SettingsUiProvider(GameSettingsController gsc) : base(MenuLevel.Level1, "Game settings")
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
            AddMenuItems(new List<MenuItem>
            {
                new (1, $"Change board sizes - height:{GameSettings.FieldHeight} width:{GameSettings.FieldWidth}", ChangeBoardSizes),
                new (2, $"Change boat touch rules - {GameRuleProvider.GetUiName(GameSettings.BoatsCanTouch)}", ChangeTouchingRules),
                new (3, $"Change hit making rules - {GameRuleProvider.GetUiName(GameSettings.HitContinuousMove)}", ChangeHitMakingRules),
                new (4, "About", DefaultMenuAction)
            });
        }
        private string ChangeBoardSizes()
        {
            var menu = new ChangeBoardSizesUiProvider(_gsc);
            var userChoice = menu.Run();
            RefreshMenuItems(AddGameSettingsToMenuItems);
            return userChoice;
        }
        private string ChangeTouchingRules()
        {
            var menu = new ChangeGameTouchRulesUiProvider(_gsc);
            var userChoice = menu.Run();
            RefreshMenuItems(AddGameSettingsToMenuItems);
            return userChoice;
        }

        private string ChangeHitMakingRules()
        {
            var menu = new ChangeGameRulesHitUiProvider(_gsc);
            var userChoice = menu.Run();
            RefreshMenuItems(AddGameSettingsToMenuItems);
            return userChoice;
        }
    }
}
