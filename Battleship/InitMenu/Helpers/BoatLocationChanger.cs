using System;
using System.Linq;
using Battleship;
using Battleship.Domain;

namespace InitMenu
{
    public static class BoatLocationChanger
    {
        public static void TryDeltaMove(ref Boat boat, int deltaX, int deltaY, GameSettings gs)
        {
            var attempt = new Boat(boat);
            attempt.StartsAt.X += deltaX;
            attempt.StartsAt.Y += deltaY;

            if (IsValidPosition(attempt, gs))
            {
                boat = attempt;
            }
        }
        
        public static void TryRotate(ref Boat boat, GameSettings gs)
        {
            var oldLocations = boat.Locations;
            int offset = oldLocations.Count / 2;

            var newStartsAt = oldLocations[offset];
            if (boat.Direction == EBoatDirection.Horizontal)
            {
                newStartsAt.Y -= offset;
            }
            else
            {
                newStartsAt.X -= offset;
            }

            var attempt = new Boat(boat);
            attempt.StartsAt = newStartsAt;
            attempt.Direction = attempt.Direction switch
            {
                EBoatDirection.Horizontal => EBoatDirection.Vertical,
                EBoatDirection.Vertical => EBoatDirection.Horizontal,
                _ => throw new ArgumentOutOfRangeException()
            };

            if (IsValidPosition(attempt, gs))
            {
                boat = attempt;
            }
        }

        public static bool IsValidPosition(Boat boat, GameSettings gameSettings)
        {
            return boat.Locations.All(pos => pos.X >= 0 && 
                                             pos.Y >= 0 && 
                                             pos.X < gameSettings.FieldWidth && 
                                             pos.Y < gameSettings.FieldHeight
            );
        }
    }
}