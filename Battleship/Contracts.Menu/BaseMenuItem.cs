using System;

namespace Contracts.Menu
{
    public class BaseMenuItem : IMenuItem
    {
        protected BaseMenuItem(int userChoice, string label, Func<string> methodToExecute)
        {
            UserChoice = userChoice;
            Label = label;
            MethodToExecute = methodToExecute;
        }

        public int UserChoice { get; }
        public string Label { get; }
        public Func<string> MethodToExecute { get; }
    }
}
