
using DSALib.Interfaces;
using DSALib.Utils;
using DSAProject.Classes.Charakter.Talente;
using DSAProject.Classes.Interfaces;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Media;

namespace DSALib.Charakter.Other
{
    public class Trait
    {
        #region Events
        public event EventHandler<IValue> ValueChanged;
        public event EventHandler<IResource> ResourceChanged;
        public event EventHandler<CharakterAttribut> AttributeChanged;
        public event EventHandler<ITalent> TaWChanged;
        public event EventHandler<AbstractTalentFighting> ATChanged;
        public event EventHandler<AbstractTalentFighting> PAChanged;
        #endregion
        #region Variables
        private Dictionary<IValue, int> valueValues;
        private Dictionary<IResource, int> resourceValues;
        private Dictionary<CharakterAttribut, int> attributeValues;
        private Dictionary<ITalent, int> tawBonus;
        private Dictionary<AbstractTalentFighting, int> atBonus;
        private Dictionary<AbstractTalentFighting, int> paBonus;
        #endregion
        #region Properties
        public TraitType TraitType { get; set; }
        public SolidColorBrush SolidColorBrush
        {
            get;
            set;
        } 
        public string GP { get; set; }
        public string Value { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string LongDescription { get => GenerateLongDescription(); }
        #endregion
        public Trait()
        {
            valueValues = new Dictionary<IValue, int>();
            resourceValues = new Dictionary<IResource, int>();
            attributeValues = new Dictionary<CharakterAttribut, int>();

            tawBonus = new Dictionary<ITalent, int>();
            atBonus = new Dictionary<AbstractTalentFighting, int>();
            paBonus = new Dictionary<AbstractTalentFighting, int>();
        }

        internal List<CharakterAttribut> UsedAttributs()
        {
            return new List<CharakterAttribut>(attributeValues.Keys);
        }
        internal List<IValue> UsedValues()
        {
            return new List<IValue>(valueValues.Keys);
        }
        internal List<IResource> UsedResources()
        {
            return new List<IResource>(resourceValues.Keys);
        }

        public void SetValue(CharakterAttribut item, int value)
        {
            if (attributeValues.ContainsKey(item))
            {
                attributeValues.Remove(item);
            }
            attributeValues.Add(item, value);
            AttributeChanged?.Invoke(this, item);
        }
        public void SetValue(IValue item, int value)
        {
            if (valueValues.ContainsKey(item))
            {
                valueValues.Remove(item);
            }
            valueValues.Add(item, value);
            ValueChanged?.Invoke(this, item);
        }
        public void SetValue(IResource item, int value)
        {
            if (resourceValues.ContainsKey(item))
            {
                resourceValues.Remove(item);
            }
            resourceValues.Add(item, value);
            ResourceChanged?.Invoke(this, item);
        }

        public void SetTaWBonus(ITalent item, int value)
        {
            if(item != null)
            {
                tawBonus.Remove(item);
                tawBonus.Add(item, value);
                TaWChanged?.Invoke(this, item);
            }
        }
        public void SetATBonus(AbstractTalentFighting item, int value)
        {
            if (item != null)
            {
                atBonus.Remove(item);
                atBonus.Add(item, value);
                ATChanged?.Invoke(this, item);
            }
        }
        public void SetPABonus(AbstractTalentFighting item, int value)
        {
            if (item != null)
            {
                paBonus.Remove(item);
                paBonus.Add(item, value);
                PAChanged?.Invoke(this, item);
            }
        }

        public int GetValue(CharakterAttribut item)
        {
            var ret = -1;
            var regularValue = attributeValues.TryGetValue(item, out int currentValue);
            if (regularValue == false)
            {
                ret = 0;
            }
            else
            {
                ret = attributeValues[item];
            }
            return ret;
        }
        public int GetValue(IValue item)
        {
            var ret = -1;
            var regularValue = valueValues.TryGetValue(item, out int currentValue);
            if (regularValue == false)
            {
                ret = 0;
            }
            else
            {
                ret = valueValues[item];
            }
            return ret;
        }
        public int GetValue(IResource item)
        {
            var ret = -1;
            var regularValue = resourceValues.TryGetValue(item, out int currentValue);
            if (regularValue == false)
            {
                ret = 0;
            }
            else
            {
                ret = resourceValues[item];
            }
            return ret;
        }

        public Dictionary<ITalent, int> GetTawBonus()
        {
            return new Dictionary<ITalent, int>(tawBonus);
        }
        public Dictionary<AbstractTalentFighting, int> GetATBonus()
        {
            return new Dictionary<AbstractTalentFighting, int>(atBonus);
        }
        public Dictionary<AbstractTalentFighting, int> GetPABonus()
        {
            return new Dictionary<AbstractTalentFighting, int>(paBonus);
        }

        public int GetTawBonus(ITalent item)
        {
            if(tawBonus.TryGetValue(item, out int value))
            {
                return value;
            }
            else
            {
                return 0;
            }
        }
        public int GetATBonus(AbstractTalentFighting item)
        {
            if (atBonus.TryGetValue(item, out int value))
            {
                return value;
            }
            else
            {
                return 0;
            }
        }
        public int GetPABonus(AbstractTalentFighting item)
        {
            if (paBonus.TryGetValue(item, out int value))
            {
                return value;
            }
            else
            {
                return 0;
            }
        }

        public void RemoveTaWBonus(ITalent item)
        {
            if (item != null)
            {
                tawBonus.Remove(item);
                TaWChanged?.Invoke(this, item);
            }
        }
        public void RemoveATBonus(AbstractTalentFighting item)
        {
            if (item != null)
            {
                atBonus.Remove(item);
                ATChanged?.Invoke(this, item);
            }
        }
        public void RemovePABonus(AbstractTalentFighting item)
        {
            if (item != null)
            {
                paBonus.Remove(item);
                PAChanged?.Invoke(this, item);
            }
        }

        private string GenerateLongDescription()
        {
            var ret = Description;
            foreach(var pair in attributeValues)
            {
                var value = pair.Value;
                ret = GenerateLongDescriptionHelper(value, ret, Helper.GetShort(pair.Key));
            }
            foreach(var pair in valueValues)
            {
                var value = pair.Value;
                ret = GenerateLongDescriptionHelper(value, ret, Helper.GetShort(pair.Key));
            }
            foreach(var pair in resourceValues)
            {
                var value = pair.Value;
                ret = GenerateLongDescriptionHelper(value, ret, Helper.GetShort(pair.Key));
            }

            foreach(var pair in tawBonus)
            {
                var value = pair.Value;
                ret = GenerateLongDescriptionHelper(value, ret, pair.Key.Name);
            }
            foreach(var pair in paBonus)
            {
                var value = pair.Value;
                ret = GenerateLongDescriptionHelper(value, ret, pair.Key.Name + " PA");
            }
            foreach(var pair in atBonus)
            {
                var value = pair.Value;
                ret = GenerateLongDescriptionHelper(value, ret, pair.Key.Name + " AT");
            }

            return ret;
        }
        private string GenerateLongDescriptionHelper(int value, string returnValue, string addString)
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
