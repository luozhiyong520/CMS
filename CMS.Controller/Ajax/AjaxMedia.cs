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
using CMS.Utility;
using System.Data;
using System.IO;

namespace CMS.Controller
{
    [Authorize]
    public class AjaxMedia
    {
       MediaClassBLL MediaClassBLL = Factory.BusinessFactory.CreateBll<MediaClassBLL>();
       MediaBLL MediaBLL = Factory.BusinessFactory.CreateBll<MediaBLL>();
         /// <summary>
       /// 添加文件类别
       /// </summary>
       /// <param name="mediaclassname"></param>
       /// <param name="sort"></param>
       /// <returns></returns>
       [Action]
       public string AddMediaClass(string mediaclassname,int sort)
       {
           MediaClass MediaClass = new MediaClass();
           if (mediaclassname.Length < 1)
           {
               return "000000";   //类别名不能为空 
           }
           MediaClass.MediaClassName = HttpUtility.UrlDecode(mediaclassname);
           MediaClass.SortId = sort;
           return MediaClassBLL.Add(MediaClass).ToString();
 
       }
       /// <summary>
       /// 修改文件类别
       /// </summary>
       /// <param name="mediaclassname"></param>
       /// <param name="sort"></param>
       /// <param name="mediaclassid"></param>
       /// <returns></returns>
       [Action]
       public string UpdateMediaClass(string mediaclassname, int sort, int mediaclassid)
       {
           MediaClass MediaClass = new MediaClass();
           if (mediaclassname.Length < 1)
               return "000000";   //类别名不能为空 
           MediaClass.MediaClassName = HttpUtility.UrlDecode(mediaclassname);
           MediaClass.SortId = sort;
           MediaClass.MediaClassId = mediaclassid;
           return MediaClassBLL.Update(MediaClass).ToString();
       }
       /// <summary>
       /// 删除文件类别
       /// </summary>
       /// <param name="mediaclassid"></param>
       /// <returns></returns>
       [Action]
       public string DelMediaClass(int mediaclassid)
       {
           MediaClass MediaClass = new MediaClass();
           MediaClass.MediaClassId = mediaclassid;
           return MediaClassBLL.Delete(MediaClass).ToString();

       }

        /// <summary>
        /// 上传图片方法
        /// </summary>
        /// <param name="Media"></param>
        /// <returns></returns>
        [Action]
       public string UploadPic(Media Media, string MediaScale, int WaterMark, int smallPiexl,string WaterImagePath, int waterPosition,string UploadType)
        {
            UpLoadMedia upLoadMedia = new UpLoadMedia();
            string upfilepath = HttpContext.Current.Request.QueryString["Filedata"]; //取得上传的对象名称
            HttpPostedFile upfile = HttpContext.Current.Request.Files[upfilepath];
            if (upfile == null)
            {
                return "请选择图片";
            }
            upLoadMedia.WaterPath = WaterImagePath;
            upLoadMedia.MediaScale = MediaScale;
            if (MediaScale == "h")
            {
                upLoadMedia.ThumbNail_Height = smallPiexl;
            }
            else
            {
                upLoadMedia.ThumbNail_Width = smallPiexl;
            }
            upLoadMedia.WaterPostion = waterPosition;
            
            string msg = upLoadMedia.fileSaveAs(upfile, WaterMark);
            if (!string.IsNullOrEmpty(UpLoadMedia.UploadPath))
            {
                if (UploadType == "add")
                {
                    Media.MediaClassId = Media.MediaClassId;
                    Media.MediaTitle = Media.MediaTitle;
                    Media.UploadTime = DateTime.Now;
                    Media.UploadPath = UpLoadMedia.UploadPath;
                    Media.MediaSize = UpLoadMedia.MediaSize;
                    Media.MediaType = Media.MediaType;
                    Media.Uploader = UserCookies.AdminName;
                    Media.MediaLabel = Media.MediaLabel;
                    Media.MediaDescript = Media.MediaDescript;
                    MediaBLL.Add(Media).ToString();
                }
                else
                {
                    Media.MediaId = Media.MediaId;
                    Media.MediaClassId = Media.MediaClassId;
                    Media.MediaTitle = Media.MediaTitle;
                    Media.UploadTime = DateTime.Now;
                    Media.UploadPath = UpLoadMedia.UploadPath;
                    Media.MediaSize = UpLoadMedia.MediaSize;
                    Media.MediaType = Media.MediaType;
                    Media.Uploader = UserCookies.AdminName;
                    Media.MediaLabel = Media.MediaLabel;
                    Media.MediaDescript = Media.MediaDescript;
                    MediaBLL.Update(Media).ToString();
                }
                upfile.InputStream.Close();
                upfile.InputStream.Dispose();
                return "操作成功";
            }
            else
            {
                return msg;
            }
         
        }

        

   

