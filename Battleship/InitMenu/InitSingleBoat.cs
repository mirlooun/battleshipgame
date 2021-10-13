using System;
using Battleship;
using Battleship.ConsoleUi;
using Battleship.Domain;
using Battleship.Helpers;
using Menu;

namespace InitMenu
{
    public sealed class InitSingleBoat : Menu.Menu
    {
        private readonly ECellState[,] _presentationalBoard;
        private Boat? _currentBoat;

        public InitSingleBoat(ECellState[,] presentationalBoard) : base(MenuLevel.InitBoats, "")
        {
            _presentationalBoard = presentationalBoard;
        }

        public Boat AddBoatToBoard(Boat boat)
        {
            _currentBoat = boat;
            SetBoatDefaultLocations();

            ConsoleKey? keyPressed;
            var isValid = false;
            do
            {
                Console.Clear();
                BattleshipUi.DrawSingleBoard(_presentationalBoard, boat);
                MenuUi.ShowMenuLabelInContext(typeof(InitSingleBoat));
                MenuUi.ShowMenuItems(MenuItems, PointerLocation);
                MenuUi.ShowPressKeyMessage();

                keyPressed = HandleKeyPress();
                if (keyPressed != ConsoleKey.Enter) continue;
                isValid = BoatLocationValidator.IsBoatLocationOccupied(boat, _presentationalBoard);
                if (isValid == false) MenuUi.ShowWarningMessage(new CellIsOccupiedException());
                keyPressed = null;
            } while (
                keyPressed != ConsoleKey.Enter && isValid == false
            );

            AddCurrentBoatToPresentationalBoard();
            
            BoatLocationChanger.ResetDirection();

            return _currentBoat;
        }

        protected override ConsoleKey HandleKeyPress()
        {
            var keyPressed = Console.ReadKey();

            try
            {
                switch (keyPressed.Key)
                {
                    case ConsoleKey.UpArrow:
                        BoatLocationChanger.MoveUp(_currentBoat!, _presentationalBoard);
                        break;
                    case ConsoleKey.DownArrow:
                        BoatLocationChanger.MoveDown(_currentBoat!, _presentationalBoard);
                        break;
                    case ConsoleKey.RightArrow:
                        BoatLocationChanger.MoveRight(_currentBoat!, _presentationalBoard);
                        break;
                    case ConsoleKey.LeftArrow:
                        BoatLocationChanger.MoveLeft(_currentBoat!, _presentationalBoard);
                        break;
                    case ConsoleKey.R:
                        BoatLocationChanger.Rotate(_currentBoat!, _presentationalBoard);
                        break;
                }

                return keyPressed.Key;
            }
            catch (IndexOutOfRangeException)
            {
                return keyPressed.Key;
            }
        }

        private void AddCurrentBoatToPresentationalBoard()
        {
            foreach (var point in _currentBoat!.Locations)
            {
                _presentationalBoard[point.X, point.Y] = ECellState.Ship;
            }
        }

        private void SetBoatDefaultLocations()
        {
            for (var i = 0; i < _currentBoat!.Locations.Capacity; i++)
            {
                _currentBoat.Locations.Add(new LocationPoint
                {
                    X = i,
                    Y = 0,
                    PointState = ECellState.Ship
                });
            }
        }
    }
}
