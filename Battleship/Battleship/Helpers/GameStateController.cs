using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Battleship.Domain;
using Battleship.DTO;
using Menu;

namespace Battleship.Helpers
{
    public static class GameStateController
    {
        private static string _pathRoot = System.IO.Directory.GetCurrentDirectory();

        private static readonly JsonSerializerOptions? JsonOptions = new ()
        {
            WriteIndented = true
        };
        private static string FileStandardDirectoryLocation => _pathRoot +
                                                               System.IO.Path.DirectorySeparatorChar +
                                                               "Saves";
        
        public static void SetInitialPath(string[] args)
        {
            _pathRoot = args.Length == 1 ? args[0] : _pathRoot;
        }
        public static void SaveGameToLocal(GameEngine gameEngine)
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
            var confJsonStr = JsonSerializer.Serialize(gameEngineDto, JsonOptions);
            var saveFileName =   "save_" + DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss") + ".json";
            System.IO.File.WriteAllText(FileStandardDirectoryLocation +
                                        System.IO.Path.DirectorySeparatorChar +
                                        saveFileName, confJsonStr);
        }

        public static GameEngine LoadGameFromLocal(string saveFilePath)
        {
            var saveText = System.IO.File.ReadAllText(saveFilePath);
            var gameEngineDto = JsonSerializer.Deserialize<GameEngineDto>(saveText)!;
            
            var gameSettings = new GameSettings
            {
                BoatsConfig = gameEngineDto.GameSettings!.BoatsConfig,
                BoatsCanTouch = gameEngineDto.GameSettings!.BoatsCanTouch,
                FieldHeight = gameEngineDto.GameSettings!.FieldHeight,
                FieldWidth = gameEngineDto.GameSettings!.FieldWidth,
                HitContinuousMove = gameEngineDto.GameSettings.HitContinuousMove
            };

            var playerABoats = gameEngineDto.Players![0].Boats!.Select(boat =>
            {
                var originalBoat = new Boat(boat.Type);
                originalBoat.SetPlaced();
                originalBoat.Locations = boat.Locations!;
                return originalBoat;
            }).ToList();
            
            var playerAHits = gameEngineDto.Players[0].MadeHits!;
            var playerAName = gameEngineDto.Players[0].Name!;
            
            var playerA = new Player(playerAName, playerABoats, playerAHits);
            
            var playerBBoats = gameEngineDto.Players![0].Boats!.Select(boat =>
            {
                var originalBoat = new Boat(boat.Type);
                originalBoat.SetPlaced();
                originalBoat.Locations = boat.Locations!;
                return originalBoat;
            }).ToList();
            
            var playerBHits = gameEngineDto.Players[0].MadeHits!;
            var playerBName = gameEngineDto.Players[0].Name!;
            
            var playerB = new Player(playerBName, playerBBoats, playerBHits);

            var nextMoveByFirst = gameEngineDto.NextMoveByFirst;
            
            var gameEngine = new GameEngine(gameSettings, playerA, playerB, nextMoveByFirst);

            return gameEngine;
        }
        
        public static List<MenuItem> GetGameSavesList(Func<GameEngine, string> delegateRunGame)
        {
            var menuItems = new List<MenuItem>();
            
            string[] filePaths = Directory.GetFiles(FileStandardDirectoryLocation);
            
            var i = 1;
            
            foreach (var file in filePaths)
            {
                var path = file.Split(System.IO.Path.DirectorySeparatorChar);
                var saveName = path[^1];
                menuItems.Add(new MenuItem(i, saveName, () =>
                {
                    var gameEngine = LoadGameFromLocal(file);
                    return delegateRunGame(gameEngine);
                }));
                i++;
            }

            return menuItems;
        }
    }
}
