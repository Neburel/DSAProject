using DSALib;
using DSAProject.util.ErrrorManagment;

namespace DSAProject.Classes.Charakter.Values.Attribute
{
    public class BaseInitiative : AbstractAttributeValues
    {
        public BaseInitiative(Interfaces.ICharakterAttribut attribute) : base(attribute) { }
        public override string Name => "Initiative-Basis";
        protected override double Calculate()
        {
            var var1 = Attribute.GetAttributMAXValue(CharakterAttribut.Mut, out Error error);
            Logger.Log(error, nameof(BaseInitiative), nameof(Calculate));
            var var2 = Attribute.GetAttributMAXValue(CharakterAttribut.Mut, out error);
            Logger.Log(error, nameof(BaseInitiative), nameof(Calculate));
            var var3 = Attribute.GetAttributMAXValue(CharakterAttribut.Intuition, out error);
            Logger.Log(error, nameof(BaseInitiative), nameof(Calculate));
            var var4 = Attribute.GetAttributMAXValue(CharakterAttribut.Gewandheit, out error);
            Logger.Log(error, nameof(BaseInitiative), nameof(Calculate));

            return (var1 + var2 + var3 + var4) / 5.0;
        }
    }
}
