using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMS.BLL;
using CMS.BLL.Oracle;
using SMVC;
using CMS.Model.Oracle;
using CMS.ViewModel;
using Common;
using System.Web;

namespace CMS.Controller
{
    [Authorize]
    public class YmsController
    {
        public static DsYmsUserBLL dsYmsUserBLL = Factory.BusinessFactory.CreateBll<DsYmsUserBLL>();

        [Action]
        [PageUrl(Url = "/cgds/yms.aspx")]
        public object GetDsYmsUserList()
        {
            DsYmsUserPageModel model = new DsYmsUserPageModel();
            model.DsYmsUserList = dsYmsUserBLL.GetYmsUserList("");
            return new PageResult(null, model);
        }

        [Action]
        [PageUrl(Url = "/cgds/deleteyms.aspx")]
        public int DeleteDsYmsUser(string UserName)
        {
            return dsYmsUserBLL.DeleteDsYmsUser(UserName);
        }

        [Action]
        [PageUrl(Url = "/cgds/updateyms.aspx")]
        public int UpdateDsYmsUser(string UserName, string ShowName, decimal LastResults, int Sort)
        {
            DsYmsUserModel model = new DsYmsUserModel();
            model.UserName = UserName;
            model.ShowName = ShowName;
            model.LastResults = LastResults;
            model.Sort = Sort;
            return dsYmsUserBLL.UpdateDsYmsUser(model);
        }

        [Action]
        [PageUrl(Url = "/cgds/insertyms.aspx")]
        public int UpdateDsYmsUser(string UserName, string ShowName, decimal LastResults, int Sort, string Package)
        {
            DsYmsUserModel model = new DsYmsUserModel();
            model.UserName = UserName;
            model.ShowName = ShowName;
            model.LastResults = LastResults;
            model.Sort = Sort;
            model.Package = Package;
            return dsYmsUserBLL.InsertDsYmsUser(model);
        }
    }
}
