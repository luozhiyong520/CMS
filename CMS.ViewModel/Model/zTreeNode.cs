using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMS.ViewModel
{
    public sealed class zTreeNode
    {
        public string id;
        public string pId;
        public string name;
        public bool open;
        public bool @checked;
        public int templateId;
        public bool chkDisabled;
        public List<zTreeNode> children;
        public zTreeNodeCustAttr attributes;
    }
    public sealed class zTreeNodeCustAttr
    {
        
        public string ParentID;
        public zTreeNodeCustAttr()
        {
            ParentID = string.Empty;
        }
        public zTreeNodeCustAttr(string ParentID)
        {
            this.ParentID = ParentID;
        }
    }
}
