using System.Collections.Generic;

namespace Battleship.Domain
{
    public class Player
    {
        private List<Boat>? _boats;

        private List<LocationPoint> _madeHits = new();
        public string Name { get; }
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

        public List<LocationPoint> GetHits()
        {
            return _madeHits;
        }

        public void AddHitToHistory(LocationPoint locationPoint)
        {
            _madeHits.Add(locationPoint);
        }
    }
}
