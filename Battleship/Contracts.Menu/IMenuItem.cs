using System;

namespace Contracts.Menu
{
    public interface IMenuItem
    {
        public int UserChoice { get; }
        public string Label { get; }

        public Func<string> MethodToExecute { get; }
    }
}
