using System;
using System.Collections.Generic;
using System.Linq;
using Battleship;
using Battleship.ConsoleUi;
using Battleship.Domain;
using Menu;

namespace InitMenu
{
    public sealed class InitBoats : Menu.Menu
    {
        private Dictionary<string, int> _boatCount = new();

        private List<Boat>? _placedBoats;

        private ECellState[,]? _presentationalBoard;

        private readonly GameSettings _gameSettings;

        public InitBoats(GameSettings gameSettings) : base(MenuLevel.InitBoats, "")
        {
            _gameSettings = gameSettings;
        }

        public void GetBoats(Player player)
        {
            _placedBoats = new List<Boat>();
            GetPresentationalBoard();
            GetBoatsList();
            AddBoatsToMenuItems();

            ConsoleKey? keyPressed;
            do
            {
                Console.Clear();
                BattleshipUi.DrawSingleBoard(_presentationalBoard!);
                MenuUi.ShowPlayerNameInContext(player.Name, typeof(InitBoats));
                MenuUi.ShowMenuLabelInContext(typeof(InitBoats));
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
            _presentationalBoard = new ECellState[_gameSettings.Height, _gameSettings.Width];
        }

        private void GetBoatsList()
        {
            _boatCount = new Dictionary<string, int>
            {
                { nameof(Carrier), 1 },
                { nameof(Battleship.Domain.Battleship), 1 },
                { nameof(Submarine), 1 },
                { nameof(Patrol), 1 }
            };
        }

        private bool IsAllBoatsPlaced()
        {
            var sum = _boatCount.Values.Sum();

            return sum != 0;
        }

        private void AddBoatsToMenuItems()
        {
            AddMenuItems(new List<MenuItem>
            {
                new(1, nameof(Carrier) + $" - x{_boatCount[nameof(Carrier)]}", () => AddBoatLocation(new Carrier())),
                new(2, nameof(Battleship.Domain.Battleship) + $" - x{_boatCount[nameof(Battleship.Domain.Battleship)]}",
                    () => AddBoatLocation(new Battleship.Domain.Battleship())),
                new(3, nameof(Submarine) + $" - x{_boatCount[nameof(Submarine)]}",
                    () => AddBoatLocation(new Submarine())),
                new(4, nameof(Patrol) + $" - x{_boatCount[nameof(Patrol)]}", () => AddBoatLocation(new Patrol()))
            });
        }

        private string AddBoatLocation(Boat boat)
        {
            if (_boatCount[boat.Name] == 0)
            {
                MenuUi.ShowBoatCountWarningMessage(boat.GetType());
            }
            else
            {
                var addBoatsMenu = new InitSingleBoat(_presentationalBoard!);
                _placedBoats!.Add(addBoatsMenu.AddBoatToBoard(boat));
                RewriteMenuItems(boat);
            }

            return "";
        }

        private void RewriteMenuItems(Boat boat)
        {
            _boatCount[boat.Name]--;
            MenuItems.Clear();
            AddBoatsToMenuItems();
        }
    }
}
