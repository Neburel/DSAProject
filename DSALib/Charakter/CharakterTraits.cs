using DSALib.Charakter.Other;
using DSALib.Interfaces;
using DSAProject.Classes.Interfaces;
using System;
using System.Collections.Generic;

namespace DSALib.Charakter
{
    public class CharakterTraits
    {
        #region Events
        public event EventHandler<IValue> ValueChanged;
        public event EventHandler<IResource> ResourceChanged;
        public event EventHandler<CharakterAttribut> AttributeChanged;
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
            if (!traits.Contains(item))
            {
                traits.Add(item);
            }
            item.AttributeChanged += (sender, args) =>
            {
                AttributeChanged(this, args);
            };
            item.ResourceChanged += (sender, args) =>
            {
                ResourceChanged(this, args);
            };
            item.ValueChanged += (sender, args) =>
            {
                ValueChanged(this, args);
            };

            CallChangedAll(item);
        }
        public void RemoveTrait(Trait item)
        {
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

        }
    }
}
