using System;
using System.Collections.Generic;
using Battleship.ConsoleUi;
using Menu;

namespace Battleship.Menus
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
            
            string userChoice;
            ConsoleKey? keyPressed;
            do
            {
                Console.Clear();
                BattleshipUi.DrawBoard(_gameEngine.GetCurrentEnemyState().PlayerBoard);
                BattleshipUi.ShowEnemyBoardMessage(_gameEngine.GetCurrentEnemyState().Player.Name);
                BattleshipUi.ShowCurrentPlayerName(_gameEngine.GetCurrentPlayerState().Player.Name);
                MenuUi.ShowMenuItems(MenuItems, PointerLocation);
                MenuUi.ShowPressKeyMessage();
                
                keyPressed = HandleKeyPress();
                
                var item = MenuItems.GetValueOrDefault(PointerLocation);
                userChoice = item!.MethodToExecute();
                if (!userChoice.Equals("HitResponse")) continue;
                BattleshipUi.ShowNextMoveByMessage(_gameEngine.GetCurrentPlayerState().Player.Name);
                keyPressed = null;
            } while (
                keyPressed != ConsoleKey.Enter && NotReturn(userChoice)
            );
            
            return userChoice;
        }

        private string MakeAHit()
        {
            var hitMenu = new HitController(_gameEngine);
            return hitMenu.Run();
        }

        private void AddMenuActions()
        {
            AddMenuItems(new List<MenuItem>
            {
                new(1, "Make a move", MakeAHit),
                new(2, "Save a game", DefaultMenuAction),
                new(3, "Return to main menu", DefaultMenuAction)
            });
        }
    }
}
