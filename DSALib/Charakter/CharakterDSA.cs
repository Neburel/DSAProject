using DSALib;
using DSALib.Charakter;
using DSALib.Charakter.Resources;
using DSALib.Charakter.Values;
using DSALib.Interfaces;
using DSAProject.Classes.Charakter.Values;
using DSAProject.Classes.Charakter.Values.Attribute;
using DSAProject.Classes.Interfaces;
using System;
using System.Collections.Generic;

namespace DSAProject.Classes.Charakter
{
    public class CharakterDSA : AbstractCharakter
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
                CharakterAttribut.Körperkraft,
                CharakterAttribut.Sozialstatus
            };
            return new CharakterAttribute(list);
        }
        protected override CharakterValues CreateValues()
        {
            var list = new List<IValue>()
            {
                new BaseAttack(Attribute),
                new BaseParade(Attribute),
                new BaseBlock(Attribute),
                new BaseRange(Attribute),
                new BaseInitiative(Attribute),
                new ControllValue(),
                new ArtifactControl(Resources, Attribute),
                new WoundSwell(Attribute),
                new Rapture(),

                new SpeedLand()
            };
            return new CharakterValues(list);
        }
        protected override CharakterResources CreateResources()
        {
            var list = new List<IResource>()
            {
                new AstralEnergy(Attribute),
                new Endurance(Attribute),
                new KarmaEnergy(Attribute),
                new MagicResistance(Attribute),
                new Vitality(Attribute),
            };

            return new CharakterResources(list);
        }

        public CharakterDSA(Guid id) : base(id) { }
    }
}
