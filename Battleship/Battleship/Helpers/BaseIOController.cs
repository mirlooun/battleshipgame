using System.Text.Json;

namespace Battleship.Helpers
{
    public class BaseIoController
    {
        protected static string BasePath => System.IO.Directory.GetCurrentDirectory();
        
        protected static JsonSerializerOptions GetJsonSerializerOptions()
        {
            var jsonOptions = new JsonSerializerOptions()
            {
                WriteIndented = true
            };

            return jsonOptions;
        }
    }
}
