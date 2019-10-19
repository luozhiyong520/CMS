using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SQLite;

namespace Common.SQLite
{
    public interface ISQLite
    {
        DataSet ExecuteDataset(string cmdText, params object[] p);

        DataRow ExecuteDataRow(string cmdText, params object[] p);

        int ExecuteNonQuery(string cmdText, params object[] p);

        SQLiteDataReader ExecuteReader(string cmdText, params object[] p);

        object ExecuteScalar(string cmdText, params object[] p);

        DataSet ExecutePager(ref int recordCount, int pageIndex, int pageSize, string cmdText, string countText, params object[] p);
    }
}
