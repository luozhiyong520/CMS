using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMS.BLL;
using SMVC;
using Common;
using System.Web;
using CMS.Model;
using CMS.ViewModel;

namespace CMS.Controller
{
    [Authorize]
    public class AjaxChannel
    {
        ChannelBLL channelBLL = Factory.BusinessFactory.CreateBll<ChannelBLL>();
        string dropdownStr;

        [Action]
        public string InsertChannel(string parentID, string channelName, string url, string typeID, string channelEnName, bool status)
        {

            string strmsg = channelBLL.InsertChannel(parentID, channelName, url, typeID, channelEnName, UserCookies.AdminName, status);
            return strmsg;
        }

        [Action]
        public string UpdateChannel(string channelID, string parentID, string channelName, string url, string typeID, string channelEnName,  bool status)
        {
            string strmsg = channelBLL.UpdateChannel(channelID, parentID,channelName, url, typeID, channelEnName, UserCookies.AdminName, status);
            return strmsg;
        }

        [Action]
        public string deleteChannelByID(string channelID)
        {
            string msg = "";
            Channel channel = new Channel();
            channel.ChannelId = channelID;
            SqlWhereList sqlWhere=new SqlWhereList();
            sqlWhere.Add("ParentID", channelID);
            List<Channel> channellist= channelBLL.GetAll(sqlWhere);
            if (channellist.Count > 0)
            {
                msg = "还有子节点，不能直接删除！！！";
            }
            else
            {
                channelBLL.Delete(channel);
            }
            return msg;
        }

        [Action]
        public JsonResult getChannelByID(string channelID, int TypeId)
        {
            string[] channelIDs = channelID.Split('"');
            channelID = channelIDs[0];
            Channel msg = channelBLL.Get("ChannelID", channelID);
            string shtml = LoaddrpData(channelID, TypeId);
            msg.Shtml = shtml;
            JsonResult json = new JsonResult(msg);
            return json;
        }

        [Action]
        public object GetChannelNodes(int TypeId)
        {
            JsTreeNode root = new JsTreeNode();
            SqlWhereList where = new SqlWhereList();
                where.Add("ParentID", "0");
                where.Add("TypeId", TypeId);
                GetTreeEx(where, root);
            return new JsonResult(root.children);
        }
       


        private void GetTreeEx(SqlWhereList where, JsTreeNode root)
        {
            List<Channel> list = channelBLL.GetAll(where);
            JsTreeNode node = null;
            root.children = new List<JsTreeNode>(); 
            foreach (Channel item in list)
            {
                node = new JsTreeNode();
                node.id = item.ChannelId;
                node.text = item.ChannelName;
                node.attributes = new JsTreeNodeCustAttr(node.id);
                SqlWhereList wherelist = new SqlWhereList();
                wherelist.Add("ParentID", node.id);
                if (item.TypeId==1)
                {
                    wherelist.Add("TypeId", 1);
                }
                else if (item.TypeId == 2)
                {
                    wherelist.Add("TypeId", 2);
                }
                GetTreeEx(wherelist, node);
                root.children.Add(node);
            }
        }




        public string LoaddrpData(string channelID, int TypeId)
        {
            ChannelPageModel channelPageModel = new ChannelPageModel();
            SqlWhereList where = new SqlWhereList();
            where.Add("ParentID", "0");
            where.Add("TypeId", TypeId);
            return channelPageModel.strAll = GetChild(where,channelID, channelPageModel.Channel, "", "|", "--");
        }

        [Action]
        public string GetChannelNodeInit(int typeID)
        {
            ChannelPageModel channelPageModel = new ChannelPageModel();
            SqlWhereList where = new SqlWhereList();
            where.Add("ParentID", "0");
            where.Add("TypeId", typeID);
            //return channelPageModel.strAll = GetChildInit(where,"", "|", "--");
            return channelPageModel.strAll = GetChildInit(where, channelPageModel.Channel, "", "|", "--"); 
        }

        /// <summary>
        /// 获取添加新闻中的栏目关联
        /// </summary>
        /// <returns></returns>
        [Action]
        public string GetChannelNodeNews(int typeID)
        {
            string dropdownStr = "";
            SqlWhereList where = new SqlWhereList();
            where.Add("ParentID", "001");
            where.Add("TypeId", typeID);
            List<Channel> list = channelBLL.GetAll(where);
            int i = 0;
            foreach (Channel ch in list)
            {
                string ChannelId = ch.ChannelId;
                string txt = ch.ChannelName;
                if (i == list.Count - 1)
                {
                    dropdownStr += " {k:\"" + ChannelId + "\",v:\"" + txt + "\"}";
                }
                else
                {
                    dropdownStr += " {k:\"" + ChannelId + "\",v:\"" + txt + "\"}|";
                }
                i++;
            }
            return dropdownStr;
             
        }

        /// <summary>
        /// 获取被选栏目的列表
        /// </summary>
        /// <param name="typeID"></param>
        /// <param name="ChannelId"></param>
        /// <returns></returns>
        [Action]
        public string GetChannelNodeSelected(int typeID,string ChannelId)
        {
            ChannelPageModel channelPageModel = new ChannelPageModel();
            SqlWhereList where = new SqlWhereList();
            where.Add("ParentID", "0");
            where.Add("TypeId", typeID);
            //return channelPageModel.strAll = GetChildInit(where,"", "|", "--");
            return channelPageModel.strAll = GetChildInitSelected(where, channelPageModel.Channel, "", "|", "--",ChannelId);
        }

