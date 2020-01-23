using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace DSAProject.util
{
    public abstract class AbstractPropertyChanged : INotifyPropertyChanged
    {
        #region Variables
        private Dictionary<string, object> propertieDictionary = new Dictionary<string, object>();
        #endregion
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        /// <summary>
        /// Gets the value of a property
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        protected T Get<T>([CallerMemberName] string name = null)
        {
            Debug.Assert(name != null, "name != null");
            if (propertieDictionary.TryGetValue(name, out object value))
                return value == null ? default : (T)value;
            return default;
        }

        /// <summary>
        /// Sets the value of a property
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="name"></param>
        /// <remarks>Use this overload when implicitly naming the property</remarks>
        protected void Set<T>(T value, [CallerMemberName] string name = null)
        {
            Debug.Assert(name != null, "name != null");
            if (Equals(value, Get<T>(name)))
                return;
            propertieDictionary[name] = value;
            OnPropertyChanged(name);
        }
    }
}
