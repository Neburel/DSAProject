using DSALib.Charakter.Values.Settable;

namespace DSAProject.Classes.Charakter.Values
{
    public class SpeedAir : AbstractSettableValue
    {
        public override int Value { get; }
        public override string Name => "Geschwindigkeit Luft";
        public override string InfoText => "";
    }
}
