using System;

namespace Battleship.Helpers
{
    public static class HitLocationChanger
    {
        public static void MoveUp(Location hitLocation, ECellState[,] board)
        {
            try
            {
                var y = hitLocation.Y - 1;
                var unused = board[hitLocation.X, y];
                hitLocation.Y--;
            }
            catch (IndexOutOfRangeException)
            {
                throw new IndexOutOfRangeException();
            }
        }

        public static void MoveDown(Location hitLocation, ECellState[,] board)
        {
            try
            {
                var y = hitLocation.Y + 1;
                var unused = board[hitLocation.X, y];
                hitLocation.Y++;
            }
            catch (IndexOutOfRangeException)
            {
                throw new IndexOutOfRangeException();
            }
        }

        public static void MoveRight(Location hitLocation, ECellState[,] board)
        {
            try
            {
                var x = hitLocation.X + 1;
                var unused = board[x, hitLocation.X];
                hitLocation.X++;
            }
            catch (IndexOutOfRangeException)
            {
                throw new IndexOutOfRangeException();
            }
        }

        public static void MoveLeft(Location hitLocation, ECellState[,] board)
        {
            try
            {
                var x = hitLocation.X - 1;
                var unused = board[x, hitLocation.X];
                hitLocation.X--;
            }
            catch (IndexOutOfRangeException)
            {
                throw new IndexOutOfRangeException();
            }
        }
    }
}
