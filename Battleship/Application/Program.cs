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
            Height = 10,
            Width = 10
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
            // Initialize players
            var initPlayerMenu = new InitPlayer();

            var playerA = initPlayerMenu.GetPlayer();

            var playerB = initPlayerMenu.GetPlayer();
            
            // Initialize players boards

            var initPlayerBoatsMenu = new InitBoats(_gameSettings);

            initPlayerBoatsMenu.GetBoats(playerA);
            
            initPlayerBoatsMenu.GetBoats(playerB);
            
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
