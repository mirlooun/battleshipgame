using System;
using System.Collections.Generic;

namespace Battleship.Domain
{
    public enum EBoatType
    {
        Carrier = 0,
        Battleship = 1,
        Cruiser = 2,
        Submarine = 3,
        Patrol = 4
    }

    public static class BoatTypeProvider
    {
        private static readonly Dictionary<EBoatType, Tuple<string, int>> BoatTypes =
            new()
            {
                { EBoatType.Carrier, new Tuple<string, int>("Carrier", 5) },
                { EBoatType.Battleship, new Tuple<string, int>("Battleship", 4) },
                { EBoatType.Cruiser, new Tuple<string, int>("Cruiser", 3) },
                { EBoatType.Submarine, new Tuple<string, int>("Submarine", 3) },
                { EBoatType.Patrol, new Tuple<string, int>("Patrol", 2) }
            };

        public static string GetUiName(EBoatType type)
        {
            return BoatTypes[type].Item1;
        }

        public static int GetLength(EBoatType type)
        {
            return BoatTypes[type].Item2;
        }
    }

    public enum EBoatDirection
    {
        Vertical,
        Horizontal
    }

    public class Boat
    {
        public EBoatType Type { get; }

        private List<LocationPoint>? _locations;
        public List<LocationPoint> Locations
        {
            get
            {
                _locations ??= GenerateLocations(StartsAt, Type, Direction);
                return IsPlaced ? _locations : GenerateLocations(StartsAt, Type, Direction);
            }
            set => _locations = value;
        }

        private bool IsPlaced { get; set; }

        public void SetPlaced()
        {
            IsPlaced = true;
        }
        public Location StartsAt { get; set; }
        public EBoatDirection Direction { get; set; }

        private int _health;

        public Boat(EBoatType type)
        {
            Type = type;

            StartsAt = new Location(0, 0);
            Direction = EBoatDirection.Horizontal;

            _health = BoatTypeProvider.GetLength(type);
        }

        public Boat(Boat from)
        {
            Type = from.Type;
            StartsAt = new Location(from.StartsAt.X, from.StartsAt.Y);
            Direction = from.Direction;
            _health = from._health;
        }
        private static List<LocationPoint> GenerateLocations(Location start, EBoatType type, EBoatDirection direction)
        {
            var locations = new List<LocationPoint>();
            
            for (var i = 0; i < BoatTypeProvider.GetLength(type); i++)
            {
                var location = new LocationPoint(
                    start.X + (direction == EBoatDirection.Horizontal ? i : 0),
                    start.Y + (direction == EBoatDirection.Vertical ? i : 0),
                    ECellState.Ship
                );

                locations.Add(location);
            }

            return locations;
        }

        public string GetName()
        {
            return BoatTypeProvider.GetUiName(Type);
        }

        public int GetLength()
        {
            return BoatTypeProvider.GetLength(Type);
        }

        public int GetHp()
        {
            return _health;
        }

        public void MakeAHit(Location hit)
        {
            var point = Locations.Find(pl =>
                pl.X.Equals(hit.X) &&
                pl.Y.Equals(hit.Y));
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
}
