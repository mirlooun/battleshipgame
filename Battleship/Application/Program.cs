using System;
using System.Collections.Generic;
using System.Threading;
using Battleship;
using Battleship.Menus;
using InitMenu;
using Menu;

namespace Application
{
    static class Program
    {
        private static GameSettings _gameSettings = new ()
        {
            FieldHeight = 10,
            FieldWidth = 10
        };
            
        static void Main()
        {
            Run();
            Exit();
        }

        private static void Run()
        {
            var menu = new Menu.Menu(MenuLevel.Level0, "Main menu");
            menu.AddMenuItems(new()
            {
                new MenuItem(1, "Start new game", RunBattleship),
                new MenuItem(2, "Load game", DefaultAction),
                new MenuItem(3, "Settings", RunSettingsMenu)
            });
            menu.Run();
        }

        private static string RunBattleship()
        {
            // Player creation screens
            var psProvider = new NewPlayerScreenProvider();

            var playerA = psProvider.NewPlayerScreen(false);

            var playerB = psProvider.NewPlayerScreen(true);
            
            // Init board screens

            var pbProvider = new NewPlayerBoatsUiProvider(_gameSettings);

            pbProvider.PlaceBoatsScreen(playerA);
            
            pbProvider.PlaceBoatsScreen(playerB);
            
            var gameEngine = new GameEngine(_gameSettings, playerA, playerB);
            
            var game = new BattleshipGame(gameEngine);
            
            return game.Run();
        }

        private static string RunSettingsMenu()
        {
            var menu = new Menu.Menu(MenuLevel.Level1, "Game settings");
            menu.AddMenuItems(new List<MenuItem>
            {
                new(1, "Board height", DefaultAction),
                new(2, "Board width", DefaultAction),
                new(3, "About", DefaultAction)
            });

            var userChoice = menu.Run();
            return userChoice;
        }

        private static void Exit()
        {
            Console.Clear();

            const string exitMsg = "Application is closing";
            Console.WriteLine(exitMsg);

            for (int i = 0; i < 3; i++)
            {
                Console.Write(".");
                Thread.Sleep(800);
            }
        }

        private static string DefaultAction()
        {
            Console.WriteLine("Choice not implemented yet...");
            Thread.Sleep(800);
            return "";
        }
    }
}
