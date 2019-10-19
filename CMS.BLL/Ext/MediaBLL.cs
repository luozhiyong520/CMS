using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMS.DAL;
using System.Data;
using Common;
using CMS.Model;

namespace CMS.BLL
{
    public partial class MediaBLL
    {
        /// <summary>
        /// 获取文件搜索列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="txtKeyword"></param>
        /// <param name="txtStartDate"></param>
        /// <param name="txtEndDate"></param>
        /// <returns></returns>
        public PagedResult GetMediaList(int? page, int rows, string txtKeyword, string txtStartDate, string txtEndDate, int MediaClass)
        {
            PagedResult st = dal.GetMediaList(page, rows, txtKeyword, txtStartDate, txtEndDate, MediaClass);
            return st;
        }

        /// <summary>
        /// 获取图片路径
        /// </summary>
        /// <param name="strMediaId"></param>
        /// <returns></returns>
        public List<Media> GetUploadList(string MediaBacthId, int? MediaId, string operateType)
        {
            return dal.GetUploadList(MediaBacthId, MediaId, operateType);
        }
        /// <summary>
        /// 文件细节操作
        /// </summary>
        /// <param name="MediaBacthId"></param>
        /// <param name="MediaId"></param>
        /// <param name="operateType"></param>
        public void MediaOperate(string MediaBacthId, int? MediaId,string operateType)
        {
            dal.MediaOperate(MediaBacthId, MediaId, operateType);
        }

   

    }
}
