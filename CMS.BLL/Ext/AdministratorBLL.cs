using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMS.DAL;
using CMS.Model;
using Common;

namespace CMS.BLL
{
    public partial class AdministratorBLL
    {
        /// <summary>
        /// 获取用户组与用户表关联的列表
        /// </summary>
        /// <returns></returns>
        public List<Administrator> GetAdministratorList()
        {
            return dal.GetAdministratorList();
        }

        /// <summary>
        /// 获取有条件的用户组与用户表关联的一条实体数据
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public Administrator GetAdministratorList(string colName, object value)
        {
            return null;
            //return dal.GetAdministratorList(colName,value);
        }

       
    }
}
