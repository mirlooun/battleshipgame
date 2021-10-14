using System;
using System.Collections.Generic;
using Battleship.Domain;

namespace Battleship
{
    public class GameSettings
    {
        public int FieldHeight { get; set; }
        public int FieldWidth { get; set; }
        public EBoatCanTouch BoatsCanTouch { get; set; }
    }

    public enum EBoatCanTouch
    {
        BoatsCanTouch,
        BoatsCantTouch
    }

    public static class GameRuleProvider
    {
        private static readonly Dictionary<EBoatCanTouch, string> Rules =
            new()
            {
                { EBoatCanTouch.BoatsCanTouch, "Boats can touch" },
                { EBoatCanTouch.BoatsCantTouch, "Boats can't touch" },
                
            };

        public static string GetUiName(EBoatCanTouch type)
        {
            return Rules[type];
        }
    }
}
