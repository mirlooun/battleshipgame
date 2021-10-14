﻿using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using Battleship;
using Battleship.Helpers;
using Battleship.UiProviders;
using InitMenu;
using Menu;

namespace Application
{
    static class Program
    {
        private static GameStateControllerUnit? GSCU;
        private static GameSettings GameSettings => GSCU!.GSettingsController.GetSettings();

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool MoveWindow(IntPtr hWnd, int x, int y, int nWidth, int nHeight, bool bRepaint);

        static void Main(string[] args)
        {
            SetConsoleWindowPosition();
            InitializeGameStateControllerUnit(args);
            Run();
            Exit();
        }

        private static void SetConsoleWindowPosition()
        {
            var ptr = GetConsoleWindow();
            MoveWindow(ptr, 600, 250, 600, 600, true);
        }

        private static void InitializeGameStateControllerUnit(string[] path)
        {
            GSCU = new GameStateControllerUnit(path);
        }

        private static void Run()
        {
            var menu = new Menu.Menu(MenuLevel.Root, "Main menu");
            menu.AddMenuItems(new()
            {
                new MenuItem(1, "Start new game", RunBattleship),
                new MenuItem(2, "Load game", LoadGame),
                new MenuItem(3, "Settings", RunSettingsMenu)
            });
            menu.Run();
        }
        
        // TODO: implement
        private static string LoadGame()
        {
            // var menu = new LoadGameMenu();
            return "";
        }

        private static string RunBattleship()
        {
            // Player creation screens
            var playerA = NewPlayerScreenProvider.NewPlayerScreen(false);

            var playerB = NewPlayerScreenProvider.NewPlayerScreen(true);

            // Init board screens

            var pbProvider = new NewPlayerBoatsUiProvider(GameSettings);

            pbProvider.PlaceBoatsScreen(playerA);

            pbProvider.PlaceBoatsScreen(playerB);

            var gameEngine = new GameEngine(GameSettings, playerA, playerB);

            var game = new BattleshipGame(gameEngine);

            return game.Run();
        }

        private static string RunSettingsMenu()
        {
            var menu = new SettingsUiProvider(GSCU!.GSettingsController);
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
            Console.Clear();
            return "";
        }
    }
}