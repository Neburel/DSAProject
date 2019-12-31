using DSALib;

namespace DSALib.Utils
{
    public class DSAError
    {
        public ErrorCode ErrorCode { get; set; } = ErrorCode.Error;
        public string Message { get; set; } = string.Empty;
    }
}
