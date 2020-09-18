namespace DSALib2.Classes.Charakter.Values.Settable
{
    public class SpeedAir : AbstractSettableValue
    {
        public override int Value { get; }
        public override string Name => DSALib2.Resources.SpeedAir;
        public override string ShortName => Name;
        public override string InfoText => "";
    }
}
