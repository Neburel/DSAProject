using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSAProject.Classes.Interfaces
{
    public interface ITalent
    {
        Guid ID { get; }

        int BaseDeduction { get; }
        string BE { get; set; }
        string Name { get; set; }
        string NameExtension { get; set; }
        List<ITalentDeduction> Deductions { get; }
    }
}
