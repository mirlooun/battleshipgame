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

        

        public static void ShowInitPlayerMessage(bool firstWas)
        {
            var playerOrder = !firstWas ? "first" : "second";
            Console.WriteLine($" Write down {playerOrder} player name");
            Console.WriteLine();
            Console.Write(">> ");
        }

        public static void ShowPlayerNameInContext(string playerName, Type type)
        {
            if (type.Name.Equals("NewPlayerBoatsUiProvider"))
            {
                Console.Write(" Player ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write($"{playerName}");
                Console.ResetColor();
                Console.Write(" places boats");
            }

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
        }

        public static void ShowBoatCountWarningMessage(Type type)
        {
            Console.Clear();
            Console.WriteLine($"All boats of type {type.Name} have been placed to board!");
            Wait();
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
            Console.Title = "Setup players";
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
            Console.Title = "Settings";
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

        public static void ShowPressRKeyMessage()
        {
            Console.WriteLine(" Press 'R' to rotate a boat");
        }
    }

    class MenuUiImpl : MenuUi
    {
    }
}