using System;
using System.Collections.Generic;
using System.Text;

namespace DSALib.Charakter.UserGenerated
{
    public abstract class AbstractUserGenerateAttribute : AbstractUserGenerate
    {
        public AbstractUserGenerateAttribute(string name, List<CharakterAttribut> attributeList) : base(name)
        {

        }
    }
}
