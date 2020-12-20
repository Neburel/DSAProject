using System.Text;

namespace DSALib2.Interfaces
{
    public interface IJSONSerializable
    {
        string JSONContent { get; set; }
        string ToJSON(Encoding encoding, out string errorString);
        string ToJSON(out string errorString);
    }
}
