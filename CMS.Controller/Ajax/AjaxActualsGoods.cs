using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMS.BLL.Oracle;
using CMS.Model.Oracle;
using CMS.ViewModel.PageData;
using Common;
using SMVC;
using CMS.ViewModel;

namespace CMS.Controller
{
    public class AjaxActualsGoods
    {
        XH_PROCUDT_TYPEBLL actualsGoodsBLL = Factory.BusinessFactory.CreateBll<XH_PROCUDT_TYPEBLL>(true);


        //添加品种数据
        [Action]
        public void AddActualsGoods(string code,string name)
        {
            actualsGoodsBLL.AddActualsGoods(code, name);
        }

        //查询品种数据
        [Action]
        public string SelectActualsGoods(string code, string name)
        {
            List<XH_PROCUDT_TYPE> seachGoods = actualsGoodsBLL.SelectGoods(code, name);

            return JsonHelper.ToJson(seachGoods);
        }

        //删除品种数据
        public string DeleteActualsGoods(string code, string name)
        {
            actualsGoodsBLL.DeleteGoodsType(code);
            //查询数据
            List<XH_PROCUDT_TYPE> seachGoods = actualsGoodsBLL.SelectGoods(code, name);
            return JsonHelper.ToJson(seachGoods);
        }
    }
}
