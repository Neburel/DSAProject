using System;
using System.Runtime.CompilerServices;

namespace DSALib.Exceptions
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1032:Standardausnahmekonstruktoren implementieren", Justification = "<Ausstehend>")]
    public abstract class AbstractDSAException : Exception
    {
        public ErrorCode ErrorCode { get; set; }

        public int CallerLineNumber { get; set; }
        public string CallerMemberName { get; set; }
        public string CallerFilePath { get; set; }

        public AbstractDSAException(ErrorCode error, String message, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0) : base(message: message)
        {
            ErrorCode           = error;
            CallerLineNumber    = callerLineNumber;
            CallerMemberName    = callerMemberName;
            CallerFilePath      = callerFilePath;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1062:Argumente von öffentlichen Methoden validieren", Justification = "<Ausstehend>")]
        public AbstractDSAException(Exception innerException, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0) : base(message: innerException.Message, innerException: innerException)
        {
            ErrorCode = ErrorCode.Error;

            CallerLineNumber = callerLineNumber;
            CallerMemberName = callerMemberName;
            CallerFilePath = callerFilePath;
        }
    }
}
