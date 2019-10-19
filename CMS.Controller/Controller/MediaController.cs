using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMS.BLL;
using SMVC;
using CMS.Model;
using CMS.ViewModel;
using Common;


namespace CMS.Controller
{
    [Authorize]
    public class MediaController
    {
        MediaClassBLL mediaClassBLL = Factory.BusinessFactory.CreateBll<MediaClassBLL>();        

        /// <summary>
        /// 获取文件类别列表
        /// </summary>
        /// <returns></returns>
        [Action]
        [PageUrl(Url = "/media/mediaclass.aspx")]
        [PageUrl(Url = "/media/medialist.aspx")]
        [PageUrl(Url = "/media/imagelist.aspx")]
        [PageUrl(Url = "/media/mediaupload.aspx")]
        [PageUrl(Url = "/news/test.aspx")]
        public object MediaClassList()
        {
            MediaPageModel Model = new MediaPageModel();
            Model.MediaClassList = mediaClassBLL.GetAll();
            return new PageResult(null, Model);            
        }
    }
}
