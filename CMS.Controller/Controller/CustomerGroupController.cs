using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMS.BLL;
using SMVC;
using CMS.Model;
using CMS.ViewModel;
using Common;
using System.Web;

namespace CMS.Controller
{
    [Authorize]
    public class CustomerGroupController
    {
        CustomerGroupBLL CustomerGroupBLL = Factory.BusinessFactory.CreateBll<CustomerGroupBLL>();
        /// <summary>
        /// 获取用户组列表
        /// </summary>
        /// <returns></returns>
        [Action]
        [PageUrl(Url = "/popupwindow/customergroup.aspx")]
        public object CustomerGroupList()
        {
            CustomerGroupPageModel model = new CustomerGroupPageModel();
            model.CustomerGroupList = CustomerGroupBLL.CustomerGroupList();
            return new PageResult(null, model);
        }
    }
}
