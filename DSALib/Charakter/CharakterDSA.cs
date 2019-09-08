using DSALib;

using DSAProject.Classes.Charakter.Values;
using DSAProject.Classes.Charakter.Values.Attribute;
using DSAProject.Classes.Interfaces;
using System;
using System.Collections.Generic;

namespace DSAProject.Classes.Charakter
{
    public class CharakterDSA : AbstractCharakter
    {
        protected override ICharakterAttribut CreateAttribute()
        {
            var list = new List<CharakterAttribut>
            {
                CharakterAttribut.Mut,
                CharakterAttribut.Klugheit,
                CharakterAttribut.Intuition,
                CharakterAttribut.Charisma,
                CharakterAttribut.Fingerfertigkeit,
                CharakterAttribut.Gewandheit,
                CharakterAttribut.Konstitution,
                CharakterAttribut.Körperkraft,
                CharakterAttribut.Sozialstatus
            };
            return new CharakterAttribute(list);
        }
        protected override ICharakterValues CreateValues()
        {
            var list = new List<IValue>()
            {
                new BaseAttack(Attribute),
                new BaseParade(Attribute),
                new BaseRange(Attribute),
                new BaseInitiative(Attribute),
                new ControllValue(),
                new WoundSwell(Attribute),
                new Rapture(),

                new SpeedLand()
            };
            return new CharakterValues(list);
        }
        public CharakterDSA(Guid id) : base(id) { }
    }
}
