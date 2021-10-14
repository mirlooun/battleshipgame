using System;

namespace Contracts.Menu
{
    public interface IMenuItem
    {
        public int ItemIndex { get; }
        public string Label { get; }
        public Func<string> MethodToExecute { get; }
    }
}
