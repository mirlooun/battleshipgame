using System.Collections.Generic;

namespace Battleship.Domain
{
    public abstract class Boat
    {
        public abstract string Name { get; }

        private int _health;

        public abstract List<LocationPoint> Locations { get; set; }

        public int GetHp()
        {
            return _health;
        }

        public void MakeAHit(LocationPoint hit)
        {
            var point = Locations.Find(pl =>
                pl.X.Equals(hit.X) && pl.Y.Equals(hit.Y));
            point!.PointState = ECellState.Hit;
            ReduceBoatHp();
        }

        private void ReduceBoatHp()
        {
            _health = Locations.FindAll(pl => pl.PointState == ECellState.Ship).Count;
        }

        public bool IsLocatedHere(int colIndex, int rowIndex)
        {
            return Locations.Exists(point => point.X == colIndex && point.Y == rowIndex);
        }
    }

    public class Carrier : Boat
    {
        public override string Name => "Carrier";
        public override List<LocationPoint> Locations { get; set; } = new(5);
    }

    public class Battleship : Boat
    {
        public override string Name => "Battleship";
        public override List<LocationPoint> Locations { get; set; } = new(4);
    }

    public class Cruiser : Boat
    {
        public override string Name => "Cruiser";
        public override List<LocationPoint> Locations { get; set; } = new(3);
    }

    public class Submarine : Boat
    {
        public override string Name => "Submarine";
        public override List<LocationPoint> Locations { get; set; } = new(3);
    }

    public class Patrol : Boat
    {
        public override string Name => "Patrol";
        public override List<LocationPoint> Locations { get; set; } = new(2);
    }
}