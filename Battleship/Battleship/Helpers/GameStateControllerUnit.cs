namespace Battleship.Helpers
{
    public class GameStateControllerUnit
    {
        public GameStateController GStateController { get; }
        public GameSettingsController GSettingsController { get; }
        public GameStateControllerUnit(string[] args)
        {
            GStateController = new GameStateController(args);
            GSettingsController = new GameSettingsController(args);
        }
    }
}
