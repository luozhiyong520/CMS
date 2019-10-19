using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using Common;
using System.Data;
using CMS.Model.Oracle;

namespace CMS.DAL.Oracle
{
    public class DsYmsUserDAL
    {
        /// <summary>
        /// 删除云秒杀记录
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public int DeleteDsYmsUser(string UserName)
        {
            try
            {
                string sql = "DELETE FROM selfstock.DS_YMS_USER WHERE USERNAME=:USERNAME";
                var commandParameters = new[]
                                            {
                                                new OracleParameter(":USERNAME", OracleDbType.Varchar2)
                                            };
                commandParameters[0].Value = UserName;
                return OracleDataAccess.ExecuteNonQuery(OracleDataAccess.OracleConnectionUpdbString, CommandType.Text, sql, commandParameters);
            }
            catch (OracleException ce)
            {
                Loger.Error(ce);
            }
            return 0;
        }

        /// <summary>
        /// 查询云秒杀记录
        /// </summary>
        /// <param name="package"></param>
        /// <returns></returns>
        public IList<DsYmsUserModel> GetYmsUserList(string package)
        {
            try
            {
                string sql;
                if (string.IsNullOrEmpty(package))
                {
                    sql = "select y.username,y.showname,y.lastresults,y.sort,y.package,u.capital,u.pos_ratio FROM selfstock.DS_YMS_USER y join selfstock.DS_QR_USERINFO u on y.username=u.user_name WHERE u.user_type='1'";
                }
                else
                {
                    sql = "select y.username,y.showname,y.lastresults,y.sort,y.package,u.capital,u.pos_ratio FROM selfstock.DS_YMS_USER y join selfstock.DS_QR_USERINFO u on y.username=u.user_name WHERE y.package='" + package.ToLower() + "' and u.user_type='1'";
                }
                OracleDataReader reader = OracleDataAccess.ExecuteReader(OracleDataAccess.OracleConnectionUpdbString, CommandType.Text, sql);

                IList<DsYmsUserModel> list = new List<DsYmsUserModel>();
                while (reader.Read())
                {
                    var model = new DsYmsUserModel();
                    model.UserName = reader["username"].ToString();
                    model.ShowName = reader["showname"].ToString();
                    model.LastResults = (decimal)reader["lastresults"];
                    model.Sort = int.Parse(reader["sort"].ToString());
                    model.Package = reader["package"].ToString();
                    model.Capital = (decimal)reader["capital"];
                    model.PosRatio = (decimal)reader["pos_ratio"];
                    list.Add(model);
                }
                reader.Close();
                return list;
            }
            catch (OracleException ce)
            {
                Loger.Error(ce);
                return null;
            }
        }

        /// <summary>
        /// 更新云秒杀记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateDsYmsUser(DsYmsUserModel model)
        {
            string sql = "update selfstock.DS_YMS_USER set showname=:showname,lastresults=:lastresults,sort=:sort where username=:username";
            var commandParameters = new[]
                                        {
                                            new OracleParameter(":showname", OracleDbType.Varchar2),
                                            new OracleParameter(":lastresults", OracleDbType.Decimal),
                                            new OracleParameter(":sort", OracleDbType.Int32),
                                            new OracleParameter(":username", OracleDbType.Varchar2)                                            
                                        };
            commandParameters[0].Value = model.ShowName;
            commandParameters[1].Value = model.LastResults;
            commandParameters[2].Value = model.Sort;
            commandParameters[3].Value = model.UserName;
            return OracleDataAccess.ExecuteNonQuery(OracleDataAccess.OracleConnectionUpdbString, CommandType.Text, sql, commandParameters);
        }

        /// <summary>
        /// 插入云秒杀记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int InsertDsYmsUser(DsYmsUserModel model)
        {
            string sql = "INSERT INTO selfstock.DS_YMS_USER (username,showname,lastresults,sort,package)";
            sql += " VALUES(:username,:showname,:lastresults,:sort,:package)";
            var commandParameters = new[]
                                        {
                                            new OracleParameter(":username", OracleDbType.Varchar2),
                                            new OracleParameter(":showname", OracleDbType.Varchar2),
                                            new OracleParameter(":lastresults", OracleDbType.Decimal),
                                            new OracleParameter(":sort", OracleDbType.Int32),
                                            new OracleParameter(":package", OracleDbType.Varchar2)
                                        };
            commandParameters[0].Value = model.UserName;
            commandParameters[1].Value = model.ShowName;
            commandParameters[2].Value = model.LastResults;
            commandParameters[3].Value = model.Sort;
            commandParameters[4].Value = model.Package.ToLower();
            return OracleDataAccess.ExecuteNonQuery(OracleDataAccess.OracleConnectionUpdbString, CommandType.Text, sql, commandParameters);
        }
    }
}
