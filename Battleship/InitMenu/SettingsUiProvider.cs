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
            AddMenuItems(new List<IMenuItem>
            {
                new MenuItem(1, $"Change board sizes - height:{GameSettings.FieldHeight} width:{GameSettings.FieldWidth}", ChangeBoardSizes),
                new MenuItem(2, $"Change game rules - {GameRuleProvider.GetUiName(GameSettings.BoatsCanTouch)}", ChangeGameRules),
                new MenuItem(3, "About", DefaultMenuAction)
            });
        }
        private string ChangeBoardSizes()
        {
            var menu = new ChangeBoardSizesUiProvider(_gsc);
            var userChoice = menu.Run();
            RefreshMenuItems(AddGameSettingsToMenuItems);
            return userChoice;
        }
        private string ChangeGameRules()
        {
            var menu = new ChangeGameRulesUiProvider(_gsc);
            var userChoice = menu.Run();
            RefreshMenuItems(AddGameSettingsToMenuItems);
            return userChoice;
        }
    }
}
