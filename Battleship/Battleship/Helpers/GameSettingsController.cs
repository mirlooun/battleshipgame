using System;
using System.Text.Json;

namespace Battleship.Helpers
{
    public class GameSettingsController : BaseIoController, IGameSettingsController
    {
        private readonly GameSettings? _gameSettings;

        private readonly string _fileNameStandardConfig;
        public GameSettingsController(string[] path)
        {
            
            var basePath = path.Length == 1 ? path[0] : BasePath;

            var defaultSettings = new GameSettings
            {
                FieldHeight = 10,
                FieldWidth = 10,
                BoatsCanTouch = EBoatCanTouch.BoatsCanTouch
            };

            _fileNameStandardConfig = basePath +
                                          System.IO.Path.DirectorySeparatorChar +
                                          "Configs" +
                                          System.IO.Path.DirectorySeparatorChar +
                                          "standard.json";

            var confJsonStr = JsonSerializer.Serialize(defaultSettings, GetJsonSerializerOptions());

            if (!System.IO.File.Exists(_fileNameStandardConfig))
            {
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