        /// <summary>
        /// 获取被选栏目的列表
        /// </summary>
        /// <param name="where"></param>
        /// <param name="chNode"></param>
        /// <param name="option"></param>
        /// <param name="leveStr"></param>
        /// <param name="nextStr"></param>
        /// <param name="ChannelId"></param>
        /// <returns></returns>
        private string GetChildInitSelected(SqlWhereList where, Channel chNode, string option, string leveStr, string nextStr,string ChannelId)
        {
            dropdownStr += option;
            List<Channel> list = channelBLL.GetAll(where);
            foreach (Channel ch in list)
            {
                string txt = ch.ChannelName;
                if (leveStr.Length != 1)
                {
                    txt = leveStr + ch.ChannelName.ToString();
                }
                if (ch.ChannelId == ChannelId)
                {
                    dropdownStr += " <option value='" + ch.ChannelId + "' selected>" + txt + "</option>";
                }
                else
                {
                    dropdownStr += " <option value='" + ch.ChannelId + "'>" + txt + "</option>";
                }
                SqlWhereList sqllist = new SqlWhereList();
                sqllist.Add("ParentID", ch.ChannelId);
                if (ch.TypeId == 1)
                {
                    sqllist.Add("TypeId", 1);
                }
                else if (ch.TypeId == 2)
                {
                    sqllist.Add("TypeId", 2);
                }
                List<Channel> channelList = channelBLL.GetAll(sqllist);
                if (channelList.Count != 0)
                {
                    string nextLevelStr = leveStr.Insert(0, nextStr);
                    GetChildInitSelected(sqllist, ch, option, nextLevelStr, nextStr, ChannelId);
                }
            }
            return dropdownStr;
        }


        /// <summary>
        /// 绑定下拉框
        /// </summary>
        /// <param name="where">绑定的数据源的查询条件</param>
        /// <param name="options">下拉框</param>
        /// <param name="leveStr">层级的前缀</param>
        /// <param name="nextStr">层级的前缀的前缀</param>
        private string GetChild(SqlWhereList where,string channelID, Channel chNode,string option, string leveStr, string nextStr)
        {
            dropdownStr += option;
            List<Channel> list = channelBLL.GetAll(where);
            foreach (Channel ch in list)
            {
                string txt = ch.ChannelName;
                if (leveStr.Length != 1)
                {
                    txt = leveStr + ch.ChannelName.ToString();
                }
                if (ch.ChannelId != channelID && ch.ParentId != channelID) //判断不在树中
                {
                    dropdownStr += " <option value='" + ch.ChannelId + "'>" + txt + "</option>";

                    SqlWhereList sqllist = new SqlWhereList();
                    sqllist.Add("ParentID", ch.ChannelId);
                    if (ch.TypeId == 1)
                    {
                        sqllist.Add("TypeId", 1);
                    }
                    else if (ch.TypeId == 2)
                    {
                        sqllist.Add("TypeId", 2);
                    }
                    List<Channel> channelList = channelBLL.GetAll(sqllist);
                    if (channelList.Count != 0)
                    {
                        string nextLevelStr = leveStr.Insert(0, nextStr);
                        string nd = channelID;
                        GetChild(sqllist, nd, ch, option, nextLevelStr, nextStr);
                    }
                }
            }
            return dropdownStr;
        }

        private string GetChildInit(SqlWhereList where, Channel chNode, string option, string leveStr, string nextStr)
        {
            dropdownStr += option;
            List<Channel> list = channelBLL.GetAll(where);
            foreach (Channel ch in list)
            {
                string txt = ch.ChannelName;
                if (leveStr.Length != 1)
                {
                    txt = leveStr + ch.ChannelName.ToString();
                }
                 dropdownStr += " <option value='" + ch.ChannelId + "'>" + txt + "</option>";
                SqlWhereList sqllist = new SqlWhereList();
                sqllist.Add("ParentID", ch.ChannelId);
                if (ch.TypeId == 1)
                {
                    sqllist.Add("TypeId", 1);
                }
                else if (ch.TypeId == 2)
                {
                    sqllist.Add("TypeId", 2);
                }
                List<Channel> channelList = channelBLL.GetAll(sqllist);
                if (channelList.Count != 0)
                {
                    string nextLevelStr = leveStr.Insert(0, nextStr);
                    GetChildInit(sqllist, ch, option, nextLevelStr, nextStr);
                }
            }
            return dropdownStr;
        }
        private string GetChildInit(SqlWhereList where, string option, string leveStr, string nextStr)
        {
            dropdownStr += option;
            List<Channel> list = channelBLL.GetAll(where);
            foreach (Channel ch in list)
            {
                string txt = ch.ChannelName;
                if (leveStr.Length != 1)
                {
                    txt = leveStr + ch.ChannelName.ToString();
                }
                dropdownStr += " <option value='" + ch.ChannelId + "'>" + txt + "</option>";
                SqlWhereList sqllist = new SqlWhereList();
                sqllist.Add("ParentID", ch.ChannelId);
                List<Channel> channelList = channelBLL.GetAll(sqllist);
                if (channelList.Count != 0)
                {
                    string nextLevelStr = leveStr.Insert(0, nextStr);
                    GetChildInit(sqllist, option, nextLevelStr, nextStr);
                }
            }
            return dropdownStr;
        }

    }
}
