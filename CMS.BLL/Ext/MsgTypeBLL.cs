using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMS.DAL;
using System.Data;
using Common;
using CMS.Model;
using System.Text.RegularExpressions;

namespace CMS.BLL
{
   public partial class MsgTypeBLL
    {
        /// <summary>
        /// 根据用户名的权限获取留言类型数据
        /// </summary>
        /// <returns></returns>
       public List<MsgType> GetMsgTypeByAdminId(int authorityParentId)
        {
            int adminId = UserCookies.AdminId;
            return dal.GetMsgTypeByAdminId(adminId, authorityParentId);
        }
    }
}
