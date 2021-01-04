using DSALib2.Classes.Charakter.Talente;
using DSALib2.Classes.Charakter.Talente.TalentFighting;
using DSALib2.Classes.Charakter.View;
using DSALib2.Interfaces.Charakter.Repository;
using DSALib2.Utils;
using System.Collections.Generic;

namespace DSALib2.Classes.Charakter.Repository.General
{
    public abstract class GeneralTraitRepository : ITraitRepository
    {
        private AbstractCharakter charakter;
        public GeneralTraitRepository(AbstractCharakter charakter){ this.charakter = charakter; }

        public abstract int GetValue(Interfaces.Charakter.IValue value);
        public abstract int GetAttribut(CharakterAttribut attribut);
        public abstract int GetResource(Interfaces.Charakter.IResource resource);
        public abstract int GetTaW(Interfaces.Charakter.ITalent value);
        public abstract int GetAT(AbstractTalentFighting value);
        public abstract int GetPA(AbstractTalentFighting value);
        public abstract int GetBL(AbstractTalentFighting value);

        public TraitView GetEmptyView()
        {
            var setAbleAttributes = charakter.Attribute.GetViewList();
            var attributList = new List<IDValueView<CharakterAttribut>>(); 
            foreach (var item in setAbleAttributes)
            {
                attributList.Add(new IDValueView<CharakterAttribut> { 
                    ID = item.ID,
                    Name = item.Name,
                    Value = 0
                });
            }

            var setAbleResources = charakter.Resources.GetList();
            var resourceList = new List<IDValueView<string>>();
            foreach (var item in setAbleResources)
            {
                var id = item.GetType().ToString();
                resourceList.Add(new IDValueView<string>
                {
                    ID = id,
                    Name = item.Name,
                    Value = 0
                });
            }

            var setAbleValues = charakter.Values.GetList();
            var valueList = new List<IDValueView<string>>();
            foreach (var item in setAbleValues)
            {
                var id = item.GetType().ToString();
                valueList.Add(new IDValueView<string>
                {
                    ID = id,
                    Name = item.Name,
                    Value = 0,
                });
            }

            return new TraitView()
            {
                APGain = 0,
                APInvest = 0,
                Type = TraitType.Keiner,
                AttributList = attributList,
                ResourceList = resourceList,
                ValueList = valueList,
                TalentList = new List<TalentView>(),
            };
        }
        public abstract List<TraitView> GetViewList();
        
        public abstract void SetByView(TraitView view);

        protected string GenerateLongDescription(TraitView trait)
        {
            var ret = trait.Description;
            foreach (var pair in trait.AttributList)
            {
                var value = pair.Value;
                ret = GenerateLongDescriptionHelper(value, ret, Helper.GetShort(pair.ID));
            }
            foreach (var pair in trait.ValueList)
            {
                var value = pair.Value;
                var type = DSAUtil.GetType(pair.ID);
                var item = charakter.Values.GetItemByType(type);                 
                ret = GenerateLongDescriptionHelper(value, ret, item.ShortName);
            }
            foreach (var pair in trait.ResourceList)
            {
                var value = pair.Value;
                var type = DSAUtil.GetType(pair.ID);
                var item = charakter.Resources.GetItemByType(type);
                ret = GenerateLongDescriptionHelper(value, ret, item.ShortName);
            }

            foreach (var pair in trait.TalentList)
            {
                var talent = charakter.Talente.Get(pair.ID);

                if(pair.TAW != 0)
                {
                    ret = GenerateLongDescriptionHelper(pair.TAW, ret, talent.Name + " TAW");
                }
                if (pair.AT != null && pair.AT != 0 && typeof(AbstractTalentFighting).IsAssignableFrom(talent.GetType()))
                {
                    ret = GenerateLongDescriptionHelper((int)pair.AT, ret, talent.Name + " AT");
                }

                if (pair.PA != null && pair.PA != 0 && (typeof(TalentClose).IsAssignableFrom(talent.GetType()) || typeof(TalentWeaponless).IsAssignableFrom(talent.GetType())))
                {
                    ret = GenerateLongDescriptionHelper((int)pair.PA, ret, talent.Name + " PA");
                }

                if (pair.BL != null && pair.BL != 0 && (typeof(TalentClose).IsAssignableFrom(talent.GetType()) || typeof(TalentWeaponless).IsAssignableFrom(talent.GetType())))
                {
                    ret = GenerateLongDescriptionHelper((int)pair.BL, ret, talent.Name + " BL");
                }
            }

            var adventurePointString = "Abenteuerpunkte";
            if (trait.APGain != 0)
            {
                ret = ret + " " + trait.APGain + adventurePointString;
            }
            if (trait.APInvest != 0)
            {
                ret = ret + " " + " -" + trait.APInvest + adventurePointString;
            }

            return ret;
        }
        protected string GenerateLongDescriptionHelper(int value, string returnValue, string addString)
        {
            if (value != 0)
            {
                if (string.IsNullOrEmpty(returnValue))
                {
                    returnValue = value + " " + addString;
                }
                else
                {
                    returnValue = returnValue + ", " + value + " " + addString;
                }
            }
            return returnValue;
        }
    }
}
