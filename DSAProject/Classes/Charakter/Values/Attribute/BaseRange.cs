﻿using DSALib;
using DSALib.Utils;
using DSAProject.Classes.Interfaces;
using DSAProject.util.ErrrorManagment;

namespace DSAProject.Classes.Charakter.Values.Attribute
{
    public class BaseRange : AbstractAttributeValues
    {
        public BaseRange(ICharakterAttribut attribute) : base(attribute) { }
        public override string Name => "Fernkampf-Basis";
        protected override double Calculate()
        {
            var var1 = Attribute.GetAttributMAXValue(CharakterAttribut.Intuition, out Error error);
            Logger.Log(error, nameof(BaseRange), nameof(Calculate));
            var var2 = Attribute.GetAttributMAXValue(CharakterAttribut.Fingerfertigkeit, out error);
            Logger.Log(error, nameof(BaseRange), nameof(Calculate));
            var var3 = Attribute.GetAttributMAXValue(CharakterAttribut.Körperkraft, out error);
            Logger.Log(error, nameof(BaseRange), nameof(Calculate));

            return (var1 + var2 + var3) / 5.0;
        }
    }
}
