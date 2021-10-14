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

        public HitScreenProvider(GameEngine gameEngine) : base(MenuLevel.LevelPlus, "")
        {
            _gameEngine = gameEngine;
        }

        public override string Run()
        {
            ConsoleKey? keyPressed;
            
            Console.Clear();
            do
            {
                ResetCursorPosition();
                HitMenuUi.DrawSingleBoard(_gameEngine.GetCurrentEnemyBoardState(), _hitLocation);
                MenuUi.ShowMenuItems(MenuItems, PointerLocation);
                HitMenuUi.ShowLegend();
                MenuUi.ShowPressKeyMessage();

                keyPressed = HandleKeyPress();
                if (keyPressed != ConsoleKey.Enter) continue;
                
                var response = _gameEngine.MakeAHit(new Location
                (
                    _hitLocation.X,
                    _hitLocation.Y
                ));

                if (response.IsSamePlayerMove)
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
                        HitLocationChanger.MoveUp(_hitLocation, _gameEngine.GetCurrentEnemyBoardState());
                        break;
                    case ConsoleKey.DownArrow:
                        HitLocationChanger.MoveDown(_hitLocation, _gameEngine.GetCurrentEnemyBoardState());
                        break;
                    case ConsoleKey.RightArrow:
                        HitLocationChanger.MoveRight(_hitLocation, _gameEngine.GetCurrentEnemyBoardState());
                        break;
                    case ConsoleKey.LeftArrow:
                        HitLocationChanger.MoveLeft(_hitLocation, _gameEngine.GetCurrentEnemyBoardState());
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