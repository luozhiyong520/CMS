using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMS.DAL.Oracle;
using CMS.Model.Oracle;

namespace CMS.BLL.Oracle
{
    public class DsYmsUserBLL
    {
        public static CMS.DAL.Oracle.DsYmsUserDAL contestDAL = new DsYmsUserDAL();

        /// <summary>
        /// 删除云秒杀记录
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public int DeleteDsYmsUser(string UserName)
        {
            return contestDAL.DeleteDsYmsUser(UserName);
        }

        /// <summary>
        /// 查询云秒杀记录
        /// </summary>
        /// <param name="package"></param>
        /// <returns></returns>
        public IList<DsYmsUserModel> GetYmsUserList(string package)
        {
            return contestDAL.GetYmsUserList(package);
        }

        /// <summary>
        /// 更新云秒杀记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateDsYmsUser(DsYmsUserModel model)
        {
            return contestDAL.UpdateDsYmsUser(model);
        }

        /// <summary>
        /// 插入云秒杀记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int InsertDsYmsUser(DsYmsUserModel model)
        {
            return contestDAL.InsertDsYmsUser(model);
        }
    }
}
