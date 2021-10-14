using System;
using System.Collections.Generic;
using Contracts.Menu;

namespace Menu
{
    public class Menu : BaseMenu, IMenu
    {
        protected readonly MenuLevel MenuLevel;

        protected readonly string Label;
        // TODO: make menu use list instead of dictionary
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
                MenuUi.ShowPressKeyMessage();
                
                keyPressed = HandleKeyPress();
                if (keyPressed != ConsoleKey.Enter) continue;
                var item = MenuItems.GetValueOrDefault(PointerLocation);
                userChoice = item!.MethodToExecute();

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
        protected virtual ConsoleKey HandleKeyPress()
        {
            var keyPressed = Console.ReadKey(true);

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
        protected void AddMenuLevelSpecificActions()
        {
            if (MenuLevel != MenuLevel.Root)
            {
                AddMenuItem(new MenuItem(MenuItems.Count + 1, "Return", () => "Return"));
            }
            AddMenuItem(new MenuItem(MenuItems.Count + 1, "Exit", () => "Exit"));
        }
        protected void AddMenuItem(IMenuItem menuItem)
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
            Console.Clear();
            Console.WriteLine("Choice not implemented yet..");
            return "";
        }
        protected void RefreshMenuItems(Action addCustomMenuItems)
        {
            MenuItems.Clear();
            addCustomMenuItems();
            AddMenuLevelSpecificActions();
            Console.Clear();
        }
    }
}
