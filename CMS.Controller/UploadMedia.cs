using System;
using System.Web;
using System.IO;
using System.Drawing;
using System.Net;
using System.Configuration;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using CMS.HtmlService.Contract;
using CMS.BLL;
using Common;


namespace CMS.Controller
{
    public class UpLoadMedia
    {

        public int WaterPostion { get; set; }
        public string WaterPath { get; set; }
        public int ThumbNail_Width { get; set; }
        public int ThumbNail_Height { get; set; }
        public string MediaScale { get; set; }
        public static string UploadPath { get; set; }
        public static long MediaSize { get; set; }
        private readonly string fileType = "GIF|JPG|PNG|BMP|RAR|DOC|XLS|TXT"; //文件类型
        private readonly int fileSize=1024; //文件大小0为不限制   
        private readonly int waterQuality = 100; //水印图片质量
        private readonly int waterTransparency = 40; //水印图片透时度


        /// <summary>
        /// 图片上传
        /// </summary>
        /// <param name="postedFile"></param>
        /// <param name="isWater"></param>
        /// <returns></returns>
        public string fileSaveAs(HttpPostedFile postedFile, int isWater)
        {
            try
            {
                string fileExt = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf(".") + 1);
                if (!FileHelper.CheckFileExt(this.fileType, fileExt))
                {
                    return "不允许上传" + fileExt + "类型的文件";
                }
                if (this.fileSize > 0 && postedFile.ContentLength > fileSize * 1024)
                {
                    return "文件超过限制的大小啦";
                }
                UpLoadMedia.MediaSize = postedFile.ContentLength/1024;
                string toFile = string.Format("/upimg/{0:yyyyMM}/{0:ddHHmmssfff}.{1}", DateTime.Now, fileExt);
                byte[] Data = new byte[1];
                Data = FileHelper.AddImageSignPic(File.ReadAllBytes(postedFile.FileName), toFile, WaterPath, WaterPostion, waterQuality, waterTransparency, isWater);
                FileServer.CreateFileImage(Data, toFile);
                UpLoadMedia.UploadPath = toFile;
                return "操作成功";
            }
            catch
            {
                return "上传过程中发生意外错误！";
            }
        }

    }
}
