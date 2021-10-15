using System.Collections.Generic;
using System.Linq;
using Battleship.Domain;
using Battleship.Helpers;

namespace Battleship
{
    public class GameEngine
    {
        public List<Player> Players { get; } = new(2);
        public GameSettings Gs { get; }
        public bool NextMoveByFirst { get; private set; } = true;
        public GameEngine(GameSettings gs, Player first, Player second)
        {
            Gs = gs;
            Players.Add(first);
            Players.Add(second);
        }
        
        public GameEngine(GameSettings gs, Player first, Player second, bool nextMoveByFirst)
        {
            Gs = gs;
            Players.Add(first);
            Players.Add(second);
            NextMoveByFirst = nextMoveByFirst;
        }
        public HitResponse MakeAHit(Location hit)
        {
            var attempt = GetCurrentPlayer().GetHits().Find(p => p.X.Equals(hit.X) && p.Y.Equals(hit.Y));
            if (attempt != null)
            {
                return new HitResponse(ECellState.Hit);
            }
            
            var targetBoat = FindTargetBoat(GetCurrentEnemy().GetBoats(), hit);

            if (targetBoat == null)
            {
                GetCurrentPlayer().AddHitToHistory(new LocationPoint(hit.X, hit.Y, ECellState.Miss));
                ChangeNextMove();
                return new HitResponse(ECellState.Miss);
            }
            
            targetBoat.MakeAHit(hit);
            GetCurrentPlayer().AddHitToHistory(new LocationPoint(hit.X, hit.Y, ECellState.Hit));
            
            return new HitResponse(targetBoat.GetName(), targetBoat.GetHp());
        }
        public string GetCurrentPlayerName()
        {
            return GetCurrentPlayer().Name;
        }
        
        public string GetCurrentEnemyName()
        {
            return GetCurrentEnemy().Name;
        }
        private Player GetCurrentPlayer()
        {
            var playerIndex = NextMoveByFirst ? 0 : 1;
            return Players[playerIndex];
        }
        private Player GetCurrentEnemy()
        {
            var playerIndex = !NextMoveByFirst ? 0 : 1;
            return Players[playerIndex];
        }
        private void ChangeNextMove()
        {
            NextMoveByFirst = !NextMoveByFirst;
        }
        private static Boat? FindTargetBoat(IEnumerable<Boat> boats, Location hit)
        {
            var boat = boats.FirstOrDefault(
                boat => boat.Locations
                    .FirstOrDefault(pl => pl.X.Equals(hit.X) && pl.Y.Equals(hit.Y)) != null
                );
            return boat;
        }

        public ECellState[,] GetCurrentEnemyBoardState()
        {
            var enemyBoats = GetCurrentEnemy().GetBoats();
            var playerHits = GetCurrentPlayer().GetHits();
            return PlayerBoardProvider.GetEnemyBoard(enemyBoats, playerHits, Gs);
        }
    }
}
