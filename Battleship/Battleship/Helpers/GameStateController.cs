using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Battleship.Domain;
using Battleship.DTO;

namespace Battleship.Helpers
{
    public class GameStateController : BaseIoController, IGameStateController
    {
        private readonly string _directoryNameStandardConfig;

        public GameStateController(string[] path)
        {
            var basePath = path.Length == 1 ? path[0] : BasePath;

            _directoryNameStandardConfig = basePath +
                                           System.IO.Path.DirectorySeparatorChar +
                                           "Saves" +
                                           System.IO.Path.DirectorySeparatorChar;

            if (!System.IO.Directory.Exists(_directoryNameStandardConfig))
            {
                System.IO.Directory.CreateDirectory(_directoryNameStandardConfig);
            }
        }

        public void SaveGameToLocal(GameEngine gameEngine)
        {
            var gameEngineDto = new GameEngineDto
            {
                Players = new List<PlayerDto>()
                {
                    new ()
                    {
                        Name = gameEngine.Players[0].Name,
                        Boats = gameEngine.Players[0].GetBoats()
                            .Select(boat => new BoatDto()
                            {
                                IsPlaced = true,
                                Locations = boat.Locations,
                                Type = boat.Type
                            }).ToList(),
                        MadeHits = gameEngine.Players[0].GetHits()
                            .Select(hit => new LocationPoint(hit.X, hit.Y, hit.PointState))
                            .ToList()
                    },
                    new ()
                    {
                        Name = gameEngine.Players[1].Name,
                        Boats = gameEngine.Players[1].GetBoats()
                            .Select(boat => new BoatDto()
                            {
                                IsPlaced = true,
                                Locations = boat.Locations,
                                Type = boat.Type
                            }).ToList(),
                        MadeHits = gameEngine.Players[1].GetHits()
                            .Select(hit => new LocationPoint(hit.X, hit.Y, hit.PointState))
                            .ToList()
                    }
                },
                NextMoveByFirst = gameEngine.NextMoveByFirst,
                GameSettings = gameEngine.Gs
            };
            var confJsonStr = JsonSerializer.Serialize(gameEngineDto, GetJsonSerializerOptions());
            var saveFileName =   "save_" + DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss") + ".json";
            System.IO.File.WriteAllText(_directoryNameStandardConfig + saveFileName, confJsonStr);
        }

        public GameEngine LoadGameFromLocal()
        {
            throw new System.NotImplementedException();
        }

        public void SaveGameToDatabase(GameEngine gameEngine)
        {
            throw new System.NotImplementedException();
        }

        public GameEngine LoadGameFromDataBase()
        {
            throw new System.NotImplementedException();
        }
    }

    public interface IGameStateController
    {
        public void SaveGameToLocal(GameEngine gameEngine);
        public GameEngine LoadGameFromLocal();

        public void SaveGameToDatabase(GameEngine gameEngine);
        public GameEngine LoadGameFromDataBase();
    }
}