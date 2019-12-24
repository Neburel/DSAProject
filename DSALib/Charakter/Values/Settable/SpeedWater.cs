using DSALib.Charakter.Values.Settable;

namespace DSAProject.Classes.Charakter.Values
{
    public class SpeedWater : AbstractSettableValue
    {
        public override int Value { get; }
        public override string Name => "Geschwindigkeit Wasser";
        public override string InfoText => "";
    }
}
