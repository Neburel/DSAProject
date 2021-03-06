﻿
using DSALib.Interfaces;
using DSALib.Utils;
using DSAProject.Classes.Charakter.Talente;
using DSAProject.Classes.Interfaces;
using System;
using System.Collections.Generic;

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
        public event EventHandler<AbstractTalentFighting> BLChanged;
        public event EventHandler<int> APEarnedChanged;
        public event EventHandler<int> APInvestChanged;
        #endregion
        #region Variables
        private int apEarned;
        private int apInvest;
        private Dictionary<IValue, int> valueValues;
        private Dictionary<IResource, int> resourceValues;
        private Dictionary<CharakterAttribut, int> attributeValues;
        private Dictionary<ITalent, int> tawBonus;
        private Dictionary<AbstractTalentFighting, int> atBonus;
        private Dictionary<AbstractTalentFighting, int> paBonus;
        private Dictionary<AbstractTalentFighting, int> blBonus;
        #endregion
        #region Properties
        public int APEarned 
        {
            get => apEarned;
            set
            {
                apEarned = value;
                APEarnedChanged?.Invoke(this, apEarned);
            }
        }
        public int APInvest 
        {
            get => apInvest;
            set
            {
                apInvest = value;
                APInvestChanged?.Invoke(this, apInvest);
            }
        }
        public string GP { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1721:Eigenschaftennamen dürfen nicht mit Get-Methoden übereinstimmen", Justification = "<Ausstehend>")]
        public string Value { get; set; }
        public string Title { get; set; } 
        public string Description { get; set; }
        public string LongDescription { get => GenerateLongDescription(); }
        public TraitType TraitType { get; set; }
        #endregion
        public Trait()
        {
            valueValues = new Dictionary<IValue, int>();
            resourceValues = new Dictionary<IResource, int>();
            attributeValues = new Dictionary<CharakterAttribut, int>();

            tawBonus = new Dictionary<ITalent, int>();
            atBonus = new Dictionary<AbstractTalentFighting, int>();
            paBonus = new Dictionary<AbstractTalentFighting, int>();
            blBonus = new Dictionary<AbstractTalentFighting, int>();

            Title = "";
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
        public void SetBLBonus(AbstractTalentFighting item, int value)
        {
            if (item != null)
            {
                blBonus.Remove(item);
                blBonus.Add(item, value);
                BLChanged?.Invoke(this, item);
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
        public Dictionary<AbstractTalentFighting, int> GetBLBonus()
        {
            return new Dictionary<AbstractTalentFighting, int>(blBonus);
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
        public int GetBLBonus(AbstractTalentFighting item)
        {
            if (blBonus.TryGetValue(item, out int value))
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
        public void RemoveBLBonus(AbstractTalentFighting item)
        {
            if (item != null)
            {
                blBonus.Remove(item);
                BLChanged?.Invoke(this, item);
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
                ret = GenerateLongDescriptionHelper(value, ret, pair.Key.ShortName);
            }
            foreach(var pair in resourceValues)
            {
                var value = pair.Value;
                ret = GenerateLongDescriptionHelper(value, ret, pair.Key.ShortName);
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
            foreach(var pair in blBonus)
            {
                var value = pair.Value;
                ret = GenerateLongDescriptionHelper(value, ret, pair.Key.Name + " BL");
            }

            var adventurePointString = "Abenteuerpunkte";
            if(APEarned != 0)
            {
                ret = ret + " " + APEarned + adventurePointString;
            }
            if (APInvest != 0)
            {
                ret = ret + " " + " -" + APInvest + adventurePointString;
            }

            return ret;
        }
        private static string GenerateLongDescriptionHelper(int value, string returnValue, string addString)
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
