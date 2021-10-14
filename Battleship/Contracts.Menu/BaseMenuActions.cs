using System;

namespace Contracts.Menu
{
    public abstract class BaseMenuActions
    {
        protected static bool NotExit(string userChoice)
        {
            return !userChoice.Equals("Exit");
        }

        protected static bool NotReturn(string userChoice)
        {
            return !userChoice.Equals("Return");
        }

        protected static bool IsDefault(string userChoice)
        {
            return userChoice.Equals("");
        }
    }
}
