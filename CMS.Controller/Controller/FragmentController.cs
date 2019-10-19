using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMS.BLL;
using SMVC;
using CMS.Model;
using CMS.ViewModel;
using Common;

namespace CMS.Controller
{
   
    public class FragmentController
    {
        FragmentBLL fragmentBll = Factory.BusinessFactory.CreateBll<FragmentBLL>();
        ChannelBLL channelBLL = Factory.BusinessFactory.CreateBll<ChannelBLL>();
        [Action]
        [PageUrl(Url = "/fragment/fragmentlist.aspx")]
        public object GetMenueList(int TypeId)
        {
            FragmentPageModel model = new FragmentPageModel();
            SqlWhereList sqlwhere = new SqlWhereList();
            sqlwhere.Add("ParentID","0");
            sqlwhere.Add("TypeId", 2);
            model.strhtml = GetChannelList(sqlwhere, "", "|", "--");
            model.TypeId = TypeId;
            return new PageResult(null, model);

        }
        [Action]
        [PageUrl(Url = "/fragment/fragment_history.aspx")]       
        public object GetFragmentHistory(string channelId)
        {
            FragmentPageModel model = new FragmentPageModel();
            model.DataTableHistory = fragmentBll.GetFragmentHistory(channelId);
            return new PageResult(null, model);
        }
        #region 绑定下拉框
        string options = "";
        /// <summary>
        /// 绑定下拉框
        /// </summary>
        /// <param name="where">绑定的数据源的查询条件</param>
        /// <param name="options">下拉框</param>
        /// <param name="leveStr">层级的前缀</param>
        /// <param name="nextStr">层级的前缀的前缀</param>
        public string GetChannelList(SqlWhereList where, string option, string leveStr, string nextStr)
        {
            options += option;
            List<Channel> list = channelBLL.GetAll(where);
            foreach (Channel ch in list)
            {
                string txt = ch.ChannelName;
                if (leveStr.Length != 1)
                {
                    txt = leveStr + ch.ChannelName.ToString();
                }
                options += " <option value='" + ch.ChannelId + "'>" + txt + "</option>";
                SqlWhereList sqllist = new SqlWhereList();
                sqllist.Add("ParentID", ch.ChannelId);
                sqllist.Add("TypeId", 2);
                List<Channel> channelList = channelBLL.GetAll(sqllist);
                if (channelList.Count != 0)
                {
                    string nextLevelStr = leveStr.Insert(0, nextStr);
                    GetChannelList(sqllist, option, nextLevelStr, nextStr);
                }
            }
            return options;
        } 
        #endregion
        //private int getFramentCountByType(int Type)
        //{
        //    SqlWhereList sqlwhere = new SqlWhereList();
        //    sqlwhere.Add("Type", Type);
        //    List<Fragment> List = fragmentBll.GetAll(sqlwhere);
        //    return List.Count;
        //}
    }
}
