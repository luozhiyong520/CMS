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
   public partial class SSOResultBLL
    {
       /// <summary>
       /// 获取SSO数据推送列表
       /// </summary>
       /// <param name="page"></param>
       /// <param name="rows"></param>
       /// <returns></returns>
       public PagedResult<SSOResult> GetSSOResultList(int? page, int rows)
       {
           PagedResult<SSOResult> st = dal.GetSSOResultList(page, rows);
           return st;
       }
    }
}
