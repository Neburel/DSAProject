using System;
using System.Collections.Generic;

namespace DSAProject.Classes.Interfaces
{
    public interface ITalent
    {
        Guid ID { get; set; }

        int BaseDeduction { get; }
        string BE { get; set; }
        string Name { get; set; }
        string NameExtension { get; set; }
        List<ITalentDeduction> Deductions { get; }
    }
}
