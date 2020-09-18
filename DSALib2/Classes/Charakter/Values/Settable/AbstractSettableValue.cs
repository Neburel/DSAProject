
using DSALib2.Interfaces.Charakter;

namespace DSALib2.Classes.Charakter.Values.Settable
{
    public abstract class AbstractSettableValue : IValue
    {
        public abstract int Value { get; }
        public abstract string Name { get; }
        public abstract string ShortName { get; }
        public abstract string InfoText { get; }
    }
}
