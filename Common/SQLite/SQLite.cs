using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SQLite;

namespace Common.SQLite
{
    public abstract class SQLite : ISQLite
    {
        protected abstract SQLiteConnection Conn { get; }

        protected virtual void PrepareCommand(SQLiteCommand cmd, SQLiteConnection conn, string cmdText, params object[] p)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Parameters.Clear();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;

            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 30;

            if (p != null)
            {
                cmd.Parameters.AddRange(p);
            }
        }

        public virtual DataSet ExecuteDataset(string cmdText, params object[] p)
        {
            using (SQLiteCommand command = Conn.CreateCommand())
            {
                DataSet ds = new DataSet();
                PrepareCommand(command, Conn, cmdText, p);
                SQLiteDataAdapter da = new SQLiteDataAdapter(command);
                da.Fill(ds);
                return ds;
            }
        }



        public virtual DataRow ExecuteDataRow(string cmdText, params object[] p)
        {
            DataSet ds = ExecuteDataset(cmdText, p);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                return ds.Tables[0].Rows[0];
            return null;
        }

        /// <summary>
        /// 返回受影响的行数
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public virtual int ExecuteNonQuery(string cmdText, params object[] p)
        {
            SQLiteCommand command = Conn.CreateCommand();
            PrepareCommand(command, Conn, cmdText, p);
            return command.ExecuteNonQuery();
        }

        public virtual SQLiteDataReader ExecuteReader(string cmdText, params object[] p)
        {
            SQLiteCommand command = Conn.CreateCommand();
            PrepareCommand(command, Conn, cmdText, p);
            SQLiteDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            return reader;


        }

        public virtual object ExecuteScalar(string cmdText, params object[] p)
        {
            SQLiteCommand cmd = Conn.CreateCommand();
            PrepareCommand(cmd, Conn, cmdText, p);
            return cmd.ExecuteScalar();
        }

        public virtual DataSet ExecutePager(ref int recordCount, int pageIndex, int pageSize, string cmdText, string countText, params object[] p)
        {
            if (recordCount < 0)
                recordCount = int.Parse(ExecuteScalar(countText, p).ToString());

            DataSet ds = new DataSet();
            SQLiteCommand command = Conn.CreateCommand();
            PrepareCommand(command, Conn, cmdText, p);
            SQLiteDataAdapter da = new SQLiteDataAdapter(command);
            da.Fill(ds, (pageIndex - 1) * pageSize, pageSize, "result");
            return ds;
        }
    }
}
