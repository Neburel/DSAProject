using System;
using System.Reflection;

namespace DSALib2.Utils
{
    public static class DSAUtil
    {
        public static Type GetType(string typeString)
        {
            Assembly asm = typeof(DSAUtil).Assembly;
            Type type = asm.GetType(typeString);
            return type;
        }
    }
}
