using System;
using System.Collections.Generic;
using System.Linq;
using Battleship;
using Battleship.ConsoleUi;
using Battleship.Domain;
using Menu;

namespace InitMenu
{
    public sealed class NewPlayerBoatsUiProvider : Menu.Menu
    {
        private Dictionary<EBoatType, int> _remainingBoatCount = default!;

        private List<Boat> _placedBoats = default!;

        private ECellState[,] _presentationalBoard = default!;

        private readonly GameSettings _gameSettings;

        public NewPlayerBoatsUiProvider(GameSettings gameSettings) : base(MenuLevel.InitBoats, "")
        {
            _gameSettings = gameSettings;
        }

        public void PlaceBoatsScreen(Player player)
        {
            _placedBoats = new List<Boat>();
            GetPresentationalBoard();
            GetInitialBoatsList();
            AddBoatsToMenuItems();

            ConsoleKey? keyPressed;

            Console.Clear();
            do
            {
                ResetCursorPosition();
                BattleshipUi.DrawSingleBoard(_presentationalBoard);
                MenuUi.ShowPlayerNameInContext(player.Name, typeof(NewPlayerBoatsUiProvider));
                MenuUi.ShowMenuLabelInContext(typeof(NewPlayerBoatsUiProvider));
                MenuUi.ShowMenuItems(MenuItems, PointerLocation);
                MenuUi.ShowPressKeyMessage();

                keyPressed = HandleKeyPress();

                if (keyPressed != ConsoleKey.Enter) continue;
                var item = MenuItems.GetValueOrDefault(PointerLocation);
                item!.MethodToExecute();
            } while (keyPressed != ConsoleKey.Enter || IsAllBoatsPlaced());

            Console.Clear();

            player.SetPlayerBoats(_placedBoats);

            ResetMenu();
        }

        private void ResetMenu()
        {
            MenuItems.Clear();
            PointerLocation = 0;
        }

        private void GetPresentationalBoard()
        {
            _presentationalBoard = new ECellState[_gameSettings.FieldHeight, _gameSettings.FieldWidth];
        }

        private void GetInitialBoatsList()
        {
            _remainingBoatCount = _gameSettings.GetBoatsConfiguration();
        }

        private bool IsAllBoatsPlaced()
        {
            return _remainingBoatCount.Values.Sum() != 0;
        }

        private void AddBoatsToMenuItems()
        {
            var i = 1;
            var items = new List<MenuItem>();
            foreach (EBoatType bt in Enum.GetValues(typeof(EBoatType)))
            {
                if (!_remainingBoatCount.ContainsKey(bt)) continue;

                string toDraw = BoatTypeProvider.GetUiName(bt) + " - x" + _remainingBoatCount[bt];
                items.Add(new MenuItem(i, toDraw, () => AddBoatLocation(bt)));
                i++;
            }

            AddMenuItems(items);
        }

        private string AddBoatLocation(EBoatType boatType)
        {
            if (_remainingBoatCount[boatType] == 0)
            {
                MenuUi.ShowBoatCountWarningMessage(boatType.GetType());
            }
            else
            {
                var bpProvider = new BoatPlacementScreenProvider(_presentationalBoard, _gameSettings);
                var newBoat = bpProvider.PlaceBoatScreen(boatType);
                AddCurrentBoatToBoatsList(newBoat);
                AddCurrentBoatToPresentationalBoard(newBoat);
                _remainingBoatCount[boatType]--;
                RefreshMenuItems(AddBoatsToMenuItems);
            }

            return "";
        }

        private void AddCurrentBoatToBoatsList(Boat newBoat)
        {
            newBoat.SetPlaced();
            _placedBoats.Add(newBoat);
        }

        private void AddCurrentBoatToPresentationalBoard(Boat boat)
        {
            foreach (var point in boat.Locations)
            {
                _presentationalBoard[point.X, point.Y] = ECellState.Ship;
            }
        }
    }
}