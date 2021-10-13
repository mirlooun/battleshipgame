using System;
using System.Collections.Generic;
using System.Linq;
using Contracts.Menu;
using Helpers;

namespace Menu
{
    public class MenuUi : BaseUi
    {
        public static void ShowMenuLabel(string label)
        {
            Console.WriteLine($"\n {label}\n");
        }

        public static void ShowMenuItems(Dictionary<int, IMenuItem> menuItems, int pointerLocation)
        {
            foreach (var item in menuItems.OrderBy(x => x.Value.UserChoice))
            {
                var itemValue = item.Value;
                if (itemValue.UserChoice - 1 == pointerLocation)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine(itemValue);
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(itemValue);
                }
            }

            Console.ResetColor();
        }

        public static void ShowPressKeyMessage()
        {
            Console.WriteLine("\n>> Use arrow keys to navigate");
        }

        public static void ShowValidatorResponse(InitPlayerResponse initPlayerResponse)
        {
            Console.Clear();
            Console.WriteLine(initPlayerResponse.Message);
            Wait();
        }

        public static void ShowInitPlayerMessage(bool firstWas)
        {
            var playerOrder = !firstWas ? "first" : "second";
            Console.WriteLine($" Write down {playerOrder} player name");
            Console.WriteLine();
            Console.Write(">> ");
        }

        public static void ShowPlayerNameInContext(string playerName, Type type)
        {
            var showMessage = "";

            if (type.Name.Equals("InitBoats"))
            {
                showMessage = $" Player {playerName} places boats";
            }

            Console.WriteLine(showMessage);
            Console.WriteLine();
        }

        public static void ShowMenuLabelInContext(Type type)
        {
            var showMessage = type.Name switch
            {
                "InitBoats" => " Choose a boat you want to place",
                "InitSingleBoat" => " Use arrows to place a boat",
                _ => ""
            };

            Console.WriteLine(showMessage);
            Console.WriteLine();
        }

        public static void ShowBoatCountWarningMessage(Type type)
        {
            Console.Clear();
            Console.WriteLine($"All boats of type {type.Name} have been placed to board!");
            Wait();
        }

        public static void ShowWarningMessage(Exception exception)
        {
            Console.Clear();
            switch (exception)
            {
                case IndexOutOfRangeException:
                    break;
                case CellIsOccupiedException:
                    Console.WriteLine("Cell is already occupied by another boat!");
                    Wait();
                    break;
            }
        }

        public static void ShowGameLogo()
        {
            Console.Title = "Battleship Primordial";
            const string title = @"
__________         __    __  .__                .__    .__        
\______   \_____ _/  |__/  |_|  |   ____   _____|  |__ |__|_____  
 |    |  _/\__  \\   __\   __\  | _/ __ \ /  ___/  |  \|  \____ \ 
 |    |   \ / __ \|  |  |  | |  |_\  ___/ \___ \|   Y  \  |  |_> >
 |______  /(____  /__|  |__| |____/\___  >____  >___|  /__|   __/ 
        \/      \/                     \/     \/     \/   |__|    
";
            Console.WriteLine(title);
        }

        public static void ShowPlayerOrder(bool first)
        {
            Console.Title = "ASCII art";
            var player = !first
                ? @"
______ _                         __  
| ___ \ |                       /  | 
| |_/ / | __ _ _   _  ___ _ __  `| | 
|  __/| |/ _` | | | |/ _ \ '__|  | | 
| |   | | (_| | |_| |  __/ |    _| |_
\_|   |_|\__,_|\__, |\___|_|    \___/
                __/ |                
               |___/                 
"
                : @"
______ _                         _____ 
| ___ \ |                       / __  \
| |_/ / | __ _ _   _  ___ _ __  `' / /'
|  __/| |/ _` | | | |/ _ \ '__|   / /  
| |   | | (_| | |_| |  __/ |    ./ /___
\_|   |_|\__,_|\__, |\___|_|    \_____/
                __/ |                  
               |___/                   
";
            Console.WriteLine(player);
        }

        public static void ShowSettingsLogo()
        {
            Console.Title = "ASCII art";
            const string title = @"
 _____      _   _   _                 
/  ___|    | | | | (_)                
\ `--.  ___| |_| |_ _ _ __   __ _ ___ 
 `--. \/ _ \ __| __| | '_ \ / _` / __|
/\__/ /  __/ |_| |_| | | | | (_| \__ \
\____/ \___|\__|\__|_|_| |_|\__, |___/
                             __/ |    
                            |___/     
";
            Console.WriteLine(title);
        }
    }

    class MenuUiImpl : MenuUi
    {
    }
}