using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using CMS.Model;
using System.Configuration;
using System.Data;

namespace CMS.DAL
{
    public partial class TemplateDetailDAL
    {
        public int DeleteTemplateDetailByTemplateId(int templateId)
        {
            string sql = string.Format("delete from TemplateDetail where TemplateId={0}", templateId);
            int result = SqlHelper.ExecuteNonQuery(SqlConnectFactory.CMS, CommandType.Text, sql);
            return result;
        }

        public bool ShieldTemplateDetailCheck(int templateId)
        {
            bool result = false;
            string sql = string.Format("select TemplateId from TemplateDetail where TemplateId<>({0})", templateId);
            object obj = SqlHelper.ExecuteScalar(SqlConnectFactory.CMS, CommandType.Text, sql);
            if (obj != null)
            {
                result = true;
            }
            return result;
        }
        public DataTable GetTemplateDetailChecked(int templateId)
        {
            DataTable result = null;
            string sql = string.Format("select * from TemplateDetail where TemplateId<>({0}) and RelationType=2", templateId);
            result = SqlHelper.ExecuteDataset(SqlConnectFactory.CMS, CommandType.Text, sql).Tables[0];
            return result;
        }
    }
}
