using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using CMS.Model;
using Common;

namespace CMS.DAL
{
   public partial class AuthorityDotDAL
    {
            
       public List<AuthorityDot> GetAuthorityDotAdmin(int adminId,int parentId)
       {
           string sql = @"SELECT dbo.AuthorityDot.Id, dbo.AuthorityDot.ParentId, dbo.AuthorityDot.Text, dbo.AuthorityDot.Type, dbo.AuthorityDot.RelevanceId, dbo.AuthorityDot.Status, 
                      dbo.AuthorityDotAdmin.AuthorityDotId, dbo.AuthorityDotAdmin.AdminId
            FROM         dbo.AuthorityDot INNER JOIN
                      dbo.AuthorityDotAdmin ON dbo.AuthorityDot.Id = dbo.AuthorityDotAdmin.AuthorityDotId
                WHERE     (dbo.AuthorityDot.ParentId = @parentId and dbo.AuthorityDot.Status=1 and AuthorityDotAdmin.AdminId=@AdminId)";
           List<AuthorityDot> list;
           SqlParameter[] parms = new SqlParameter[] { new SqlParameter("@AdminId", adminId), new SqlParameter("@parentId", parentId) };
           using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlConnectFactory.CMS, CommandType.Text, sql,parms))
           {
               list = EntityHelper.GetEntityListByDataReader<AuthorityDot>(dr,null);
           }
           return list;
       }

       public List<AuthorityDot> GetAuthoritydot(int adminId, int ParentId)
       {
           string sql = "select au.Text,au.RelevanceId from dbo.AuthorityDot au inner join dbo.AuthorityDotAdmin ad on au.Id=ad.AuthorityDotId where au.ParentId=@ParentId and au.Status=1 and ad.AdminId=@AdminId";
           List<AuthorityDot> list;
           SqlParameter[] parms = new SqlParameter[] { new SqlParameter("@AdminId", adminId), new SqlParameter("@ParentId", ParentId) };
           using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlConnectFactory.CMS, CommandType.Text, sql, parms))
           {
               list = EntityHelper.GetEntityListByDataReader<AuthorityDot>(dr, null);
           }
           return list;
       }

       public int DelAuthoritydot(int adminId)
       {
           string sql = "delete from AuthorityDotAdmin where AdminId=@adminId";
            SqlParameter[] parms = new SqlParameter[] { new SqlParameter("@adminId", adminId) };
            int result = SqlHelper.ExecuteNonQuery(SqlConnectFactory.CMS, CommandType.Text, sql, parms);
            return result;
       }

       public int DelAuthoritydotByParentId(int Id)
       {
           string sql = "delete from AuthorityDot where ParentId=@Id";
           SqlParameter[] parms = new SqlParameter[] { new SqlParameter("@Id", Id) };
           int result = SqlHelper.ExecuteNonQuery(SqlConnectFactory.CMS, CommandType.Text, sql, parms);
           return result;
       }
    }
}
