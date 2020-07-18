using System;
using System.Runtime.CompilerServices;

namespace DSALib.Exceptions
{
    public class DSABaseException : AbstractDSAException
    {
        public DSABaseException(ErrorCode error, string message, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0) : base(error: error, message: message, callerMemberName: callerMemberName, callerFilePath: callerFilePath, callerLineNumber: callerLineNumber) { }
        public DSABaseException(Exception innerException, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0) : base(innerException: innerException, callerMemberName: callerMemberName, callerFilePath: callerFilePath, callerLineNumber: callerLineNumber) { }
    }
}
