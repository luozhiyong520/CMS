using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMVC;
using CMS.Model;
using CMS.BLL;
using Common;

namespace CMS.Controller
{
    [Authorize]
    public class AjaxAuthorityDot
    {
       AuthorityDotBLL AuthorityBLL = Factory.BusinessFactory.CreateBll<AuthorityDotBLL>();
       AuthorityDotAdminBLL authorityDotAdminBLL = Factory.BusinessFactory.CreateBll<AuthorityDotAdminBLL>();
        /// <summary>
        /// 添加功能点权限
        /// </summary>
        /// <param name="dot"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [Action]
        public int AddAuthority(string dotName, string dot, int type)
        {
            AuthorityDot Authority = new AuthorityDot();
            if (string.IsNullOrEmpty(dotName) || string.IsNullOrEmpty(dot))
                return 0;  //用户组名或功能点权限不能为空;

            string[] aa = dot.Split(';');
            Authority.ParentId = 0;
            Authority.Text = dotName;
            Authority.Type = type;
            Authority.Status = true;
            int parentId = AuthorityBLL.Add(Authority);

            foreach (var item in aa)
            {
                Authority.ParentId = parentId;
                Authority.Text = item.Split(',')[0];
                Authority.Type = type;
                Authority.RelevanceId = item.Split(',')[1];
                Authority.Status = true;
                AuthorityBLL.Add(Authority);
            }

            return parentId; 

        }

        /// <summary>
        /// 删除功能点权限
        /// </summary>
        /// <param name="groupid"></param>
        /// <returns></returns>
        [Action]
        public string DelAuthorityDot(int authorityid)
        {
            AuthorityDot authority = new AuthorityDot();
            authority.Id = authorityid;
            AuthorityBLL.Delete(authority);
            return AuthorityBLL.Delete(authority).ToString();
        }
        [Action]
        public string DelAjaxAuthorityDotById(int id)
        {
            AuthorityDot authority = new AuthorityDot();
            authority.Id =id;
            string str = AuthorityBLL.Delete(authority).ToString();
            return str;
        }

        /// <summary>
        /// 更改可用状态
        /// </summary>
        /// <returns></returns>
        [Action]
        public string SetStatus(int id, int status)
        {
            AuthorityDot authority = AuthorityBLL.Get("Id", id);
            if (status == 0)
                authority.Status = false;
            else if (status == 1)
                authority.Status = true;

            return AuthorityBLL.Update(authority).ToString();
        }

        //获取下拉列表的权限数据
        [Action]
        public string GetAuthoritydot(int productId)
        {
            string shtml = "";
            int adminId = UserCookies.AdminId;
            List<AuthorityDot> list = AuthorityBLL.GetAuthoritydot(adminId, productId);
            if (list != null)
            {
                foreach (AuthorityDot item in list)
                {
                    shtml += "<option value=\"" + item.RelevanceId + "\">" + item.Text + "</option>";
                }
            }
            return shtml;
        }
        //获取下来列表查看权限数据
        [Action]
        public string GetAuthoritydotLook()
        {
            string shtml = "";
            SqlWhereList str = new SqlWhereList();
            str.Add("ParentId",1);
            str.Add("Status", 1);
            List<AuthorityDot> list = AuthorityBLL.GetAll(str);
            if (list != null)
            {
                foreach (AuthorityDot item in list)
                {
                    shtml += "<option value=\"" + item.RelevanceId + "\">" + item.Text + "</option>";
                }
            }
            return shtml;
        }
        /// <summary>
        /// 根据用户登录id得到推送权限
        /// </summary>
        /// <returns></returns>
        [Action]
        public JsonResult GetAuthorityDotAdmin(int parentId)
        {
            int adminId = UserCookies.AdminId;
            List<AuthorityDot> list=AuthorityBLL.GetAuthorityDotAdmin(adminId,parentId);
            JsonResult json = null;
            if (list!=null)
            {
              json = new JsonResult(list);
            }
            return json;
        }

        /// <summary>
        /// 添加用户功能点权限
        /// </summary>
        /// <param name="dot"></param>
        /// <returns></returns>
        public string AddAuthorityDotAdmin(string dot)
        {
            return "aaa";
        }
        /// <summary>
        /// 设置用户的功能点权限
        /// </summary>
        /// <param name="checks"></param>
        /// <returns></returns>
        [Action]
        public string UpdateAuthorityDotAdmin(string checks, int adminId)
        {
            string strval = "";
           // int adminId = UserCookies.AdminId;
            AuthorityBLL.DelAuthoritydot(adminId);
            if (!string.IsNullOrEmpty(checks))
            {
                string[] AuthorityDotIds = checks.Split(',');
                AuthorityDotAdmin authorityDotAdmin = null;
                foreach (var AuthorityDotId in AuthorityDotIds)
                {
                    authorityDotAdmin = new AuthorityDotAdmin();
                    authorityDotAdmin.AuthorityDotId = int.Parse(AuthorityDotId);
                    authorityDotAdmin.AdminId = adminId;
                    strval += authorityDotAdminBLL.Add(authorityDotAdmin);
                }
            }
            return strval;
        }
        //更加adminId展示权限
        [Action]
        public JsonResult ShwowAuthorityDotAdmin(int adminId)
        {
            SqlWhereList sqlwhere = new SqlWhereList();
            sqlwhere.Add("AdminId", adminId);
            List<AuthorityDotAdmin> list = authorityDotAdminBLL.GetAll(sqlwhere);
            JsonResult json = new JsonResult(list);
             return json;
        }
        /// <summary>
        /// 绑定权限数据
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Action]
        public JsonResult ShowAjaxAuthorityDotById(int Id)
        {
            SqlWhereList sqlwhere = new SqlWhereList();
            sqlwhere.Add("ParentId", Id);
            List<AuthorityDot> list = AuthorityBLL.GetAll(sqlwhere);
            JsonResult json = new JsonResult(list);
            return json;
        }

        /// <summary>
        /// 更新权限数据
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Action]
        public int UpdateAjaxAuthorityDotById(int Id, string dots, int type)
        {
            int count = 0;
            AuthorityDot Authority = new AuthorityDot();
            string[] dot = dots.Split(';');
            if (string.IsNullOrEmpty(dots))
                return 0;  //功能点权限不能为空;
            foreach (var item in dot)
            {
                Authority.ParentId = Id;
                Authority.Text = item.Split(',')[0];
                Authority.Type = type;
                Authority.RelevanceId = item.Split(',')[1];
                Authority.Status = true;
                if (!item.Split(',')[2].Equals("undefined"))
                {
                    Authority.Id =int.Parse(item.Split(',')[2]);
                    AuthorityBLL.Update(Authority);
                }
                else
                {
                    count = AuthorityBLL.Add(Authority);
                }
            }
            return count;
        }
    }
}
