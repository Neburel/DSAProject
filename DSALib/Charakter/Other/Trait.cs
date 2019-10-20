
using DSALib.Interfaces;
using DSALib.Utils;
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
        #endregion
        #region Variables
        private Dictionary<IValue, int> valueValues;
        private Dictionary<IResource, int> resourceValues;
        private Dictionary<CharakterAttribut, int> attributeValues;
        #endregion
        #region Properties
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

        private string GenerateLongDescription()
        {
            var ret = Description;
            foreach(var attribut in attributeValues)
            {
                var value = attribut.Value;
                if(value != 0)
                {
                    if (string.IsNullOrEmpty(ret))
                    {
                        ret = value + " " + Helper.GetShort(attribut.Key);
                    }
                    else
                    {
                        ret = ret + ", " + value + " " + Helper.GetShort(attribut.Key);
                    }
                }
            }
            foreach (var pair in valueValues)
            {
                var value = pair.Value;
                if (value != 0)
                {
                    if (string.IsNullOrEmpty(ret))
                    {
                        ret = value + " " + Helper.GetShort(pair.Key);
                    }
                    else
                    {
                        ret = ret + ", " + value + " " + Helper.GetShort(pair.Key);
                    }
                }
            }
            foreach (var pair in resourceValues)
            {
                var value = pair.Value;
                if (value != 0)
                {
                    if (string.IsNullOrEmpty(ret))
                    {
                        ret = value + " " + Helper.GetShort(pair.Key);
                    }
                    else
                    {
                        ret = ret + ", " + value + " " + Helper.GetShort(pair.Key);
                    }
                }
            }
            return ret;
        }
    }
}
