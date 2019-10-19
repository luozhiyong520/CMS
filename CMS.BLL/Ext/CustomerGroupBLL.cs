using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMS.DAL;
using CMS.Model;
using System.Data;
using Common;
using CMS.HtmlService.Contract;
using System.ServiceModel;

namespace CMS.BLL
{
    public partial class CustomerGroupBLL
    {
        /// <summary>
        /// 删除已存在的用户组用户信息
        /// </summary>
        /// <param name="GroupName">用户组名</param>
        /// <returns></returns>
        public int DeleteGroup(string GroupName)
        {
            return dal.DeleteGroup(GroupName);
        }
        public List<CustomerGroup> CustomerGroupList()
        {
            return dal.CustomerGroupList();
        }

        /// <summary>
        /// 例: select (ago) from table (after)
        /// </summary>
        public List<CustomerGroup> GetAll(string ago, string after)
        {
            return dal.GetAll(ago, after);
        }

         /// <summary>
        /// 一次性把Table中的数据插入到数据库
        /// </summary>
        /// <param name="dt"></param>
        public void BulkToDB(DataTable dt)
        {
            dal.BulkToDB(dt);
        }

         

        /// <summary>
        /// 获取群组列表
        /// </summary>
        public string GetGroupList()
        {

            string returnstring = RequestHelper.WebRequest("http://app.upchina.com/upos_service/getpopupgroups", "post", "", "utf-8",true);
            return returnstring;
        }
        /// <summary>
        /// 获取用户组列表
        /// </summary>
        public string GetUserDataList(int GroupId)
        {
            string returnstring = RequestHelper.WebRequest("http://app.upchina.com/upos_service/getpopupgroupusers?GroupId=" + GroupId, "post", "", "utf-8", true);
            return returnstring;
        }
    }
}
