using DSALib.Charakter.Other;
using DSAProject.util;


namespace DSAProject.Layout.ViewModels
{
    public class SpellViewModel : AbstractPropertyChanged
    {
        public string Name { get => Get<string>(); set => Set(value); }


        public SpellViewModel(Spell spell)
        {
            Name = spell.Name;
        }

    }
}
