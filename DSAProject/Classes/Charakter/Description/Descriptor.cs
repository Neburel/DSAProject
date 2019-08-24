using DSAProject.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSAProject.Classes.Charakter.Description
{
    public class Descriptor : AbstractPropertyChanged
    {
        private string descriptionTitle = string.Empty;
        private string descriptionText = string.Empty; 


        public int Priority { get; set; } = 0;
        public string DescriptionTitle
        {
            get => descriptionTitle;
            set
            {
                descriptionTitle = value;
                OnPropertyChanged(nameof(DescriptionTitle));
            }
        }
        public string DescriptionText
        {
            get => descriptionText;
            set
            {
                descriptionText = value;
                OnPropertyChanged(nameof(DescriptionText));
            }
        }
    }
}
