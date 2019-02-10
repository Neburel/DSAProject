using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSAProject.util
{
    public enum ErrorCode
    {
        Error = 1,                          //Nicht näher Definierter Error
        NullValue = 2,                      //Ein Übergebener Wert erhält einen nicht erwarteten Null wert
        InvalidValue = 3,
        NetworkConnectionError = 4,         //Netzwerkfehler
        NetworkAuthentificationError = 5,   //Authentifizierungsfehler
        WebExceptionError = 6               //Fehler auf einem Server
    };

    public enum LogLevel
    {
        ErrorLog,
        ActionLog,
        DebugInfo
    }
}
