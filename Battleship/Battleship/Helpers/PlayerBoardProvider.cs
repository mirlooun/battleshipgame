using System.Collections.Generic;
using System.Linq;
using Battleship.Domain;

namespace Battleship.Helpers
{
    public static class PlayerBoardProvider
    {
        public static ECellState[,] GetEnemyBoard(List<Boat> enemyBoats, List<LocationPoint> playerHits, GameSettings gc)
        {
            var generatedBoard = GenerateEmptyBoard(gc);
            
            foreach (var locationPoint in enemyBoats.SelectMany(boat => boat.Locations))
            {
                generatedBoard[locationPoint.X, locationPoint.Y] = ECellState.Ship;
            }
            
            foreach (var hit in playerHits)
            {
                generatedBoard[hit.X, hit.Y] = hit.PointState;
            }

            return generatedBoard;
        }
        
        private static ECellState[,] GenerateEmptyBoard(GameSettings gs)
        {
            var board = new ECellState[
                gs.FieldHeight,
                gs.FieldWidth
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