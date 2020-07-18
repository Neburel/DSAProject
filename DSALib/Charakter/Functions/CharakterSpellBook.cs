using DSALib.Charakter.Other;
using System.Collections.Generic;

namespace DSALib.Charakter.Functions
{
    public class CharakterSpellBook
    {
        private List<Spell> spellList { get; set; } = new List<Spell>();

        public void AddSpell(Spell spell)
        {
            if (!spellList.Contains(spell))
            {
                spellList.Add(spell);
            }
        }
        public void RemoveSpell(Spell spell)
        {
            if (spellList.Contains(spell))
            {
                spellList.Remove(spell);
            }
        }
        public List<Spell> GetSpellList()
        {
            return new List<Spell>(spellList);
        }
    }
}
