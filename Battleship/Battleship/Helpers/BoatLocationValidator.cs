using System.Linq;
using Battleship.Domain;

namespace Battleship.Helpers
{
    public static class BoatLocationValidator
    {
        public static bool IsBoatLocationOccupied(ECellState[,] board, Boat boat)
        {
            return boat.Locations.Any(point => board[point.X, point.Y] == ECellState.Ship);
        }
    }
}
