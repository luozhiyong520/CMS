using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMS.DAL;
using System.Data;
using Common;
using CMS.Model;
using CMS.DAL.Ext;


namespace CMS.BLL.Ext
{
    public partial class GetFrontPageListBLL
    {
        /// <summary>
        /// 获取文件搜索列表
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageNo"></param>
        /// <param name="TotalFlag"></param>
        /// <param name="ChannelId"></param>
        /// <param name="Sort"></param>
        /// <returns></returns>
        public PagedResult GetFrontPageList(int? page, int rows, int TotalFlag, string ChannelId, int Sort)
        {
            GetFrontPageListDAl FrontPageListDal = new GetFrontPageListDAl();
            PagedResult st = FrontPageListDal.GetFrontPageList(page, rows, TotalFlag, ChannelId, Sort);
            return st; 
        }
    }
}
