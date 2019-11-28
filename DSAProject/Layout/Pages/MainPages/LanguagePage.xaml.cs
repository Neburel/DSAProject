using DSALib.Charakter.Talente.TalentLanguage;
using DSAProject.Classes.Charakter.Talente.TalentLanguage;
using DSAProject.Classes.Game;
using DSAProject.Layout.Pages.ItemPages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Die Elementvorlage "Leere Seite" wird unter https://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace DSAProject.Layout.Pages.MainPages
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class LanguagePage : Page
    {
        private int counter = 0;

        public LanguagePage()
        {
            this.InitializeComponent();

            var languageFamily = Game.LanguageFamilies[0];

            foreach(var family in Game.LanguageFamilies)
            {
                XAML_Grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(0, GridUnitType.Auto) });
                var title = new TextBlock { Text = family.Name };
                XAML_Grid.Children.Add(title);
                Grid.SetRow(title, counter);
                counter++;

                for (int i=0; i<family.GetHighestPosition(); i++)
                {
                    Language_ItemPage x = new Language_ItemPage();
                    if(family.Languages.TryGetValue(i, out TalentLanguage language))
                    {
                        x.SetLanguageTalent(language);
                    }
                    if (family.Writings.TryGetValue(i, out TalentWriting writing))
                    {
                        x.SetWritingTalent(writing);
                    }
                    AddNewRow(x);
                }
            }
        }
        private void AddNewRow(Language_ItemPage item)
        {
            XAML_Grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(0, GridUnitType.Auto) });

            XAML_Grid.Children.Add(item);
            Grid.SetRow(item, counter);
            counter++;
        }
    }
}
