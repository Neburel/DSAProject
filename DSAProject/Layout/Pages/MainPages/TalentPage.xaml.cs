﻿using DSAProject.Classes.Charakter.Talente;
using DSAProject.Classes.Game;
using DSAProject.Classes.Interfaces;
using DSAProject.Layout.Pages.ItemPages;
using DSAProject.Layout.Views;
using DSAProject.util.ErrrorManagment;
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

namespace DSAProject.Layout.Pages
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class TalentPage : Page
    {
        private int counter = 0;
        private List<Talent_ItemPage> items = new List<Talent_ItemPage>();

        public TalentPage()
        {
            this.InitializeComponent();
        }
        public void SetTalents(List<ITalent> talent)
        {
            if (talent != null && talent.Count > 0)
            {
                var collection = talent.OrderBy(x => x.ToString()).ToList();

                if (typeof(AbstractTalentFighting).IsAssignableFrom(talent[0].GetType()))
                {
                    XAML_BorderProbeText.Visibility = Visibility.Collapsed;
                    XAML_BorderATPA.Visibility = Visibility.Visible;
                }
                if (typeof(AbstractTalentGeneral).IsAssignableFrom(talent[0].GetType()))
                {
                    XAML_BorderProbeText.Visibility = Visibility.Visible;
                    XAML_BorderATPA.Visibility = Visibility.Collapsed;
                }

                foreach (var item in collection)
                {
                    AddNewRow(new Talent_ItemPage(item));
                }
            }
        }
        private void AddNewRow(Talent_ItemPage item)
        {
            XAML_Grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(0, GridUnitType.Auto)});

            XAML_Grid.Children.Add(item);
            Grid.SetRow(item, counter);
            counter++;

            items.Add(item);
        }

        private void XAML_SwitchTAWEdit_Toggled(object sender, RoutedEventArgs e)
        {
        }

        private void AutoSuggestBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {

        }

        private void AutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            foreach(var item in items)
            {
                if (item.Talent.Name.StartsWith(sender.Text))
                {
                    item.Visibility = Visibility.Visible;
                } 
                else
                {
                    item.Visibility = Visibility.Collapsed;
                }
            }
        }
    }
}