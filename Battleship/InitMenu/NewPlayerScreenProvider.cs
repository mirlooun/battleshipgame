using System;
using Battleship.Domain;
using Helpers;
using InitMenu.Helpers;
using InitMenu.Utils;
using Menu;

namespace InitMenu
{
    public static class NewPlayerScreenProvider
    {
        public static Player NewPlayerScreen(bool isPlayerB)
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
                    WarningUi.ShowValidatorResponse(initPlayerResponse);
                }
                
            } while (!initPlayerResponse.IsValid);

            var player = new Player(playerName);

            return player;
        }
    }
}
