using System;
using System.Collections.Generic;
using System.Text;

namespace DSALib.Utils
{
    public static class DSALibExtensions
    {
        public static Guid GenerateNextGuid(this Guid currentGuid, List<Guid> guidList)
        {
            if (guidList == null) guidList = new List<Guid>();

            var newGuid = Guid.NewGuid();

            while (guidList.Contains(newGuid))
            {
                newGuid = Guid.NewGuid();
            }
            return newGuid;
        }
    }
}
