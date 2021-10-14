using System.Text.Json;

namespace Battleship.Helpers
{
    public class BaseIoController
    {
        protected string BasePath => System.IO.Directory.GetCurrentDirectory();
        
        protected JsonSerializerOptions GetJsonSerializerOptions()
        {
            var jsonOptions = new JsonSerializerOptions()
            {
                WriteIndented = true
            };

            return jsonOptions;
        }
    }
}
