namespace DSALib2.Classes.Charakter.Values.Settable
{
    public class SpeedLand : AbstractSettableValue
    {
        public override int Value { get; }
        public override string Name => DSALib2.Resources.SpeedLand;
        public override string ShortName => Name;
        public override string InfoText => "";
    }
}
