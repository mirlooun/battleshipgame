using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Battleship;
using Battleship.ConsoleUi;
using Battleship.Domain;
using Menu;

namespace InitMenu
{
    public sealed class NewPlayerBoatsUiProvider : Menu.Menu
    {
        private Dictionary<EBoatType, int> _remainingBoatCount = new();

        private List<Boat>? _placedBoats;

        private ECellState[,]? _presentationalBoard;

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
            do
            {
                Console.Clear();
                BattleshipUi.DrawSingleBoard(_presentationalBoard!);
                MenuUi.ShowPlayerNameInContext(player.Name, typeof(NewPlayerBoatsUiProvider));
                MenuUi.ShowMenuLabelInContext(typeof(NewPlayerBoatsUiProvider));
                MenuUi.ShowMenuItems(MenuItems, PointerLocation);
                MenuUi.ShowPressKeyMessage();

                keyPressed = HandleKeyPress();

                if (keyPressed != ConsoleKey.Enter) continue;
                var item = MenuItems.GetValueOrDefault(PointerLocation);
                item!.MethodToExecute();
            } while (keyPressed != ConsoleKey.Enter || IsAllBoatsPlaced());

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
            // TODO: should use get_something from _gameSettings
            _remainingBoatCount = new Dictionary<EBoatType, int>
            {
                { EBoatType.Carrier, 1 },
                { EBoatType.Battleship, 1 },
                { EBoatType.Submarine, 1 },
                { EBoatType.Patrol, 1 }
            };
        }

        private bool IsAllBoatsPlaced()
        {
            var sum = _remainingBoatCount.Values.Sum();

            return sum != 0;
        }

        private void AddBoatsToMenuItems()
        {
            int i = 1;
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
                var bpProvider = new BoatPlacementScreenProvider(_presentationalBoard!, _gameSettings);
                _placedBoats!.Add(bpProvider.PlaceBoatScreen(boatType));
                RefreshMenuItems(boatType);
            }

            return "";
        }

        private void RefreshMenuItems(EBoatType boatType)
        {
            _remainingBoatCount[boatType]--;
            MenuItems.Clear();
            AddBoatsToMenuItems();
        }
    }
}
