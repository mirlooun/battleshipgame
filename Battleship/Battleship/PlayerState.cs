using Battleship.Domain;

namespace Battleship
{
    public class PlayerState
    {
        public readonly Player Player;
        public readonly ECellState[,] PlayerBoard;

        public PlayerState(Player player, ECellState[,] playerBoard)
        {
            Player = player;
            PlayerBoard = playerBoard;
        }
    }
}
