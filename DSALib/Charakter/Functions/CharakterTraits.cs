﻿using DSALib.Charakter.Other;
using DSALib.Interfaces;
using DSAProject.Classes.Charakter.Talente;
using DSAProject.Classes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DSALib.Charakter
{
    public class CharakterTraits
    {
        #region Events
        public event EventHandler<IValue> ValueChanged;
        public event EventHandler<IResource> ResourceChanged;
        public event EventHandler<CharakterAttribut> AttributeChanged;
        public event EventHandler<ITalent> TaWChanged;
        public event EventHandler<AbstractTalentFighting> ATChanged;
        public event EventHandler<AbstractTalentFighting> PAChanged;
        public event EventHandler<AbstractTalentFighting> BLChanged;
        public event EventHandler<int> APEarnedChanged;
        public event EventHandler<int> APInvestChanged;
        #endregion
        #region Variables
        internal List<Trait> traits;
        #endregion
        public CharakterTraits()
        {
            traits = new List<Trait>();
        }
        public void AddTrait(Trait item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            if (!traits.Contains(item))
            {
                traits.Add(item);
            }
            item.AttributeChanged += (sender, args) =>
            {
                AttributeChanged?.Invoke(this, args);
            };
            item.ResourceChanged += (sender, args) =>
            {
                ResourceChanged?.Invoke(this, args);
            };
            item.ValueChanged += (sender, args) =>
            {
                ValueChanged?.Invoke(this, args);
            };
            item.TaWChanged += (sender, args) =>
            {
                TaWChanged?.Invoke(this, args);
            };
            item.ATChanged += (sender, args) =>
            {
                ATChanged?.Invoke(this, args);
            };
            item.PAChanged += (sender, args) =>
            {
                PAChanged?.Invoke(this, args);
            };
            item.APInvestChanged += (sender, args) =>
            {
                APInvestChanged?.Invoke(this, GetAPInvested());
            };
            item.APEarnedChanged += (sender, args) =>
            {
                APEarnedChanged?.Invoke(this, GetAPEarned());
            };

            CallChangedAll(item);
        }
        public void RemoveTrait(Trait item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            if (traits.Contains(item))
            {
                traits.Remove(item);
            }
            CallChangedAll(item);
        }
        public List<Trait> GetTraits()
        {
            return new List<Trait>(traits);
        }

        public int GetValue(CharakterAttribut item)
        {
            var ret = 0;
            foreach(var trait in traits)
            {
                ret = ret + trait.GetValue(item);
            }
            return ret;
        }
        public int GetValue(IValue item)
        {
            var ret = 0;
            foreach (var trait in traits)
            {
                ret = ret + trait.GetValue(item);
            }
            return ret;
        }
        public int GetValue(IResource item)
        {
            var ret = 0;
            foreach (var trait in traits)
            {
                ret = ret + trait.GetValue(item);
            }
            return ret;
        }

        public int GetAPEarned()
        {
            return traits.Select(x => x.APEarned).Sum();
        }
        public int GetAPInvested()
        {
            return traits.Select(x => x.APInvest).Sum();
        }

        public int GetTawBonus(ITalent item)
        {
            var ret = 0;
            foreach (var trait in traits)
            {
                ret = ret + trait.GetTawBonus(item);
            }
            return ret;
        }
        public int GetATBonus(AbstractTalentFighting item)
        {
            var ret = 0;
            foreach (var trait in traits)
            {
                ret = ret + trait.GetATBonus(item);
            }
            return ret;
        }
        public int GetPABonus(AbstractTalentFighting item)
        {
            var ret = 0;
            foreach (var trait in traits)
            {
                ret = ret + trait.GetPABonus(item);
            }
            return ret;
        }
        public int GetBLBonus(AbstractTalentFighting item)
        {
            var ret = 0;
            foreach (var trait in traits)
            {
                ret = ret + trait.GetBLBonus(item);
            }
            return ret;
        }

        private void CallChangedAll(Trait trait)
        {
            foreach(var item in trait.UsedAttributs())
            {
                AttributeChanged(this, item);
            }
            foreach (var item in trait.UsedValues())
            {
                ValueChanged(this, item);
            }
            foreach (var item in trait.UsedResources())
            {
                ResourceChanged(this, item);
            }
            foreach (var item in trait.GetTawBonus())
            {
                TaWChanged?.Invoke(this, item.Key);
            }
            foreach (var item in trait.GetATBonus())
            {
                ATChanged?.Invoke(this, item.Key);
            }
            foreach (var item in trait.GetPABonus())
            {
                PAChanged?.Invoke(this, item.Key);
            }
            foreach (var item in trait.GetBLBonus())
            {
                BLChanged?.Invoke(this, item.Key);
            }
            APInvestChanged?.Invoke(this, GetAPInvested());
            APEarnedChanged?.Invoke(this, GetAPEarned());

        }
    }
}
