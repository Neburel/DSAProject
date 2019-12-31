using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace DSALib.Exceptions
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1032:Standardausnahmekonstruktoren implementieren", Justification = "<Ausstehend>")]
    public class AttributException : AbstractDSAException
    {
        public AttributException(ErrorCode error, string message, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0) : base(error: error, message: message, callerMemberName: callerMemberName, callerFilePath: callerFilePath, callerLineNumber: callerLineNumber) { }
        public AttributException(Exception innerException, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0) : base(innerException: innerException, callerMemberName: callerMemberName, callerFilePath: callerFilePath, callerLineNumber: callerLineNumber) { }

    }
}
