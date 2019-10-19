using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SQLite;

namespace Common.SQLite
{
    public class MemorySQLite : SQLite
    {
        private SQLiteConnection _conn;

        protected override System.Data.SQLite.SQLiteConnection Conn
        {
            get
            {
                return this._conn;
            }
        }

        public MemorySQLite(SQLiteConnection conn)
        {
            this._conn = conn;
        }

    }
}
