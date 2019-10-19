using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMS.ViewModel
{
    public sealed class JsTreeNode
    {
        public string id;
        public string text;
        public string state;
        public string iconCls;
        public List<JsTreeNode> children;
        public JsTreeNodeCustAttr attributes;
    }


    public sealed class JsTreeNodeCustAttr
    {

        public string ParentID;
        public JsTreeNodeCustAttr()
        {
           // children = null;
            ParentID = string.Empty;
        }
        public JsTreeNodeCustAttr(string ParentID)
        {
            this.ParentID = ParentID;
        }
    }
}