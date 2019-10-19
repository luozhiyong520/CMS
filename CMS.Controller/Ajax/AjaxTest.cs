using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMVC;
using System.Web;
using System.IO;
using CMS.BLL;
using System.Web.Script.Serialization;

namespace CMS.Controller.Ajax
{
    public class AjaxTest
    {
        [Action]
        public string Upload()
        {
            string localPath = "";
            string filename = "";
            string strfile = "NoFile";

            foreach (string upload in HttpContext.Current.Request.Files)
            {
                HttpPostedFile file = HttpContext.Current.Request.Files[upload];
                if (file == null && file.ContentLength <= 0) continue;
                if (IsAllowedExtension(file))
                {
                    filename = DateTime.Now.ToString("ddHHmmssfff") + Path.GetExtension(file.FileName);
                    localPath = HttpContext.Current.Request.PhysicalApplicationPath + ("upload/images/");

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

                    //读取本地图片, 转成二进制, 调用服务生成图片, 返回路径另上域名
                    if (FileServer.CreateFileImage(ReadFile(localPath + filename), path))
                        strfile = localPath + filename + ",http://img.stock.com" + path;
                    else
                        strfile = "Error";

                    //File.WriteAllBytes("E:/Project/CMS/code/branches/v1.0/CMS.Web/upload/images/aab.jpg", ReadFile(localPath + filename));

                    //判断文件是否存在, 存在则删除
                    //if (File.Exists(localPath + filename))
                    //    File.Delete(localPath + filename);

                    //if (FileServer.CreateFileImage(File.ReadAllBytes(file.FileName), path))
                    //    if (FileServer.CreateFileImage(StreamToBytes(file.InputStream), path))
                    //        strfile = "http://img.stock.com" + path;
                    //    else
                    //        strfile = "Error";

                    //int intlenght = file.ContentLength;
                    //Stream sr = file.InputStream;
                    //StreamReader srr = new StreamReader(sr);
                    //string str = srr.ReadToEnd();
                    

                    //图片保存为二进制字节
                    //byte[] arrBt = new byte[intlenght];

                    //Stream strm = file.InputStream;
                    //strm.Read(arrBt, 0, intlenght);

                    //File.WriteAllText(path + filename, str, Encoding.UTF8);

                    //FileStream fs = new FileStream(path + filename, FileMode.Create, FileAccess.Write);

                    //将内容写入文件流
                    //fs.Write(StreamToBytes(file.InputStream), 0, file.ContentLength);
                    
                    //必须关闭文件流，否则得到的文本什么内容都没有
                    //fs.Close();//必须关闭
                    
                }
                else
                {
                    strfile = "格式不正确！";
                }

            }
            return strfile;
        }

        /// <summary> 
        /// 将 Stream 转成 byte[] 
        /// </summary> 
        public byte[] StreamToBytes(Stream stream)
        {
            byte[] bytes = new byte[stream.Length];
            //stream.Read(bytes, 0, bytes.Length);

            // 设置当前流的位置为流的开始 
            //stream.Seek(0, SeekOrigin.Begin);
            return bytes;
        }

        /// <summary>
        /// 读文件返回byte[]
        /// </summary>
        /// <param name="fileName">完整带文件名路径</param>
        /// <returns></returns>
        private byte[] ReadFile(string fileName)
        {
            FileStream pFileStream = null;
            byte[] pReadByte = new byte[0];

            try
            {
                pFileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                BinaryReader r = new BinaryReader(pFileStream);
                r.BaseStream.Seek(0, SeekOrigin.Begin);    //将文件指针设置到文件开
                pReadByte = r.ReadBytes((int)r.BaseStream.Length);
                return pReadByte;
            }
            catch
            {
                return pReadByte;
            }
            finally
            {
                if (pFileStream != null)
                    pFileStream.Close();
            }
        }

        /// <summary>
        /// 验证文件类型
        /// </summary>
        /// <param name="file">上传上来的文件</param>
        /// <returns></returns>
        public Boolean IsAllowedExtension(HttpPostedFile file)
        {
            int fileLen = file.ContentLength;
            byte[] imgArray = new byte[fileLen];
            file.InputStream.Read(imgArray, 0, fileLen);
            MemoryStream ms = new MemoryStream(imgArray);
            System.IO.BinaryReader br = new System.IO.BinaryReader(ms);
            string fileclass = "";
            byte buffer;
            try
            {
                buffer = br.ReadByte();
                fileclass = buffer.ToString();
                buffer = br.ReadByte();
                fileclass += buffer.ToString();
            }
            catch
            {
            }
            finally
            {
                br.Close();
                ms.Close();
            }
            //255216是jpg;7173是gif;6677是bmp,13780是png;7790是exe,8297是rar,6787是swf
            if (fileclass == "255216" || fileclass == "7173" || fileclass == "6677" || fileclass == "13780" || fileclass == "6787")
                return true;
            else
                return false;
        }
        [Action]
        public string aa()
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string bb = "[{'productId':3001,'tel':'0000111222','url':'http://www.pinyifu.com','email':'','company':'','agenturl':','agentschool':[{'title':'今天很累','description':'','pageurl':'','roomid':0}]}]";
            //return jss.Serialize(bb);
            return bb;
        }
    }
}
