using System;
using System.Collections.Generic;
using System.Text;

namespace DSALib.Utils
{
    public static class Extensions
    {
        public static Guid GenerateNextGuid(this Guid guid, List<Guid> guids)
        {
            var newGuid = Guid.NewGuid();

            while (guids.Contains(newGuid))
            {
                guid = Guid.NewGuid();
            }
            return newGuid;
        }

    }
}
