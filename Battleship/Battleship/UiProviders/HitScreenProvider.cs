using System;
using Battleship.ConsoleUi;
using Battleship.Helpers;
using Menu;

namespace Battleship.UiProviders
{
    public sealed class HitScreenProvider : Menu.Menu
    {
        private readonly GameEngine _gameEngine;

        private readonly Location _hitLocation = new(0, 0);

        private readonly ECellState[,] _enemyBoard;

        public HitScreenProvider(GameEngine gameEngine) : base(MenuLevel.LevelPlus, "")
        {
            _gameEngine = gameEngine;
            _enemyBoard = gameEngine.GetCurrentEnemyState().PlayerBoard;
        }

        public override string Run()
        {
            ConsoleKey? keyPressed;
            
            Console.Clear();
            do
            {
                ResetCursorPosition();
                HitMenuUi.DrawSingleBoard(_gameEngine.GetCurrentEnemyState().PlayerBoard, _hitLocation);
                MenuUi.ShowMenuItems(MenuItems, PointerLocation);
                HitMenuUi.ShowLegend();
                MenuUi.ShowPressKeyMessage();

                keyPressed = HandleKeyPress();
                if (keyPressed != ConsoleKey.Enter) continue;
                var response = _gameEngine.MakeAHit(new LocationPoint
                (
                    _hitLocation.X,
                    _hitLocation.Y,
                    _gameEngine.GetCurrentEnemyState().PlayerBoard[_hitLocation.X, _hitLocation.Y]
                ));

                if (response.IsHit || response.IsDestroyed)
                {
                    BattleshipUi.ShowHitResponseMessage(response.GetMessage());
                    keyPressed = null;
                }
                else
                {
                    BattleshipUi.ShowHitResponseMessage(response.GetMessage());
                }
            } while (
                keyPressed != ConsoleKey.Enter
            );
            Console.Clear();

            return "HitResponse";
        }

        protected override ConsoleKey HandleKeyPress()
        {
            var keyPressed = Console.ReadKey(true);

            try
            {
                switch (keyPressed.Key)
                {
                    case ConsoleKey.UpArrow:
                        HitLocationChanger.MoveUp(_hitLocation, _enemyBoard);
                        break;
                    case ConsoleKey.DownArrow:
                        HitLocationChanger.MoveDown(_hitLocation, _enemyBoard);
                        break;
                    case ConsoleKey.RightArrow:
                        HitLocationChanger.MoveRight(_hitLocation, _enemyBoard);
                        break;
                    case ConsoleKey.LeftArrow:
                        HitLocationChanger.MoveLeft(_hitLocation, _enemyBoard);
                        break;
                }
            }
            catch (Exception)
            {
                return keyPressed.Key;
            }

            return keyPressed.Key;
        }
    }
}