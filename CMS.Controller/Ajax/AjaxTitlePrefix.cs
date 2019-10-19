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
   public class AjaxTitlePrefix
    {
        NewsTitlePrefixBLL newsTitlePrefixBLL = Factory.BusinessFactory.CreateBll<NewsTitlePrefixBLL>();
        NewsBLL newsBLL = Factory.BusinessFactory.CreateBll<NewsBLL>();
        /// <summary>
        /// 获取标题前列表
        /// </summary>
        /// <returns></returns>
        [Action]
        public string GetNewsTitlePrefix()
        {
            NewsTitlePrefixModel newsTitlePrefixModel = new NewsTitlePrefixModel();
            return newsTitlePrefixModel.GetTitlePrefix = GetChildInit();
        }
        /// <summary>
        /// 获取标题前列表 
        /// </summary>
        /// <returns></returns>
        private string GetChildInit()
        {
            string dropdownStr="";
            List<NewsTitlePrefix> list = newsTitlePrefixBLL.GetAll();
            foreach (NewsTitlePrefix ch in list)
            {
                string txt = ch.Prefix;
                dropdownStr += " <option value='" + txt + "'>" + txt + "</option>";
            }
            return dropdownStr;
        }

        public string GetChildInit(int NewsId)
        {
            string dropdownStr = "";
            News news = new News();
            news=newsBLL.Get("NewsId", NewsId);
            List<NewsTitlePrefix> list = newsTitlePrefixBLL.GetAll();
            foreach (NewsTitlePrefix ch in list)
            {
                string txt = ch.Prefix;
                if(news.Prefix ==txt)
                  dropdownStr += " <option value='" + txt + "' selected>" + txt + "</option>";
                else
                  dropdownStr += " <option value='" + txt + "'>" + txt + "</option>";
            }
            return dropdownStr;
        }

    }
}
