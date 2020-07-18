using System;
using System.Collections.Generic;
using System.Text;

namespace DSALib.Charakter.UserGenerated
{
    public abstract class AbstractUserGenerate
    {
        public string Name { get; set; }
        public AbstractUserGenerate(string name)
        {
            Name = name;
        }
    }
}
