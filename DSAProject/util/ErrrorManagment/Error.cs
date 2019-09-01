using DSALib;

namespace DSAProject.util.ErrrorManagment
{
    public class Error
    {
        public ErrorCode ErrorCode { get; set; } = ErrorCode.Error;
        public string Message { get; set; } = string.Empty;
    }
}
