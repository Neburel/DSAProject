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
        public int Priority { get; set; } = 0;
        public string DescriptionTitle { get; set; } = string.Empty;
        public string DescriptionText { get; set; } = string.Empty;
    }
}
