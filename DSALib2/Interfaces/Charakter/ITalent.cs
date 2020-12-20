using System;
using System.Collections.Generic;

namespace DSALib2.Interfaces.Charakter
{
    public interface ITalent
    {
        Guid ID { get; set; }
        int OrginalPosition { get; set; }
        int BaseDeduction { get; }
        string BE { get; set; }
        string Name { get; set; }
        string NameExtension { get; set; }
        List<ITalentDeduction> Deductions { get; }
    }
}
