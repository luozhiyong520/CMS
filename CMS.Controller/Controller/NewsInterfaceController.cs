using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMVC;
using CMS.BLL;
using CMS.ViewModel;
using CMS.Model;
using Common;
using System.Web;
using System.Web.Script.Serialization;
using System.Configuration;

namespace CMS.Controller
{
    public class NewsInterfaceController
    {
        NewsBLL newsBll = Factory.BusinessFactory.CreateBll<NewsBLL>();
       
        [Action]
        [PageUrl(Url = "/news/GetQueryInfo.aspx")]
        public string GetQueryInfo(string keywords, string ChannelId, string callbak)
        {
           
            var news = newsBll.GetQueryInfo(HttpContext.Current.Server.UrlDecode(keywords), ChannelId);
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return callbak + "(" + jss.Serialize(JsonHelper.ToJson(news)) + ")";
        
        }


        [Action]
        [PageUrl(Url = "/news/GetPassWord.aspx")]
        public string GetPassWord(string pass, string callbak)
        {
            var password = ConfigurationManager.AppSettings["PassWord"].ToString();
            if (pass.Trim() == password.Trim())
                return callbak + "({\"result\":\"true\"})";
            return callbak + "({\"result\":\"false\"})";

        }

        [Action]
        [PageUrl(Url = "/user/userpass.aspx")]
        public object userlist(string pass)
        {
            UserManagePageModel model = new UserManagePageModel();
            model.password = ConfigurationManager.AppSettings["PassWord"].ToString();
            return new PageResult(null, model);

        }

    }
}
