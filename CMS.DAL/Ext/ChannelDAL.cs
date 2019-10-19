using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using CMS.Model;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace CMS.DAL
{
    public partial class ChannelDAL
    {
        public string InsertChannel(string parentID,string channeName, string url, string typeID, string channelEnName, string createUser, bool status)
        {
            SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(SqlConnectFactory.CMS, "up_Channel_InsertChannel");
            commandParameters[0].Value = parentID;
            commandParameters[1].Value = channeName;
            commandParameters[2].Value = url;
            commandParameters[3].Value = typeID;
            commandParameters[4].Value = channelEnName;
            commandParameters[5].Value = createUser;
            commandParameters[6].Value = DateTime.Now;
            commandParameters[7].Value =status;
            int result = SqlHelper.ExecuteNonQuery(SqlConnectFactory.CMS, CommandType.StoredProcedure, "up_Channel_InsertChannel", commandParameters);

            return commandParameters[8].Value.ToString();
        }

        public string UpdateChannel(string channelID, string parentID, string channeName, string url, string typeID, string channelEnName, string updateUser, bool status)
        {
            SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(SqlConnectFactory.CMS, "up_Channel_UpdateChannel");

            commandParameters[0].Value = channelID;
            commandParameters[1].Value = parentID;
            commandParameters[2].Value = channeName;
            commandParameters[3].Value = url;
            commandParameters[4].Value = typeID;
            commandParameters[5].Value = channelEnName;
            commandParameters[6].Value = updateUser;
            commandParameters[7].Value = status;

            int result = SqlHelper.ExecuteNonQuery(SqlConnectFactory.CMS, CommandType.StoredProcedure, "up_Channel_UpdateChannel", commandParameters);

            return commandParameters[8].Value.ToString();
        }
    }
}
