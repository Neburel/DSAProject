using DSALib.Charakter.Values.Settable;

namespace DSAProject.Classes.Charakter.Values
{
    public class SpeedLand : AbstractSettableValue
    {
        public override int Value { get; }
        public override string Name => "Geschwindigkeit";
        public override string InfoText => "";
    }
}
