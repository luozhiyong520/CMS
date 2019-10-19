using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using CMS.HtmlService.Contract;
using System.Configuration;
using System.ServiceModel;
using Common;

namespace CMS.HtmlService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, Namespace = "CMS.HtmlService")]
    public class FileReceive : IFileReceive
    {
        string path = AppConfig.FileSaveBasePath;

        /// <summary>
        /// 创建图片文件
        /// </summary>
        /// <param name="btImage">图片二进制字符串</param>
        /// <param name="imageName">文件相对路径+文件名</param>
        /// <returns></returns>
        public bool CreateFileImage(byte[] image, string readPath)
        {
            if (image == null)
                return false;
            try
            {
                bool flag = true;
                string errorinfo = string.Empty;
                isCreateDir(path + readPath);
                System.IO.File.WriteAllBytes(path + readPath, image);
                Loger.Info(string.Format("方法名：{0}，创建图片文件成功，文件路径:{1}", "CreateFileImage", path + readPath));
                //ftp上传操作
                flag = FtpUpload(path + readPath, readPath, AppConfig.FtpFileBasePath, out errorinfo);  
                return flag;
            }
            catch (Exception ex)
            {
                Loger.Info(string.Format("方法名：{0}，创建图片文件失败!", "CreateFileImage"));
                Loger.Error(ex);
                return false;
            }
        }

        public bool DeleteFile(string readPath)
        {
            try
            {
                File.Delete(path + readPath);
                Loger.Info(string.Format("方法名：{0}，删除文件成功，文件路径:{1}", "DeleteFile", path + readPath));
                return true;
            }
            catch (Exception ex)
            {
                Loger.Error(ex, string.Format("方法名：{0}，删除文件失败!", "DeleteFile"));
                return false;
            }
        }

        public bool DeleteFiles(string readPaths)
        {
            try
            {
                string[] pat = readPaths.Split(',');
                StringBuilder sb = new StringBuilder();
                foreach (var item in pat)
                {
                    File.Delete(path + item);
                    sb.Append(path + item + "\n");
                }
                Loger.Info(string.Format("方法名：{0}，批量删除文件，删除成功，文件路径:{1}", "DeleteFiles", sb.ToString()));
                return true;
            }
            catch (Exception ex)
            {
                Loger.Error(ex, string.Format("方法名：{0}，批量删除文件，删除失败!", "DeleteFiles"));
                return false;
            }
        }

        ///// <summary>
        ///// 创建图片文件
        ///// </summary>
        ///// <param name="bitImage">图片位图对像</param>
        ///// <param name="imageName">文件相对路径+文件名</param>
        ///// <returns></returns>
        //public bool CreateFileImageII(Bitmap image, string readPath)
        //{
        //    if (image == null)
        //        return false;
        //    try
        //    {
        //        isCreateDir(path + readPath);
        //        Image pic = Image.FromHbitmap(image.GetHbitmap());
        //        pic.Save(path + @"" + readPath);
        //        Loger.Info(string.Format("方法名：{0}，创建图片文件，生成文件成功，文件路径:{1}", "CreateFileImageII", path + readPath));
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Loger.Info(string.Format("方法名：{0}，创建图片文件，生成文件失败!", "CreateFileImageII"));
        //        Loger.Debug(ex);
        //        return false;
        //    }
        //}

        //是否存在文件夹, 不存在则创建
        private void isCreateDir(string fullPath)
        {
            //匹配文件名
            System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex("[^/\\\\]+$");
            string dirPath = fullPath.Replace(r.Match(fullPath).Value, "");

            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);
        }

        public bool FtpUpload(string fileLocalPath, string filePath, string fileFtpPath, out string errorInfo)
        {
            bool flag = true;
            errorInfo = string.Empty;
            if (AppConfig.FtpUpload)
            {
                FtpHelper ftp = new FtpHelper();
                ftp.FtpUpDown(AppConfig.FtpServerIP, AppConfig.FtpUserId, AppConfig.FtpPassword);
                string directory = FileHelper.GetFileDirectory(filePath);//获取该文件的文件路径结构
                if (!string.IsNullOrEmpty(directory) && directory != "/" && directory != "\\")
                {
                    //ftp.MakeDir(fileFtpPath + directory);
                    directory = directory.TrimStart('/').TrimEnd('/');
                    string[] dirs = directory.Split('/');
                    string ftpCreateDir = fileFtpPath;
                    //创建ftp目录，目前只能一级一级创建
                    for (int i = 0; i < dirs.Length; i++)
                    {
                        ftpCreateDir += "/" + dirs[i] + "/";
                        ftp.MakeDir(ftpCreateDir);
                    }
                    
                }
                flag = ftp.Upload(fileLocalPath, fileFtpPath + filePath, out errorInfo);
                if (flag)
                    Loger.Info(string.Format("上传ftp文件成功，文件路径:{0},{1}", fileFtpPath + filePath, errorInfo));
                else
                    Loger.Warn(string.Format("上传ftp文件失败，文件路径:{0},{1}", fileFtpPath + filePath, errorInfo));
            }
            return flag;
        }

        //private void makeFtpDir(string[] dirStrs, int dirStep, FtpHelper ftp, string fileFtpPath)
        //{
        //    if (dirStrs == null || dirStrs.Length == 0)
        //    {
        //        Loger.Warn("ftp不能创建空文件夹");
        //        return;
        //    }
        //    string ftpCreateDir;
        //    if (dirStep == 1)
        //    {
        //        ftpCreateDir = fileFtpPath + "/" + dirStrs[0] + "/";
        //        ftp.MakeDir(ftpCreateDir);
        //        Loger.Info("创建目录:" + ftpCreateDir);
        //    }
        //    else
        //    {
        //        makeFtpDir(dirStrs, dirStep - 1, ftp, fileFtpPath);
        //    }
        //}
    }
}
