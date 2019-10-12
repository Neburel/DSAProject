
using DSALib.Interfaces;
using DSAProject.Classes.Interfaces;
using System;
using System.Collections.Generic;

namespace DSALib.Charakter.Trait
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

    }
}
