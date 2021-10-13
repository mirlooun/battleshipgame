using System;
using Battleship;
using Battleship.ConsoleUi;
using Battleship.Domain;
using Battleship.Helpers;
using Menu;

namespace InitMenu
{
    public sealed class BoatPlacementScreenProvider : Menu.Menu
    {
        private readonly ECellState[,] _presentationalBoard;
        private readonly GameSettings _gameSettings;
        private Boat? _preliminaryBoat;

        public BoatPlacementScreenProvider(ECellState[,] presentationalBoard, GameSettings gameSettings) : base(MenuLevel.InitBoats, "")
        {
            _presentationalBoard = presentationalBoard;
            _gameSettings = gameSettings;
        }

        public Boat PlaceBoatScreen(EBoatType boatType)
        {
            _preliminaryBoat = new Boat(boatType);

            ConsoleKey? keyPressed;
            var isValid = false;
            do
            {
                Console.Clear();
                BattleshipUi.DrawSingleBoard(_presentationalBoard, _preliminaryBoat);
                MenuUi.ShowMenuLabelInContext(typeof(BoatPlacementScreenProvider));
                MenuUi.ShowMenuItems(MenuItems, PointerLocation);
                MenuUi.ShowPressKeyMessage();

                keyPressed = HandleKeyPress();
                if (keyPressed != ConsoleKey.Enter) continue;
                isValid = BoatLocationValidator.IsBoatLocationOccupied(_preliminaryBoat, _presentationalBoard);
                if (isValid == false) MenuUi.ShowWarningMessage(new CellIsOccupiedException());
                keyPressed = null;
            } while (
                keyPressed != ConsoleKey.Enter && isValid == false
            );

            AddCurrentBoatToPresentationalBoard();

            return _preliminaryBoat;
        }

        protected override ConsoleKey HandleKeyPress()
        {
            var keyPressed = Console.ReadKey();

            switch (keyPressed.Key)
            {
                case ConsoleKey.UpArrow:
                    BoatLocationChanger.TryDeltaMove(ref _preliminaryBoat!, 0, -1, _gameSettings);
                    break;
                case ConsoleKey.DownArrow:
                    BoatLocationChanger.TryDeltaMove(ref _preliminaryBoat!, 0, +1, _gameSettings);
                    break;
                case ConsoleKey.RightArrow:
                    BoatLocationChanger.TryDeltaMove(ref _preliminaryBoat!, +1, 0, _gameSettings);
                    break;
                case ConsoleKey.LeftArrow:
                    BoatLocationChanger.TryDeltaMove(ref _preliminaryBoat!, -1, 0, _gameSettings);
                    break;
                case ConsoleKey.R:
                    BoatLocationChanger.TryRotate(ref _preliminaryBoat!, _gameSettings);
                    break;
            }

            return keyPressed.Key;
        }

        private void AddCurrentBoatToPresentationalBoard()
        {
            foreach (var point in _preliminaryBoat!.Locations)
            {
                _presentationalBoard[point.X, point.Y] = ECellState.Ship;
            }
        }
    }
}
