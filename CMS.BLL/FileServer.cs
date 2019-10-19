using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using CMS.HtmlService.Contract;
using Common;
using System.Drawing;

namespace CMS.BLL
{
    public class FileServer
    {
        /// <summary>
        /// 文件传输服务器
        /// </summary>
        /// <param name="btImage"></param>
        /// <param name="readPath"></param>
        /// <returns></returns>
        public static bool CreateFileImage(byte[] image, string readPath)
        {
            var factory = new ChannelFactory<IFileReceive>("FileService");
            var channel = factory.CreateChannel();
            bool flag;
            try
            {
                flag = channel.CreateFileImage(image, readPath);
                factory.Close();
                Loger.Info("文件上传成功");
                return flag;
            }
            catch (Exception ex)
            {
                Loger.Error(ex);
                factory.Abort();
                return false;
            }
        }

        /// <summary>
        /// 删除上传的文件
        /// </summary>
        /// <param name="readPath"></param>
        /// <returns></returns>
        public static bool DeleteFile(string readPath, string deleteType)
        {
            var factory = new ChannelFactory<IFileReceive>("FileService");
            var channel = factory.CreateChannel();
            bool flag;
            try
            {
                if (deleteType == "SinggleDel")
                    flag = channel.DeleteFile(readPath);
                else
                    flag = channel.DeleteFiles(readPath);
                factory.Close();
                Loger.Info("文件删除成功");
                return flag;
            }
            catch (Exception ex)
            {
                Loger.Error(ex);
                factory.Abort();
                return false;
            }
        }
    }
}
