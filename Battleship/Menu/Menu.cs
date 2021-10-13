using System;
using System.Collections.Generic;
using Contracts.Menu;

namespace Menu
{
    public class Menu
    {
        protected readonly MenuLevel MenuLevel;
        protected readonly string Label;
        protected readonly Dictionary<int, IMenuItem> MenuItems = new();
        protected int PointerLocation;
        public Menu(MenuLevel level, string label)
        {
            MenuLevel = level;
            Label = label;
        }
        public virtual string Run()
        {
            AddMenuLevelSpecificActions();

            ConsoleKey? keyPressed;
            var userChoice = "";
            do
            {
                Console.Clear();
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
                MenuUi.ShowPressKeyMessage();
                
                keyPressed = HandleKeyPress();

                if (keyPressed != ConsoleKey.Enter) continue;
                var item = MenuItems.GetValueOrDefault(PointerLocation);
                userChoice = item!.MethodToExecute();
            } while (
                keyPressed != ConsoleKey.Enter && NotReturn(userChoice)
                || MenuLevel == MenuLevel.Level0 && NotExit(userChoice) 
                || IsDefault(userChoice)
            );

            if (userChoice == "Return") userChoice = "";

            return userChoice;
        }
        private void AddMenuLevelSpecificActions()
        {
            if (MenuLevel != MenuLevel.Level0)
            {
                AddMenuItem(new MenuItem(MenuItems.Count + 1, "Return", () => "Return"));
            }
            AddMenuItem(new MenuItem(MenuItems.Count + 1, "Exit", () => "Exit"));
        }

        private bool NotExit(string userChoice)
        {
            return !userChoice.Equals("Exit");
        }

        protected bool NotReturn(string userChoice)
        {
            return !userChoice.Equals("Return");
        }

        private bool IsDefault(string userChoice)
        {
            return userChoice.Equals("");
        }
        protected virtual ConsoleKey HandleKeyPress()
        {
            var keyPressed = Console.ReadKey();

            switch (keyPressed.Key)
            {
                case ConsoleKey.UpArrow:
                    PointerLocation = PointerLocation == 0
                        ? PointerLocation = MenuItems.Count - 1
                        : --PointerLocation;
                    break;
                case ConsoleKey.DownArrow:
                    PointerLocation = PointerLocation == MenuItems.Count - 1
                        ? PointerLocation = 0
                        : ++PointerLocation;
                    break;
            }

            return keyPressed.Key;
        }

        private void AddMenuItem(IMenuItem menuItem)
        {
            MenuItems.Add(MenuItems.Count, menuItem);
        }
        public void AddMenuItems(List<MenuItem> menuItems)
        {
            foreach (var menuItem in menuItems)
            {
                MenuItems.Add(MenuItems.Count, menuItem);    
            }
        }

        protected string DefaultMenuAction()
        {
            Console.WriteLine("Not implemented yet.");
            return "";
        }
    }
}
