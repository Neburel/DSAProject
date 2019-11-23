using System;
using System.Runtime.CompilerServices;

namespace DSALib.Exceptions
{
    public abstract class AbstractDSAException : Exception
    {
        public ErrorCode ErrorCode { get; set; }

        public int CallerLineNumber { get; set; }
        public string CallerMemberName { get; set; }
        public string CallerFilePath { get; set; }

        public AbstractDSAException(ErrorCode error, String message, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0) : base(message: message)
        {
            CallerLineNumber    = callerLineNumber;
            CallerMemberName    = callerMemberName;
            CallerFilePath      = callerFilePath;
        }
        public AbstractDSAException(Exception innerException, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0) : base(message: innerException.Message, innerException: innerException)
        {
            ErrorCode = ErrorCode.Error;

            CallerLineNumber = callerLineNumber;
            CallerMemberName = callerMemberName;
            CallerFilePath = callerFilePath;
        }
    }
}
