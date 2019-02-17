using DSAProject.util.ErrrorManagment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSAProject.Classes.Charakter.Values.Attribute
{
    public class WoundSwell : AbstractAttributeValues
    {
        public WoundSwell(Interfaces.ICharakterAttribut attribute) : base(attribute) { }
        public override string Name => "Wundschwelle";
        protected override double Calculate()
        {
            var var1 = Attribute.GetAttributMAXValue(CharakterAttribut.Konstitution, out Error error);
            Logger.Log(error, nameof(WoundSwell), nameof(Calculate));
            return (var1) / 2.0;
        }
    }
}
