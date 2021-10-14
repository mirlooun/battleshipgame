using System;
using Helpers;

namespace InitMenu.Utils
{
    public class WarningUi : BaseUi
    {
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
        
        public static void ShowValidatorResponse(InitPlayerResponse initPlayerResponse)
        {
            Console.Clear();
            Console.WriteLine(initPlayerResponse.Message);
            Wait();
        }
    }
}
