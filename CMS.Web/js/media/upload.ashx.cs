using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;
using System.Data;
using CMS.BLL;
using Common;
using CMS.Model;
namespace CMS.Web.media
{
    /// <summary>
    /// upload 的摘要说明
    /// </summary>
    public class upload : IHttpHandler
    {

        public static long MediaSize = 0;
        private readonly string fileType = "GIF|JPG|PNG|BMP|RAR|DOC|XLS|TXT"; //文件类型
        private readonly int fileSize = 1024; //文件大小0为不限制   
        private readonly int waterQuality = 100; //水印图片质量
        private readonly int waterTransparency = 40; //水印图片透时度
        string localPath = "";
        string filename = "";
        string strfile = "NoFile";

        public void ProcessRequest(HttpContext context)
        {
            int MediaClassId=0;
            string MediaScale="";
            int   smallPiexl=0;
            int   WaterMark=0;
            string   WaterImagePath="";
            int   waterPosition=0;
            string   MediaLabel="";
            string   MediaDescript="";
            string MediaTitle="";

      
            MediaClassId = int.Parse(context.Request["MediaClassId"].ToString());
            MediaScale = context.Request["MediaScale"].ToString();
            smallPiexl = int.Parse(context.Request["smallPiexl"].ToString());
            WaterMark = int.Parse(context.Request["WaterMark"].ToString());
            WaterImagePath = context.Request["WaterImagePath"].ToString();
            waterPosition = int.Parse(context.Request["waterPosition"].ToString());
            if(!string.IsNullOrEmpty(context.Request["MediaLabel"]))
                MediaLabel = context.Request["MediaLabel"].ToString();
            if (!string.IsNullOrEmpty(context.Request["MediaLabel"]))
              MediaLabel = context.Request["MediaLabel"].ToString();
            if (!string.IsNullOrEmpty(context.Request["MediaDescript"]))
               MediaDescript = context.Request["MediaDescript"].ToString();
            if (!string.IsNullOrEmpty(context.Request["MediaTitle"]))
               MediaTitle = context.Request["MediaTitle"].ToString();

            context.Response.ContentType = "text/plain";
            HttpPostedFile file = context.Request.Files["Filedata"];//接受文件


            string fileExt = file.FileName.Substring(file.FileName.LastIndexOf(".") + 1);
            if (!FileHelper.CheckFileExt(this.fileType, fileExt))
            {
                context.Response.Write ("不允许上传" + fileExt + "类型的文件");
            }
            if (this.fileSize > 0 && file.ContentLength > fileSize * 1024)
            {
                context.Response.Write ("文件超过限制的大小啦");
            }

             MediaSize = file.ContentLength / 1024;
             filename = DateTime.Now.ToString("ddHHmmssfff") + Path.GetExtension(file.FileName);
             localPath = HttpContext.Current.Request.PhysicalApplicationPath + ("upimg/" + DateTime.Now.ToString("yyyyMM") + "/");

            //判断文件夹是否存在, 不存在则创建
            if (!System.IO.Directory.Exists(localPath))
                System.IO.Directory.CreateDirectory(localPath);

            try
            {
                //本地创建上传的图片文件
                file.SaveAs(Path.Combine(localPath, filename));
            }
            catch (Exception)
            {
                 strfile = "Error";
            }

            string path = "/upimg/" + DateTime.Now.ToString("yyyyMM") + "/" + filename;

            byte[] Data = new byte[1];
            Data = FileHelper.AddImageSignPic(File.ReadAllBytes(localPath + filename), path, WaterImagePath, waterPosition, waterQuality, waterTransparency, WaterMark);

            //读取本地图片, 转成二进制, 调用服务生成图片, 返回路径另上域名
            if (FileServer.CreateFileImage(Data, path))
                strfile = "http://img.stock.com" + path;
            else
                strfile = "Error";


            
            //判断文件是否存在, 存在则删除
           // if (File.Exists(localPath + filename))
            //    File.Delete(localPath + filename);


            Media Media = new Media();
            MediaBLL MediaBLL = Factory.BusinessFactory.CreateBll<MediaBLL>();
            if (!string.IsNullOrEmpty(path))
            {
               
                    Media.MediaClassId = MediaClassId;
                    Media.MediaTitle = MediaTitle;
                    Media.UploadTime = DateTime.Now;
                    Media.UploadPath = path;
                    Media.MediaSize = MediaSize;
                    Media.MediaType = 1;
                    Media.Uploader = UserCookies.AdminName;
                    Media.MediaLabel = MediaLabel;
                    Media.MediaDescript = MediaDescript;
                    MediaBLL.Add(Media).ToString();
            }

            context.Response.Write("/upimg/" + DateTime.Now.ToString("yyyyMM") + "/" + filename);

           

        }


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }




    }
}