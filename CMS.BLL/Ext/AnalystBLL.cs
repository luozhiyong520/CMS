using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMS.DAL;
using CMS.Model;
using Common;

namespace CMS.BLL
{
    public partial class AnalystBLL
    {
        public string EditAnalyst(Analyst analyst)
        {
            return dal.EditAnalyst(analyst);
        }

        public string EditAnalystSort(int analyst, int sort)
        {
            return dal.EditAnalystSort(analyst, sort);
        }
        public List<Analyst> AnalystList(int adminid)
        {
            List<Analyst> list = dal.AnalystList(adminid);
            return list;
        }
        public PagedResult<Analyst> AnalystListPage(int? page, int rows,int analysType, int vipType)
        {
            return dal.AnalystListPage(page, rows, analysType, vipType);
        }
        public void AnalystRelative(string analystId, int adminId)
        {
            dal.AnalystRelative(analystId, adminId);
        }

        /// <summary>
        /// 获取现货分析师/贵金属分析师列表
        /// </summary>
        /// <param name="AnalystType"></param>
        /// <returns></returns>
        public List<Analyst> Analystlist(int AnalystType)
        {
           return dal.Analystlist(UserCookies.AdminId, AnalystType);
        }

    }
}
