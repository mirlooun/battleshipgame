using System;
using Contracts.Menu;

namespace Menu
{
    public class MenuItem : BaseMenuItem
    {

        public MenuItem(int choice, string itemLabel, Func<string> methodToExecute) : base(choice, itemLabel, methodToExecute)
        {
            if (string.IsNullOrEmpty(itemLabel)) throw new Exception("Menu item label can't be empty string!");
        }
        
        public override string ToString()
        {
            return UserChoice + ") " + Label;
        }
    }
}
