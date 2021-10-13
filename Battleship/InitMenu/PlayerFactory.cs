using System;
using Battleship.Domain;
using Helpers;
using Menu;

namespace InitMenu
{
    public sealed class NewPlayerScreenProvider : Menu.Menu
    {
        public NewPlayerScreenProvider() : base(MenuLevel.InitPlayer, "")
        {
        }

        public Player NewPlayerScreen(bool isPlayerB)
        {
            string playerName;
            InitPlayerResponse initPlayerResponse;
            do
            {
                Console.Clear();
                MenuUi.ShowPlayerOrder(isPlayerB);
                MenuUi.ShowInitPlayerMessage(isPlayerB);
                playerName = Console.ReadLine()?.Trim()!;
                
                initPlayerResponse = PlayerValidator.IsNameValid(playerName);

                if (!initPlayerResponse.IsValid)
                {
                    MenuUi.ShowValidatorResponse(initPlayerResponse);
                }
                
            } while (!initPlayerResponse.IsValid);

            var player = new Player(playerName);

            return player;
        }
    }
}