        /// <summary>
        /// 获取单张图片信息
        /// </summary>
        /// <param name="mediaId"></param>
        /// <returns></returns>
       [Action]
       public JsonResult GetMediaInfo(int mediaId)
       {
           return new JsonResult(MediaBLL.Get("MediaId", mediaId.ToString()));
       }

       

       /// <summary>
       /// 删除单个文件
       /// </summary>
       /// <param name="mediaId"></param>
       /// <returns></returns>
       [Action]
       public string DelMedia(int MediaId)
       {
           MediaPageModel MediaMode = new MediaPageModel();
           Media Media = new Media();
           Media.MediaId = MediaId;
           MediaMode.MediaList = MediaBLL.GetUploadList(null, MediaId, "GetSingleUpladPath");
             foreach (Media m in MediaMode.MediaList)
             {
                 if (!string.IsNullOrEmpty(m.UploadPath))
                 {
                     FileServer.DeleteFile(m.UploadPath, "SinggleDel");
                 }
             }
             MediaBLL.MediaOperate(null, MediaId, "SinggleDel");
             return "000000";
       }

       /// <summary>
       /// 删除多个文件
       /// </summary>
       /// <param name="key"></param>
       /// <returns></returns>
       [Action]
       public string DelAllMedia(string MediaId)
       {
           MediaPageModel MediaMode = new MediaPageModel();
           string UploadPath = "";
           MediaMode.MediaList = MediaBLL.GetUploadList(MediaId, null, "GetBacthUploadPath");
           foreach (Media m in MediaMode.MediaList)
           {
               if (!string.IsNullOrEmpty(m.UploadPath))
               {
                   UploadPath += m.UploadPath + ",";
               }
           }
           UploadPath = UploadPath.Substring(0, UploadPath.Length - 1);
           FileServer.DeleteFile(UploadPath, "BacthDel");
           MediaBLL.MediaOperate(MediaId, null, "BacthDel");
           return "000000";
       }

      

       /// <summary>
       /// 搜索文件/文件列表
       /// </summary>
       /// <param name="page"></param>
       /// <param name="rows"></param>
       /// <param name="txtKeyword"></param>
       /// <param name="txtStartDate"></param>
       /// <param name="txtEndDate"></param>
       /// <returns></returns>
       [Action]
       public JsonResult SearchMedia(int? page, int rows, string txtKeyword, string txtStartDate, string txtEndDate, int MediaClass)
       {
           MediaPageModel MediaMode = new MediaPageModel();
           PagingInfo PageInfo = new PagingInfo();
           PagedResult pagedResult = MediaBLL.GetMediaList(page, rows, txtKeyword, txtStartDate, txtEndDate, MediaClass);
           PageInfo.PageIndex = page.HasValue ? page.Value - 1 : 0;
           PageInfo.PageSize = rows;
           PageInfo.TotalRecords = pagedResult.Total;
           MediaMode.DataTable = pagedResult.Result;

           var result = new GridResult<Media>(pagedResult.Result.ToList<Media>(), pagedResult.Total);
           return new JsonResult(result);

       }

       /// <summary>
       /// 批量获取图片信息
       /// </summary>
       /// <param name="key"></param>
       /// <returns></returns>
       [Action]
       public string GetMediaImage(string MediaId)
       {
           MediaPageModel MediaMode = new MediaPageModel();
           MediaMode.MediaList = MediaBLL.GetUploadList(MediaId, null, "GetBacthUploadPath");
           string img="";
           foreach (Media m in MediaMode.MediaList)
           {
                img += m.UploadPath + "&";
           }
           img = img.Substring(0, img.Length - 1);
           return img;
       }

    }
}
