using DSALib;

using DSAProject.Classes.Interfaces;
using DSAProject.util.ErrrorManagment;

namespace DSAProject.Classes.Charakter.Values.Attribute
{
    public class BaseParade : AbstractAttributeValues
    {
        public BaseParade(ICharakterAttribut attribute) : base(attribute) { }
        public override string Name => "Padrade-Basis";
        protected override double Calculate()
        {
            var var1 = Attribute.GetAttributMAXValue(CharakterAttribut.Intuition, out Error error);
            Logger.Log(error, nameof(BaseAttack), nameof(Calculate));
            var var2 = Attribute.GetAttributMAXValue(CharakterAttribut.Gewandheit, out error);
            Logger.Log(error, nameof(BaseAttack), nameof(Calculate));
            var var3 = Attribute.GetAttributMAXValue(CharakterAttribut.Körperkraft, out error);
            Logger.Log(error, nameof(BaseAttack), nameof(Calculate));

            return (var1 + var2 + var3) / 5.0;
        }
    }
}
