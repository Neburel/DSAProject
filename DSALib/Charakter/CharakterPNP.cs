using DSALib;

using System.Collections.Generic;

using DSAProject.Classes.Charakter.Values;
using DSAProject.Classes.Charakter.Values.Attribute;
using DSAProject.Classes.Interfaces;
using System;
using DSALib.Interfaces;

namespace DSAProject.Classes.Charakter
{
    public class CharakterPNP : AbstractCharakter
    {
        protected override CharakterAttribute CreateAttribute()
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
                CharakterAttribut.Körperkraft
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
            };
            return new CharakterValues(list);
        }

        protected override ICharakterResources CreateResources()
        {
            throw new NotImplementedException();
        }

        public CharakterPNP(Guid id) : base(id) { }
    }
}
