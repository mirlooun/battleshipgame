using System.Collections.Generic;

namespace Battleship.DTO
{
    public class PlayerDto
    {
        public string? Name { get; set; }
        public List<BoatDto>? Boats { get; set; }
        public List<LocationPoint>? MadeHits { get; set; }
    }
}
