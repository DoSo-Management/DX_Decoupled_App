using System;

namespace PostgreSqlConnectionProviderEx
{
    public class SelectDataFinishedEventArgs : EventArgs
    {
        public int Duration;
        public string TableName;
        public string Query;
    }
}
