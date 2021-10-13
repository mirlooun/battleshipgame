using System;
using Battleship;
using Battleship.Domain;

namespace InitMenu
{
    public static class BoatLocationChanger
    {
        private static EBoatDirection _direction = EBoatDirection.Horizontal;

        public static void ResetDirection()
        {
            _direction = EBoatDirection.Horizontal;
        }

        public static void MoveUp(Boat boat, ECellState[,] board)
        {
            try
            {
                foreach (var locationPoint in boat.Locations)
                {
                    var y = locationPoint.Y - 1;
                    var unused = board[locationPoint.X, y];
                    locationPoint.Y = y;
                }
            }
            catch (IndexOutOfRangeException)
            {
                throw new IndexOutOfRangeException();
            }
        }

        public static void MoveDown(Boat boat, ECellState[,] board)
        {
            try
            {
                foreach (var locationPoint in boat.Locations)
                {
                    var y = locationPoint.Y + 1;
                    var unused = board[locationPoint.X, y];
                    locationPoint.Y = y;
                }
            }
            catch (IndexOutOfRangeException)
            {
                throw new IndexOutOfRangeException();
            }
        }

        public static void MoveRight(Boat boat, ECellState[,] board)
        {
            try
            {
                foreach (var locationPoint in boat.Locations)
                {
                    if (_direction == EBoatDirection.Horizontal)
                    {
                        var x = locationPoint.X + 1;
                        var length = board.GetLength(0);
                        var index = boat.Locations.IndexOf(locationPoint);
                        var delta = x + boat.Locations.Count - index;
                        if (delta > length) throw new IndexOutOfRangeException();
                        locationPoint.X = x;
                    }
                    else
                    {
                        var x = locationPoint.X + 1;
                        var length = board.GetLength(0);
                        var index = boat.Locations.IndexOf(locationPoint);
                        var delta = x + 1 - index;
                        if (delta > length) throw new IndexOutOfRangeException();
                        locationPoint.X = x;
                    }
                }
            }
            catch (IndexOutOfRangeException)
            {
                throw new IndexOutOfRangeException();
            }
        }

        public static void MoveLeft(Boat boat, ECellState[,] board)
        {
            try
            {
                foreach (var locationPoint in boat.Locations)
                {
                    var x = locationPoint.X - 1;
                    var unused = board[x, locationPoint.Y];
                    locationPoint.X = x;
                }
            }
            catch (IndexOutOfRangeException)
            {
                throw new IndexOutOfRangeException();
            }
        }

        // TODO: implement
        public static void Rotate(Boat boat, ECellState[,] board)
        {
            foreach (var locationPoint in boat.Locations)
            {
                try
                {
                    int x;
                    int y;
                    ECellState unused;
                    switch (_direction)
                    {
                        case EBoatDirection.Vertical:
                            x = locationPoint.X;
                            y = locationPoint.Y;
                            unused = board[y, x];
                            locationPoint.X = y;
                            locationPoint.Y = x;
                            break;
                        case EBoatDirection.Horizontal:
                            x = locationPoint.X;
                            y = locationPoint.Y;
                            unused = board[y, x];
                            locationPoint.X = y;
                            locationPoint.Y = x;
                            break;
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    throw new IndexOutOfRangeException();
                }
            }

            _direction = _direction switch
            {
                EBoatDirection.Horizontal => EBoatDirection.Vertical,
                EBoatDirection.Vertical => EBoatDirection.Horizontal,
                _ => _direction
            };
        }
    }
}