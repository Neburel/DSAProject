﻿using System;
using System.Runtime.CompilerServices;

namespace DSALib.Exceptions
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1032:Standardausnahmekonstruktoren implementieren", Justification = "<Ausstehend>")]
    public class ValueException : AbstractDSAException
    {
        public ValueException(ErrorCode error, string message, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0) : base(error: error, message: message, callerMemberName: callerMemberName, callerFilePath: callerFilePath, callerLineNumber: callerLineNumber) { }
        public ValueException(Exception innerException, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0) : base(innerException: innerException, callerMemberName: callerMemberName, callerFilePath: callerFilePath, callerLineNumber: callerLineNumber) { }
    }
}
