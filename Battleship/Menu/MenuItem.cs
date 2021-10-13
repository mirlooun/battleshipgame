using System;
using Contracts.Menu;

namespace Menu
{
    public class MenuItem : IMenuItem
    {
        public int UserChoice { get; }
        public string Label { get; }
        public Func<string> MethodToExecute { get; }

        public MenuItem(int choice, string itemLabel, Func<string> methodToExecute)
        {
            if (string.IsNullOrEmpty(itemLabel)) throw new Exception("Menu item label can't be empty string!");
            UserChoice = choice;
            Label = itemLabel;
            MethodToExecute = methodToExecute;
        }
        
        public override string ToString()
        {
            return UserChoice + ") " + Label;
        }
    }
}
