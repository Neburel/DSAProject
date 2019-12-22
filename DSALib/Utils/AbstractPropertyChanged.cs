using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DSAProject.util
{
    public abstract class AbstractPropertyChanged : INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
