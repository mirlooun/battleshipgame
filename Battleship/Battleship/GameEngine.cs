using System.Collections.Generic;
using System.Linq;
using Battleship.Domain;
using Battleship.Helpers;

namespace Battleship
{
    public class GameEngine
    {
        private readonly Dictionary<bool, Player> _players = new();

        private readonly GameSettings _gs;
        private bool NextMoveByFirst { get; set; } = true;
        public GameEngine(GameSettings gs, Player first, Player second)
        {
            _gs = gs;
            first.PlayerBoard = GenerateEmptyBoard();
            second.PlayerBoard = GenerateEmptyBoard();
            _players.Add(NextMoveByFirst, first);
            _players.Add(!NextMoveByFirst, second);
        }
        public HitResponse MakeAHit(LocationPoint hit)
        {
            var targetBoat = FindTargetBoat(GetCurrentEnemy().GetBoats(), hit);

            if (targetBoat == null)
            {
                GetCurrentEnemy().PlayerBoard[hit.X, hit.Y] = ECellState.Miss;
                ChangeNextMove();
                return new HitResponse();
            }
            
            targetBoat.MakeAHit(hit);
            
            return new HitResponse(targetBoat.GetName(), targetBoat.GetHp());
        }
        public PlayerState GetCurrentPlayerState()
        {
            var player = GetCurrentPlayer();

            var playerState = new PlayerState(player, GetPlayerBoard(player));
            
            return playerState;
        }
        public PlayerState GetCurrentEnemyState()
        {
            var enemyPlayer = GetCurrentEnemy();
            
            var playerState = new PlayerState(enemyPlayer, GetPlayerBoard(enemyPlayer));
            
            return playerState;
        }
        private Player GetCurrentPlayer()
        {
            return _players[NextMoveByFirst];
        }
        private Player GetCurrentEnemy()
        {
            return _players[!NextMoveByFirst];
        }
        private void ChangeNextMove()
        {
            NextMoveByFirst = !NextMoveByFirst;
        }
        private static ECellState[,] GetPlayerBoard(Player player)
        {
            var playerBoard = player.PlayerBoard;

            foreach (var location in player.GetBoats().SelectMany(boat => boat.Locations))
            {
                playerBoard[location.X,location.Y] = location.PointState;
            }

            return playerBoard;
        }
        private static Boat? FindTargetBoat(IEnumerable<Boat> boats, Location hit)
        {
            var boat = boats.FirstOrDefault(
                boat => boat.Locations
                    .FirstOrDefault(pl => pl.X.Equals(hit.X) && pl.Y.Equals(hit.Y)) != null
                );
            return boat;
        }
        private ECellState[,] GenerateEmptyBoard()
        {
            var board = new ECellState[
                _gs.FieldHeight,
                _gs.FieldWidth
            ];

            for (var i = 0; i < board.GetUpperBound(1); i++)
            {
                for (var j = 0; j < board.GetUpperBound(0); j++)
                {
                    board[i, j] = ECellState.Empty;
                }
            }

            return board;
        }
    }
}
