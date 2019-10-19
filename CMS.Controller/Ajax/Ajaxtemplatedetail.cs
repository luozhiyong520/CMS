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
using CMS.Utility;
using System.Data;
using Factory;

namespace CMS.Controller
{
    [Authorize]
    public class Ajaxtemplatedetail
    {
        //select * from dbo.PageTemplate where TemplateId not in(select TemplateId from TemplateDetail where TemplateId=11 and ChannelId in(002001,002001001))

        ChannelBLL channelBLL = BusinessFactory.CreateBll<ChannelBLL>();
        TemplateDetailBLL templateDetailBLL = BusinessFactory.CreateBll<TemplateDetailBLL>();

        #region 获得树形结构数据
        [Action]

        public object GetzTreeData(int templateId, int templeteType)
        {
            zTreeNode root = new zTreeNode();
            root.name = "栏目列表";
            root.open = true;
            root.templateId = templateId;
            SqlWhereList where = new SqlWhereList();
            where.Add("ParentID", "0");
            if (templeteType == 2)
            {
                where.Add("TypeId", 1);
            }
            GetzTree(where, root, templateId, templeteType);
            return new JsonResult(root.children);
        }

        private void GetzTree(SqlWhereList where, zTreeNode root, int templateId, int templeteType)
        {
            List<Channel> list = channelBLL.GetAll(where);
            zTreeNode node = null;
            root.children = new List<zTreeNode>();
            foreach (Channel item in list)
            {
                node = new zTreeNode();
                DataTable dt = templateDetailBLL.GetTemplateDetailChecked(templateId);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["ChannelId"].Equals(item.ChannelId) && templeteType == 2)
                    {
                        node.@checked = true;
                        node.chkDisabled = true;//设为禁用
                    }
                }
                node.id = item.ChannelId;
                node.name = item.ChannelName;
                node.pId = item.ParentId;
                node.open = true;
                TemplateDetail _TemplateDetail = getIsExistTemplatedetail(templateId, node.id);
                if (_TemplateDetail == null)
                {
                    node.@checked = false;
                }
                else
                {
                    node.@checked = true;
                }
                node.attributes = new zTreeNodeCustAttr(node.id);
                SqlWhereList wherelist = new SqlWhereList();
                wherelist.Add("ParentID", node.id);
                if (templeteType == 2)
                {
                    wherelist.Add("TypeId", 1);
                }
                GetzTree(wherelist, node, templateId, templeteType);
                root.children.Add(node);
            }
        }

        #endregion

        #region 点击保存时执行删除和新增方法

        [Action]
        public int IsertTemplatedetail(int templateId, string channelIds, int templeteType)
        {
            int strmsg = 0;
            strmsg = templateDetailBLL.DeleteTemplateDetailByTemplateId(templateId);
            if (!string.IsNullOrEmpty(channelIds))
            {
                string[] channelIdlist = channelIds.Split(',');
                TemplateDetail templateDetail = new TemplateDetail();
                templateDetail.TemplateId = templateId;
                templateDetail.RelationType = templeteType == 1 ? 1 : 2;//1:涉及；2:所属
                foreach (string chId in channelIdlist)
                {
                    TemplateDetail _TemplateDetail = getIsExistTemplatedetail(templateId, chId);
                    if (_TemplateDetail == null)
                    {
                        if (!string.IsNullOrEmpty(chId))
                        {
                            templateDetail.ChannelId = chId;
                            strmsg = templateDetailBLL.Add(templateDetail);
                        }
                    }
                }
            }
            return strmsg;
        }

        private TemplateDetail getIsExistTemplatedetail(int templateId, string channelId)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("TemplateId", templateId);
            dic.Add("ChannelId", channelId);
            TemplateDetail templateDetail = templateDetailBLL.Get(dic);
            return templateDetail;
        }
        #endregion
    }
}
