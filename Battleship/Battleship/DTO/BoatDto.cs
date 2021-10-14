using System.Collections.Generic;
using Battleship.Domain;

namespace Battleship.DTO
{
    public class BoatDto
    {
        public EBoatType Type { get; set; }

        public List<LocationPoint>? Locations { get; set; }

        public bool IsPlaced { get; set; }
    }
}
