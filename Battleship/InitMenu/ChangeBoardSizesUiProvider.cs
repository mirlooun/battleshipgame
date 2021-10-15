using System;
using System.Collections.Generic;
using Battleship;
using Battleship.Helpers;
using Contracts.Menu;
using InitMenu.Utils;
using Menu;

namespace InitMenu
{
    public class ChangeBoardSizesUiProvider : Menu.Menu
    {
        private readonly GameSettings _gameSettings;
        private (bool, bool) _isSetting;
        public ChangeBoardSizesUiProvider() : base(MenuLevel.LevelPlus, "Setup board sizes")
        {
            _gameSettings = GameSettingsController.GetGameSettings();
        }

        public override string Run()
        {
            AddGameSettingsToMenuItems();
            AddMenuLevelDefaultActions();
            
            ConsoleKey? keyPressed;
            var userChoice = "";
            
            Console.Clear();
            do
            {
                ResetCursorPosition();
                if (MenuLevel == MenuLevel.Level1)
                {
                    MenuUi.ShowSettingsLogo();
                }
                else
                {
                    MenuUi.ShowGameLogo();
                }
                MenuUi.ShowMenuLabel(Label);
                MenuUi.ShowMenuItems(MenuItems, PointerLocation);
                
                if (_isSetting.Item1)
                {
                    SettingsUi.ShowPressKeyMessage();
                    SettingsUi.ShowPressEnterKeyMessageToSaveSettings();
                    keyPressed = SizeChanger(_isSetting.Item2);
                    
                    RefreshMenuItems(AddGameSettingsToMenuItems);
                    if (keyPressed == ConsoleKey.Enter)
                    {
                        _isSetting.Item1 = false;
                        GameSettingsController.SaveGameSettings();
                        keyPressed = null;
                        Console.Clear();
                        continue;
                    }
                }
                else
                {
                    MenuUi.ShowPressKeyMessage();
                    keyPressed = HandleKeyPress();
                }
                if (keyPressed != ConsoleKey.Enter) continue;
                var item = MenuItems[PointerLocation];
                userChoice = item.MethodToExecute();
            } while (
                keyPressed != ConsoleKey.Enter &&
                NotReturn(userChoice) ||
                MenuLevel == MenuLevel.Root &&
                NotExit(userChoice) ||
                IsDefault(userChoice)
            );
            Console.Clear();

            if (userChoice == "Return") userChoice = "";

            return !NotReturn(userChoice) ? "" : userChoice;
        }

        private void AddGameSettingsToMenuItems()
        {
            AddMenuItems(new List<MenuItem>
            {
                new (1, $"Change height - current: {_gameSettings.FieldHeight}", () =>
                {
                    Console.Clear();
                    _isSetting = (true, true);
                    return "";
                }),
                new (2, $"Change height - current: {_gameSettings.FieldWidth}", () =>
                {
                    Console.Clear();
                    _isSetting = (true, false);
                    return "";
                })
            });
        }
        private ConsoleKey SizeChanger(bool isHeight)
        {
            var keyPressed = Console.ReadKey(true);

            switch (keyPressed.Key)
            {
                case ConsoleKey.UpArrow:
                    if (isHeight)
                    {
                        if (_gameSettings.FieldHeight == 0) break;
                        _gameSettings.FieldHeight++;
                    }
                    else
                    {
                        if (_gameSettings.FieldWidth == 0) break;
                        _gameSettings.FieldWidth++;
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (isHeight)
                    {
                        if (_gameSettings.FieldHeight == 25) break;
                        _gameSettings.FieldHeight--;
                    }
                    else
                    {
                        if (_gameSettings.FieldWidth == 25) break;
                        _gameSettings.FieldWidth--;
                    }
                    break;
            }

            return keyPressed.Key;
        }
    }
}
