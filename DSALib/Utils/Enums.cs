namespace DSALib
{
    public enum GameType
    {
        DSA,
        PNP
    }
    public enum CharakterAttribut
    {
        Mut = 1,
        Klugheit = 2,
        Intuition = 3,
        Charisma = 4,
        Fingerfertigkeit = 5,
        Gewandheit = 6,
        Konstitution = 7,
        Körperkraft = 8,
        Sozialstatus = 9
    };
    public enum ErrorCode
    {
        Error = 1,                          //Nicht näher Definierter Error
        NullValue = 2,                      //Ein Übergebener Wert erhält einen nicht erwarteten Null wert
        InvalidValue = 3,
        NetworkConnectionError = 4,         //Netzwerkfehler
        NetworkAuthentificationError = 5,   //Authentifizierungsfehler
        WebExceptionError = 6,              //Fehler auf einem Server
        SerializationError = 7
    };
    public enum LogLevel
    {
        ErrorLog,
        ActionLog,
        DebugInfo
    }
    public enum TraitType
    {
        Keiner = 1,
        Vorteil = 2,
        Nachteil = 3,  
        Event = 4,
        Belohnung = 5,
        Quest = 6,
        Geburstag = 7,
        Title = 8
    }
    
}
