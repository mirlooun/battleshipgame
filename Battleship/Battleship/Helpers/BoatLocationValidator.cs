using System.Linq;
using Battleship.Domain;

namespace Battleship.Helpers
{
    public static class BoatLocationValidator
    {
        public static bool IsBoatLocationOccupied(Boat boat, ECellState[,] board)
        {
            return boat.Locations.All(point => board[point.X, point.Y] != ECellState.Ship);
        }
    }
}