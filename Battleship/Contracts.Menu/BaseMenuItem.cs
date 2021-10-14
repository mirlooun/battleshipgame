using System;

namespace Contracts.Menu
{
    public abstract class BaseMenuItem : IMenuItem
    {
        protected BaseMenuItem(int itemIndex, string label, Func<string> methodToExecute)
        {
            ItemIndex = itemIndex;
            Label = label;
            MethodToExecute = methodToExecute;
        }

        public int ItemIndex { get; }
        public string Label { get; }
        public Func<string> MethodToExecute { get; }
    }
}
