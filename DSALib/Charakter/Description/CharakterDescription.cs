using System.Collections.Generic;
using System.Linq;

namespace DSAProject.Classes.Charakter.Description
{
    public class CharakterDescription
    {
        private List<Descriptor> descriptions           = new List<Descriptor>();
        public List<Descriptor> Descriptions
        {
            get => new List<Descriptor>(descriptions.OrderBy(x => x.Priority).ThenBy(x => x.DescriptionTitle));
        }
        /// <summary>
        /// Fügt eine Description der Liste hinzu. Die Priorität gibt an an welcher Position der Liste das Element hinzugefügt wird (1 Vorne). 
        /// Haben zwei Descriptions die gleiche Priorität wird die Reihenfolge Alphabetisch festgelegt
        /// </summary>
        /// <param name="description"></param>
        /// <param name="priority"></param>
        /// <returns></returns>
        public void AddDescripton(Descriptor descriptor)
        {
            descriptions.Add(descriptor);
        }
    }
}
