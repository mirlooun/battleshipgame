using System;
using Battleship.Domain;
using Helpers;
using Menu;

namespace InitMenu
{
    public sealed class InitPlayer : Menu.Menu
    {
        private bool _firstWas;
        public InitPlayer() : base(MenuLevel.InitPlayer, "")
        {
        }

        public Player GetPlayer()
        {
            string playerName;
            InitPlayerResponse initPlayerResponse;
            do
            {
                Console.Clear();
                MenuUi.ShowPlayerOrder(_firstWas);
                MenuUi.ShowInitPlayerMessage(_firstWas);
                playerName = Console.ReadLine()?.Trim()!;
                
                initPlayerResponse = PlayerValidator.IsNameValid(playerName);

                if (!initPlayerResponse.IsValid)
                {
                    MenuUi.ShowValidatorResponse(initPlayerResponse);
                }
                
            } while (!initPlayerResponse.IsValid);

            var player = new Player(playerName);

            _firstWas = true;
            
            return player;
        }
    }
}
