using System;
using System.Collections.Generic;
using Battleship.ConsoleUi;
using Battleship.Helpers;
using Menu;

namespace Battleship.UiProviders
{
    public sealed class BattleshipGame : Menu.Menu
    {
        private readonly GameEngine _gameEngine;

        public BattleshipGame(GameEngine gameEngine) : base(MenuLevel.Battleship, "Battleship")
        {
            _gameEngine = gameEngine;
        }
        public override string Run()
        {
            AddMenuActions();

            ConsoleKey? keyPressed;
            var userChoice = "";
            
            Console.Clear();
            do
            {
                ResetCursorPosition();
                BattleshipUi.DrawBoard(_gameEngine.GetCurrentEnemyBoardState());
                BattleshipUi.ShowEnemyBoardMessage(_gameEngine.GetCurrentEnemyName());
                BattleshipUi.ShowCurrentPlayerName(_gameEngine.GetCurrentPlayerName());
                MenuUi.ShowMenuItems(MenuItems, PointerLocation);
                MenuUi.ShowPressKeyMessage();

                keyPressed = HandleKeyPress();

                if (keyPressed != ConsoleKey.Enter) continue;
                
                var item = MenuItems[PointerLocation];
                userChoice = item.MethodToExecute();
                if (userChoice.Equals("HitResponse"))
                {
                    BattleshipUi.ShowNextMoveByMessage(_gameEngine.GetCurrentPlayerName());
                    keyPressed = null;
                }
                
                if (userChoice.Equals("Saved"))
                {
                    keyPressed = null;
                }
            } while (
                keyPressed != ConsoleKey.Enter && NotReturn(userChoice)
            );
            Console.Clear();

            return userChoice;
        }
        
        private string MakeAHit()
        {
            var hitMenu = new HitScreenProvider(_gameEngine);
            return hitMenu.Run();
        }
        private string SaveGameStateToLocal()
        {
            GameStateController.SaveGameToLocal(_gameEngine);
            Console.Clear();
            Console.WriteLine("Game is saved");
            Wait();
            return "Saved";
        }
        
        private void AddMenuActions()
        {
            AddMenuItems(new List<MenuItem>
            {
                new (1, "Make a move", MakeAHit),
                new (2, "Save a game", SaveGameStateToLocal),
                new (3, "Return to main menu", () => "Return to main")
            });
        }
    }
}
