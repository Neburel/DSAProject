using DSAProject.Classes.Charakter.Description;
using DSAProject.Classes.Game;
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
    public sealed partial class ChracterDescriptionView : Page
    {
        public ChracterDescriptionView()
        {
            this.InitializeComponent();
            DemoDate();

            
        }
        public void DemoDate()
        {
            var descriptors = Game.Charakter.Descriptions.Descriptions;


            FillDescriptor(XAML_Descriptor0, descriptors, (int)CreationOrder.playerName);
            FillDescriptor(XAML_Descriptor1, descriptors, (int)CreationOrder.PlayerHight);
            FillDescriptor(XAML_Descriptor2, descriptors, (int)CreationOrder.playerWight);
            FillDescriptor(XAML_Descriptor3, descriptors, (int)CreationOrder.age);
            FillDescriptor(XAML_Descriptor4, descriptors, (int)CreationOrder.gender);
            FillDescriptor(XAML_Descriptor5, descriptors, (int)CreationOrder.familyStatus);
            FillDescriptor(XAML_Descriptor6, descriptors, (int)CreationOrder.playerAdressName);
            FillDescriptor(XAML_Descriptor7, descriptors, (int)CreationOrder.eyeColor);
            FillDescriptor(XAML_Descriptor8, descriptors, (int)CreationOrder.skinColor);
            FillDescriptor(XAML_Descriptor9, descriptors, (int)CreationOrder.hairColor);
            FillDescriptor(XAML_Descriptor10, descriptors, (int)CreationOrder.profession);
            FillDescriptor(XAML_Descriptor11, descriptors, (int)CreationOrder.culture);

            FillDescriptor(XAML_Descriptor14, descriptors, (int)CreationOrder.race);
            FillDescriptor(XAML_Descriptor15, descriptors, (int)CreationOrder.faith);
        }
        private void FillDescriptor(Descriptor_ItemPage page, List<Descriptor> desciptions, int prio)
        {
            var des = desciptions.Where(x => x.Priority == prio).ToList();
            if(des.Count > 0)
            {
                page.Descriptor = des[0];
            }
        }
    }
}
