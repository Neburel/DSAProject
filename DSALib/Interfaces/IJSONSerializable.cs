using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSALib.Classes.Interfaces
{
    public interface IJSONSerializable
    {
        string JSONContent { get; set; }
        string ToJSON(Encoding encoding, out string error);
        string ToJSON(out string error);
    }
}
