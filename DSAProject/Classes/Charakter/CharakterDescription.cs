using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSAProject.Classes.Charakter
{
    public class CharakterDescription
    {
        public int Alter { get; set; }
        public int Größe { get; set; }
        public int Gewicht { get; set; }
        public string Name { get; set; }
        public string Augenfarbe { get; set; }
        public string Anrede { get; set; }
        public string Famlienstand { get; set; }
        public List<string> Rassen { get; set; }
        public List<string> Kulturen { get; set; }
        public List<string> Professionen { get; set; }
        public List<string> Gottheiten { get; set; }
        public List<string> Modifikationen { get; set; }
        public List<string> Göttergeschenke { get; set; }
        public DateTime Geburtstdatum { get; set; } 
    }
}
