using System;
using System.Collections.Generic;
using System.Text.Json;
using Battleship.Domain;

namespace Battleship.Helpers
{
    public class GameSettingsController : BaseIoController, IGameSettingsController
    {
        private readonly GameSettings? _gameSettings;

        private readonly string _fileNameStandardConfig;

        public GameSettingsController(string[] path)
        {
            var basePath = path.Length == 1 ? path[0] : BasePath;


            _fileNameStandardConfig = basePath +
                                      System.IO.Path.DirectorySeparatorChar +
                                      "Configs" +
                                      System.IO.Path.DirectorySeparatorChar +
                                      "standard.json";

            if (!System.IO.File.Exists(_fileNameStandardConfig))
            {
                var defaultSettings = new GameSettings
                {
                    FieldHeight = 10,
                    FieldWidth = 10,
                    BoatsCanTouch = EBoatCanTouch.BoatsCanTouch,
                    HitContinuousMove = EHitContinuousMove.HitContinuousMove,
                    BoatsConfig = new List<BoatConfigurationDto>
                    {
                        new (){ BoatType = EBoatType.Carrier, BoatCount = 1 },
                        new (){ BoatType = EBoatType.Cruiser, BoatCount = 1 },
                        new (){ BoatType = EBoatType.Submarine, BoatCount = 1 },
                        new (){ BoatType = EBoatType.Patrol, BoatCount = 1 }
                    }
                };

                if (!System.IO.Directory.Exists(basePath + System.IO.Path.DirectorySeparatorChar + "Configs"))
                {
                    System.IO.Directory.CreateDirectory(basePath + System.IO.Path.DirectorySeparatorChar + "Configs");
                }
                
                var confJsonStr = JsonSerializer.Serialize(defaultSettings, GetJsonSerializerOptions());
                
                System.IO.File.WriteAllText(_fileNameStandardConfig, confJsonStr);
                _gameSettings = defaultSettings;
            }

            if (System.IO.File.Exists(_fileNameStandardConfig))
            {
                var confText = System.IO.File.ReadAllText(_fileNameStandardConfig);
                _gameSettings = JsonSerializer.Deserialize<GameSettings>(confText);
            }
        }

        public GameSettings GetSettings()
        {
            return _gameSettings!;
        }

        public void SaveSettings()
        {
            var confJsonStr = JsonSerializer.Serialize(_gameSettings, GetJsonSerializerOptions());
            System.IO.File.WriteAllText(_fileNameStandardConfig, confJsonStr);
        }
    }

    public interface IGameSettingsController
    {
        public GameSettings GetSettings();

        public void SaveSettings();
    }
}