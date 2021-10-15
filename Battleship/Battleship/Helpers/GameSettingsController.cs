using System.Collections.Generic;
using System.Text.Json;
using Battleship.Domain;

namespace Battleship.Helpers
{
    public static class GameSettingsController
    {
        private static string _pathRoot = System.IO.Directory.GetCurrentDirectory();
        
        private static GameSettings? _gameSettings;

        private static readonly JsonSerializerOptions? JsonOptions = new ()
        {
            WriteIndented = true
        };
        private static string FileStandardPath => FileStandardDirectoryLocation +
                                                  System.IO.Path.DirectorySeparatorChar +
                                                  "gameConfig.json";
        private static string FileStandardDirectoryLocation => _pathRoot +
                                                               System.IO.Path.DirectorySeparatorChar +
                                                               "Configs";
        public static GameSettings GetGameSettings()
        {
            if (_gameSettings != null) return _gameSettings;
            
            if (!System.IO.Directory.Exists(FileStandardDirectoryLocation))
            {
                System.IO.Directory.CreateDirectory(FileStandardDirectoryLocation);
            }
            
            if (System.IO.File.Exists(FileStandardPath))
            {
                var confText = System.IO.File.ReadAllText(FileStandardPath);
                _gameSettings = JsonSerializer.Deserialize<GameSettings>(confText);
            }
            else
            {
                var defaultSettings = GetDefaultGameSettings();
                
                var confJsonStr = JsonSerializer.Serialize(defaultSettings, JsonOptions);

                System.IO.File.WriteAllText(FileStandardPath, confJsonStr);
                
                _gameSettings = defaultSettings;
            }

            return _gameSettings!;
        }

        public static void SaveGameSettings()
        {
            var confJsonStr = JsonSerializer.Serialize(_gameSettings, JsonOptions);
            System.IO.File.WriteAllText(_pathRoot, confJsonStr);
        }

        public static void SetInitialPath(string[] args)
        {
            _pathRoot = args.Length == 1 ? args[0] : _pathRoot;
        }

        private static GameSettings GetDefaultGameSettings()
        {
            return new GameSettings
            {
                FieldHeight = 10,
                FieldWidth = 10,
                BoatsCanTouch = EBoatCanTouch.BoatsCanTouch,
                HitContinuousMove = EHitContinuousMove.HitContinuousMove,
                BoatsConfig = new List<BoatConfigurationDto>
                {
                    new() { BoatType = EBoatType.Carrier, BoatCount = 1 },
                    new() { BoatType = EBoatType.Cruiser, BoatCount = 1 },
                    new() { BoatType = EBoatType.Submarine, BoatCount = 1 },
                    new() { BoatType = EBoatType.Patrol, BoatCount = 1 }
                }
            };
        }
    }
}
