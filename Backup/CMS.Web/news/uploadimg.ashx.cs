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
namespace CMS.Web.news
{
    /// <summary>
    /// uploadimg 的摘要说明
    /// </summary>
    public class uploadimg : IHttpHandler
    {

        private readonly string fileType = "GIF|JPG|PNG|BMP"; //文件类型
        private readonly int fileSize = 0; //文件大小0为不限制 
        string localPath = "";
        string filename = "";
        string strfile = "NoFile";
        public void ProcessRequest(HttpContext context)
        {
          //  Loger.Info("-----------这里出错了，还是js出错了！");
            context.Response.ContentType = "text/plain";
            HttpPostedFile file = context.Request.Files["Filedata"];//接受文件
            string fileExt = file.FileName.Substring(file.FileName.LastIndexOf(".") + 1);
            if (!FileHelper.CheckFileExt(this.fileType, fileExt))
            {
                context.Response.Write("不允许上传" + fileExt + "类型的文件");
            }
            if (this.fileSize > 0 && file.ContentLength > fileSize * 1024)
            {
                context.Response.Write("文件超过限制的大小啦");
            }
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
            Data = FileHelper.AddImageSignPic(File.ReadAllBytes(localPath + filename), path, "", 0, 100, 100, 0);

            //读取本地图片, 转成二进制, 调用服务生成图片, 返回路径另上域名
            if (FileServer.CreateFileImage(Data, path))
                strfile = "http://img.upchina.com" + path;
            else
                strfile = "Error";
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