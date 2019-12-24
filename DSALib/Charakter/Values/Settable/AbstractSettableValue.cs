using DSAProject.Classes.Interfaces;

namespace DSALib.Charakter.Values.Settable
{
    public abstract class AbstractSettableValue : IValue
    {
        public abstract int Value { get; }
        public abstract string Name { get; }
        public abstract string InfoText { get; }
    }
}
