using System;
using System.Text.Json;
using Helpers;

namespace Battleship.Helpers
{
    public class GameStateController : BaseIoController, IGameStateController
    {
        private string _directoryNameStandardConfig;
        public GameStateController(string[] path)
        {
            var basePath = path.Length == 1 ? path[0] : BasePath;
            _directoryNameStandardConfig = basePath + 
                                           System.IO.Path.DirectorySeparatorChar + "Configs" +
                                           System.IO.Path.DirectorySeparatorChar + "standard.json";

            var directoryNameStandardConfig = basePath +
                                         System.IO.Path.DirectorySeparatorChar +
                                         "Saves";

            if (!System.IO.Directory.Exists(directoryNameStandardConfig))
            {
                System.IO.Directory.CreateDirectory(directoryNameStandardConfig);
            }
        }

        public void SaveGameToLocal(GameEngine gameEngine)
        {
            throw new System.NotImplementedException();
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
