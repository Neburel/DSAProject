namespace DSALib2.Classes.Charakter.Values.Settable
{
    public class SpeedWater : AbstractSettableValue
    {
        public override int Value { get; }
        public override string Name => DSALib2.Resources.SpeedWater;
        public override string ShortName => Name;
        public override string InfoText => "";
    }
}
