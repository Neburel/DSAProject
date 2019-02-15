using DSAProject.Classes.Interfaces;
using DSAProject.util.ErrrorManagment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSAProject.Classes.Charakter.Values.Attribute
{
    public class BaseAttack : AbstractAttributeValues
    {
        public BaseAttack(ICharakterAttribut attribute) : base(attribute) { }
        public override string Name => "Attacke-Basis";
        protected override int Calculate()
        {
            var var1 = Attribute.GetAttributMAXValue(CharakterAttribut.Mut, out Error error);
            Logger.Log(error, nameof(BaseAttack), nameof(Calculate));
            var var2 = Attribute.GetAttributMAXValue(CharakterAttribut.Gewandheit, out error);
            Logger.Log(error, nameof(BaseAttack), nameof(Calculate));
            var var3 = Attribute.GetAttributMAXValue(CharakterAttribut.Körperkraft, out error);
            Logger.Log(error, nameof(BaseAttack), nameof(Calculate));

            return (var1 + var2 + var3) / 5;
        }
    }
}
