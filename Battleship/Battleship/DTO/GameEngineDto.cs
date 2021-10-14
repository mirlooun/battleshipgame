using System.Collections.Generic;
using Battleship.Domain;

namespace Battleship.DTO
{
    public class GameEngineDto
    {
        public List<PlayerDto>? Players { get; set; }
        public GameSettings? GameSettings { get; set; }
        public bool NextMoveByFirst { get; set; }
    }
}
