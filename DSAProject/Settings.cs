using System;
using Windows.Storage;

namespace DSAProject
{
    public class Settings
    {
        private ApplicationDataContainer localSettings;

        public bool OrderTalentAlphabetic
        {
            get
            {
                try
                {
                    return (bool)localSettings.Values[nameof(OrderTalentAlphabetic)];
                }
                catch (Exception)
                {
                    return false;
                }
            }
            set
            {
                localSettings.Values[nameof(OrderTalentAlphabetic)] = value;
            }
        }
        public bool AutoDeduction
        {
            get
            {
                try
                {
                    //return (bool)localSettings.Values[nameof(AutoDeduction)];
                    return false;
                }
                catch(Exception)
                {
                    return false;
                }
            }
            set
            {
                localSettings.Values[nameof(AutoDeduction)] = value;
                DSALib.Utils.Settings.AutoDeduction = value;
            }
        }
        public Settings()
        {
            localSettings           = ApplicationData.Current.LocalSettings;
            AutoDeduction           = AutoDeduction;
        }
    }
}
