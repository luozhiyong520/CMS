using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMS.DAL;
using System.Data;
using Common;
using CMS.Model;

namespace CMS.BLL
{
    public partial class FragmentBLL
    {
        public PagedResult GetPageList(int TypeId, string searchdrphannelID, string searchContent, int pageSize, int? pageindex)
        {
            FragmentDAL fragmentdal = new FragmentDAL();
            PagedResult st = fragmentdal.GetPageList(TypeId, searchdrphannelID, searchContent, pageSize, pageindex);
            return st;
        }

        /// <summary>
        /// 如果栏目被对应的列表页模板所涉及，返回对应的模板ID
        /// </summary>
        /// <param name="ChanelId"></param>
        /// <returns></returns>
        public List<int> GetTemplateIdListByChanel(string ChannelId)
        {
            List<int> templateIdList = new List<int>();
            TemplateDetailDAL templateDetailDAL = new TemplateDetailDAL();
            SqlWhereList conditon=new SqlWhereList();
            conditon.Add("ChannelId", ChannelId);
            conditon.Add("RelationType", 1);
           List<TemplateDetail> list=  templateDetailDAL.GetAll(conditon);
           foreach (TemplateDetail item in list)
           {
               templateIdList.Add(item.TemplateId.Value);
           }
           return templateIdList;
        }
        /// <summary>
        /// 历史记录
        /// </summary>
        /// <param name="channelId"></param>
        /// <returns></returns>
        public DataTable GetFragmentHistory(string channelId)
        {
            FragmentDAL fragmentdal = new FragmentDAL();
            return fragmentdal.GetFragmentHistory(channelId);
        }

        /// <summary>
        /// 获取栏目最新的一条碎片
        /// </summary>
        /// <param name="channelId"></param>
        /// <returns></returns>
        public Fragment GetLastOneFrgment(string channelId)
        {
            return dal.GetLastOneFrgment(channelId);
        }
    }
}
