using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SQLite;

namespace Common.SQLite
{
    public class DBSQLite : SQLite
    {
        string _connString;

        public DBSQLite(string connString)
        {
            this._connString = connString;
        }

        protected override SQLiteConnection Conn
        {
            get
            {
                return new SQLiteConnection(_connString);
            }
        }
    }
}
