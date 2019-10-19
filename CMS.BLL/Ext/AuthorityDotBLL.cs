using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMS.DAL;
using CMS.Model;
using Common;
using Factory;

namespace CMS.BLL
{
   public partial class AuthorityDotBLL
    {
       public List<AuthorityDot> GetAuthorityDotAdmin(int adminId, int parentId)
       {
           return dal.GetAuthorityDotAdmin(adminId,parentId);
       }

       public List<AuthorityDot> GetAuthoritydot(int adminId,int productId)
       {
           return dal.GetAuthoritydot(adminId, productId);
       }

       public int DelAuthoritydot(int adminId)
       {
           return dal.DelAuthoritydot(adminId);
       }

       public int DelAuthoritydotByParentId(int Id)
       {
           return dal.DelAuthoritydotByParentId(Id);
       }

       /// <summary>
       /// 通过功能点名称，获取功能点对象
       /// </summary>
       /// <param name="authorityDotName"></param>
       /// <returns></returns>
       [Cache("AuthorityDotBLL",30)]
       public virtual AuthorityDot GetAuthorityDotByName(string authorityDotName)
       {
           var authorityDot = dal.Get(new Dictionary<string, object>(){{"Text",authorityDotName }});
           return authorityDot;
       }
    }
}
