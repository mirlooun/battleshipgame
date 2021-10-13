using System.Collections.Generic;

namespace Battleship.Domain
{
    public class Player
    {
        private List<Boat>? _boats;
        public string Name { get; }
        public ECellState[,] PlayerBoard { get; set; } = default!;
        
        public Player(string name)
        {
            Name = name;
        }

        public void SetPlayerBoats(List<Boat> boats)
        {
            _boats = boats;
        }

        public List<Boat> GetBoats()
        {
            return _boats!;
        }
    }
}
