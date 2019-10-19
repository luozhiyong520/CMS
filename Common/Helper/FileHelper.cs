using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Web;

namespace Common
{
    public class FileHelper
    {
        /// <summary>
        /// 获取目录路径
        /// </summary>
        /// <param name="pGroup"></param>
        /// <returns></returns>
        public static string GetFileFold(string pFile)
        {
            return HttpContext.Current.Server.MapPath(pFile);
        }


        /// <summary>
        /// 获取目录的所有文件
        /// </summary>
        /// <param name="pGroup"></param>
        /// <returns></returns>
        public static System.IO.FileInfo[] GetAllFiles(string pFile)
        {
            string dir = GetFileFold(pFile);
            System.IO.DirectoryInfo info = new System.IO.DirectoryInfo(dir);
            if (info.Exists)
                return info.GetFiles();
            else
                return null;
        }

        /// <summary>
        /// 获取行业图片文件总数
        /// </summary>
        /// <param name="pGroup"></param>
        /// <returns></returns>
        public static int Count(string pFile)
        {
            string dir = GetFileFold(pFile);
            System.IO.DirectoryInfo info = new System.IO.DirectoryInfo(dir);
            if (info.Exists)
            {
                return info.GetFiles().Length;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 获取目录的文件
        /// </summary>
        /// <param name="pGroup"></param>
        /// <param name="pPageIndex"></param>
        /// <param name="pPageSize"></param>
        /// <returns>文件名列表，null表示找不到对应的路径</returns>
        public static string[] GetGroupFiles(string pFile)
        {
            string dir = GetFileFold(pFile);
            System.IO.DirectoryInfo info = new System.IO.DirectoryInfo(dir);
            if (info.Exists == false)
                return null;
            System.IO.FileInfo[] fileList = info.GetFiles();
            int iTotal = fileList.Length;
            string[] lowFileList = new string[iTotal];
            for (int i = 0; i < iTotal; i++)
            {
                lowFileList[i] = fileList[i].Name;
            }
            return lowFileList;
        }

        /// <summary>
        /// 获取web文件的内容
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string GetWebText(string filePath)
        {
            string str = "";
            string strFile = System.Web.HttpContext.Current.Server.MapPath(filePath);
            
            System.IO.StreamReader read = null;
            try
            {
                read = new System.IO.StreamReader(strFile, System.Text.Encoding.UTF8);
                str = read.ReadToEnd();
            }
            catch (Exception ee)
            {
                throw new Exception("读取邮件模板时出错", ee);
            }
            finally
            {
                if (read != null)
                    read.Close();
            }
            return str;
        }

        /// <summary>
        /// 获取winform文件内容
        /// </summary>
        /// <param name="filefullPath"></param>
        /// <returns></returns>
        public static string GetWinformText(string filefullPath)
        {
            string str = "";

            System.IO.StreamReader read = null;
            try
            {
                read = new System.IO.StreamReader(filefullPath, System.Text.Encoding.UTF8);
                str = read.ReadToEnd();
            }
            catch (Exception ee)
            {
                throw new Exception("读取文件出错", ee);
            }
            finally
            {
                if (read != null)
                    read.Close();
            }
            return str;
        }

        /// <summary>
        ///  取得某文件里的所有内容，带缓冲
        /// </summary>
        /// <param name="fileFullPath"></param>
        /// <param name="bufferSize">缓冲区大小</param>
        /// <returns></returns>
        public static string GetTextContent(string fileFullPath, int bufferSize, Encoding encode)
        {
            StreamReader reader = null;
            Stream stream = new FileStream(fileFullPath, FileMode.Open, FileAccess.Read);
            BufferedStream bufferedStream = new BufferedStream(stream, bufferSize);
            StringBuilder content = new StringBuilder();
            try
            {
                reader = new StreamReader(bufferedStream, encode);
                string line = null;
                while((line = reader.ReadLine()) != null)
                {
                    content.AppendLine(line);
                }
            }
            catch (Exception ee)
            {
                throw new Exception("读取文件时出错", ee);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
            return content.ToString();
        }

        /// <summary>
        /// 取得某文件里的所有内容<br />
        /// 缓冲区的大小为512字节，若想自行调节缓冲区大小，请使用此方法的重载版本
        /// </summary>
        /// <param name="fileFullPath"></param>
        /// <returns></returns>
        public static string GetTextContent(string fileFullPath)
        {
            return GetTextContent(fileFullPath, 512, System.Text.Encoding.GetEncoding("GBK"));
        }

        /// <summary>
        /// 取得某文件里的所有内容，默认编码为gbk
        /// </summary>
        /// <param name="fileFullPath"></param>
        /// <param name="bufferSize"></param>
        /// <returns></returns>
        public static string GetTextContent(string fileFullPath, int bufferSize)
        {
            return GetTextContent(fileFullPath, bufferSize, System.Text.Encoding.GetEncoding("GBK"));
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="postedFile"></param>
        /// <param name="savePath">存储的路径</param>
        /// <returns></returns>
        public static string UploadFile(System.Web.HttpPostedFile postedFile,string savePath)
        {
            string logFile = null;
            if (postedFile.ContentLength > 0)//有内容
            {
                System.IO.FileInfo fileInfo = new System.IO.FileInfo(postedFile.FileName);
                //本地文件名
                logFile = string.Format("{0:yyMM}/{0:ddhhmmss}{1}", DateTime.Now, fileInfo.Extension);

                string localFold = HttpContext.Current.Server.MapPath(savePath);
                string localFile = localFold + logFile;
                
                System.IO.FileInfo file = new System.IO.FileInfo(localFile);
                if (!file.Directory.Exists)
                    file.Directory.Create();
                postedFile.SaveAs(localFile);
            }
          
            return logFile;
        }

        /// <summary>
        /// 检查文件夹是否存在，如果不存在则创建之
        /// </summary>
        /// <param name="dir"></param>
        public static void CreateDirectory(string dir)
        {
            if (string.IsNullOrEmpty(dir))
                throw new ArgumentNullException("dir");
            if(!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
        }

        /// <summary>
        /// 检查文件是否存在，如果不存在则创建之
        /// </summary>
        /// <param name="path"></param>
        public static void CreateFile(string path)
        {
            System.IO.FileInfo info = new FileInfo(path);
            if (!info.Exists)
                info.Create();
        }

        /// <summary>
        /// 写入字符串内容
        /// </summary>
        /// <param name="fullPath"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static void WriteFile(string fullPath, string content)
        {
            WriteFile(fullPath, content, "utf-8");
        }

        /// <summary>
        /// 写入字符串内容
        /// </summary>
        /// <param name="fullPath"></param>
        /// <param name="content"></param>
        /// <param name="encoding"></param>
        public static void WriteFile(string fullPath, string content,string encoding)
        {
            if(string.IsNullOrEmpty(fullPath))
                throw new ArgumentNullException("fullPath");
           
            
            fullPath = fullPath.Replace("/", "\\");
            string dir = string.Empty;//文件所在的文件夹
            
            int slashIndex = fullPath.LastIndexOf("/");//"/"索引位置
            int backslashIndex = fullPath.LastIndexOf("\\");//"\"索引位置
            if (backslashIndex > 0)
            {
                dir = fullPath.Substring(0, backslashIndex);
            }
            else
            {
                if (slashIndex > 0)
                    dir = fullPath.Substring(0, slashIndex);
            }
            CreateDirectory(dir);//检查文件的路径是否存在，不存在就创建
            if (encoding == "utf-8-1") //如果选择的是UTF-8(不带签名)，那生成不带签名的utf-8文件，这样保证shtml里面的include，没有空行，不带签名的话，在浏览器里面会出现乱码
                WriteFileWithoutBOM(fullPath, content);
            else
            {
                Encoding encode = Encoding.GetEncoding(encoding);
                Stream s = new FileStream(fullPath, FileMode.Create, FileAccess.ReadWrite, FileShare.Read, 512);
                using (StreamWriter writer = new StreamWriter(s, encode))
                {
                    writer.Write(content);
                    writer.Flush();
                    writer.Close();
                }
            }
        }

        /// <summary>
        /// 生成不带BOM标志的utf-8文件(不带签名的utf-8文件）
        /// </summary>
        /// <param name="fullPath"></param>
        /// <param name="content"></param>
        public static void WriteFileWithoutBOM(string fullPath, string content)
        {
            UTF8Encoding utf8 = new UTF8Encoding(false);
            byte[] contents = utf8.GetBytes(content.ToCharArray(), 0, content.Length);

            using (FileStream outstream = new FileStream(fullPath, FileMode.Create, FileAccess.ReadWrite, FileShare.Read, 512))
            {
                outstream.Write(contents, 0, contents.Length);
                outstream.Flush();
                outstream.Close();
            }
        }


        /// <summary>
        /// 根据文件路径取文件后缀(图片格式，其他返回空值)
        /// </summary>
        /// <param name="filepath">文件路径</param>
        /// <returns>文件后缀，如bmp，jpg等</returns>
        public string GetExt(string filepath)
        {
            filepath = CheckLastIndex(filepath);
            string strExt = "";
            if (!FileExists(filepath))//判断文件是否存在
            {
                return strExt + filepath;
            }
            FileStream fs = new FileStream(filepath, FileMode.Open, FileAccess.Read);
            return GetExt(fs);
        }


        /// <summary>
        /// 根据文件路径取文件后缀(图片格式，其他返回空值)
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public string GetExt(Stream stream)
        {
            string strExt = "";
            if (stream == null)//判断文件是否存在
            {
                return strExt;
            }
            byte[] imagebytes = new byte[stream.Length];
            BinaryReader br = new BinaryReader(stream);//二进制文件读取器
            imagebytes = br.ReadBytes(2);//从当前流中将2个字节读入字节数组中
            string s = "";
            for (int i = 0; i < imagebytes.Length; i++)
            {
                s += imagebytes[i];
            }
            switch (s)
            {
                case "6677":
                    strExt = ".bmp";
                    break;
                case "7173":
                    strExt = ".gif";
                    break;
                case "7373":
                    strExt = ".tiff";
                    break;
                case "7777":
                    strExt = ".tiff";
                    break;
                case "13780":
                    strExt = ".png";
                    break;
                case "255216":
                    strExt = ".jpg";
                    break;
                case "208207":
                    strExt = ".doc";
                    break;
                case "6787":
                    strExt = ".swf";
                    break;
                default:
                    break;
            }
            return strExt;

        }


        /// <summary>
        /// 判断文件是否存在 
        /// </summary>
        /// <param name="strSourceFilePath">文件路径</param>
        /// <returns>判断结果</returns>
        public static bool FileExists(string strSourceFilePath)
        {
            try
            {
                strSourceFilePath = CheckLastIndex(strSourceFilePath);
                if (!File.Exists(strSourceFilePath))
                {
                    return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }


        /// <summary>
        /// 根据操作系统的类型的作判断
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        protected static string CheckLastIndex(string path)
        {
            if (path.LastIndexOf("\\") == path.Length - 1)
            {
                path = path.Substring(0, path.LastIndexOf("\\"));
            }
            if (path.LastIndexOf("/") == path.Length - 1)
            {
                path = path.Substring(0, path.LastIndexOf("/"));
            }
            return path;
        }

        /// <summary>
        /// 获取文件路径下面的文件夹
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string GetFileDirectory(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                return string.Empty;
            if (filePath.LastIndexOf("\\") > -1)
                return filePath.Substring(0, filePath.LastIndexOf("\\") + 1);
            else if (filePath.LastIndexOf("/") > -1)
                return filePath.Substring(0, filePath.LastIndexOf("/") + 1);
            return string.Empty;

                
        }

        /// <summary>
        /// 获取文件大小
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public static string FileSize(string filepath)
        {

            if (!FileExists(filepath))
            {
                return "";
            }
            else
            {
                FileInfo inf;
                inf = new FileInfo(filepath);
                return inf.OpenRead().Length.ToString();
            }

        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="strSourceFilePath">要删除的文件路径</param>
        /// <returns>删除结果</returns>
        public bool DeleteFile(string strSourceFilePath)
        {
            try
            {
                strSourceFilePath = CheckLastIndex(strSourceFilePath);
                if (!FileExists(strSourceFilePath))
                {
                    return true;
                }
                File.Delete(strSourceFilePath);
               
                if (FileExists(strSourceFilePath))
                {
                    return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }


        /// <summary>
        /// 检查是否为合法的上传文件
        /// </summary>
        /// <returns></returns>
        public static bool CheckFileExt(string _fileType, string _fileExt)
        {
            string[] allowExt = _fileType.Split('|');
            for (int i = 0; i < allowExt.Length; i++)
            {
                if (allowExt[i].ToLower() == _fileExt.ToLower()) { return true; }
            }
            return false;
        }

        #region 获得当前绝对路径
        /// <summary>
        /// 获得当前绝对路径
        /// </summary>
        /// <param name="strPath">指定的路径</param>
        /// <returns>绝对路径</returns>
        public static string GetMapPath(string strPath)
        {
            if (strPath.ToLower().StartsWith("http://"))
            {
                return strPath;
            }
            if (HttpContext.Current != null)
            {
                return HttpContext.Current.Server.MapPath(strPath);
            }
            else //非web程序引用
            {
                strPath = strPath.Replace("/", "\\");
                if (strPath.StartsWith("\\"))
                {
                    strPath = strPath.Substring(strPath.IndexOf('\\', 1)).TrimStart('\\');
                }
                return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, strPath);
            }
        }
        #endregion

        /// <summary>
        /// 图片水印
        /// </summary>
        /// <param name="imgPath">服务器图片相对路径</param>
        /// <param name="filename">保存文件名</param>
        /// <param name="watermarkFilename">水印文件相对路径</param>
        /// <param name="watermarkStatus">图片水印位置 0=不使用 1=左上 2=中上 3=右上 4=左中  9=右下</param>
        /// <param name="quality">附加水印图片质量,0-100</param>
        /// <param name="watermarkTransparency">水印的透明度 1--10 10为不透明</param>
        public static byte[] AddImageSignPic(byte[] imageBytes, string filename, string watermarkFilename, int watermarkStatus, int quality, int watermarkTransparency, int isWater)
        {
            if (isWater == 0)
            {
                return imageBytes;
            }
            else
            {
                //byte[] _ImageBytes = File.ReadAllBytes(imgPath);
                byte[] _ImageBytes = imageBytes;
                Image img = Image.FromStream(new System.IO.MemoryStream(_ImageBytes));
                filename = HttpContext.Current.Server.MapPath(filename);

                if (watermarkFilename.StartsWith("/") == false)
                    watermarkFilename = "/" + watermarkFilename;
                watermarkFilename = GetMapPath(watermarkFilename);
                if (!File.Exists(watermarkFilename))
                    return null;
                Graphics g = Graphics.FromImage(img);
                //设置高质量插值法
                //g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                //设置高质量,低速度呈现平滑程度
                //g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                Image watermark = new Bitmap(watermarkFilename);

                if (watermark.Height >= img.Height || watermark.Width >= img.Width)
                    return null;

                ImageAttributes imageAttributes = new ImageAttributes();
                ColorMap colorMap = new ColorMap();

                colorMap.OldColor = Color.FromArgb(255, 0, 255, 0);
                colorMap.NewColor = Color.FromArgb(0, 0, 0, 0);
                ColorMap[] remapTable = { colorMap };

                imageAttributes.SetRemapTable(remapTable, ColorAdjustType.Bitmap);

                float transparency = 0.5F;
                if (watermarkTransparency >= 1 && watermarkTransparency <= 10)
                    transparency = (watermarkTransparency / 10.0F);


                float[][] colorMatrixElements = {
												    new float[] {1.0f,  0.0f,  0.0f,  0.0f, 0.0f},
												    new float[] {0.0f,  1.0f,  0.0f,  0.0f, 0.0f},
												    new float[] {0.0f,  0.0f,  1.0f,  0.0f, 0.0f},
												    new float[] {0.0f,  0.0f,  0.0f,  transparency, 0.0f},
												    new float[] {0.0f,  0.0f,  0.0f,  0.0f, 1.0f}
											    };

                ColorMatrix colorMatrix = new ColorMatrix(colorMatrixElements);

                imageAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                int xpos = 0;
                int ypos = 0;

                switch (watermarkStatus)
                {
                    case 1:
                        xpos = (int)(img.Width * (float).01);
                        ypos = (int)(img.Height * (float).01);
                        break;
                    case 2:
                        xpos = (int)((img.Width * (float).50) - (watermark.Width / 2));
                        ypos = (int)(img.Height * (float).01);
                        break;
                    case 3:
                        xpos = (int)((img.Width * (float).99) - (watermark.Width));
                        ypos = (int)(img.Height * (float).01);
                        break;
                    case 4:
                        xpos = (int)(img.Width * (float).01);
                        ypos = (int)((img.Height * (float).50) - (watermark.Height / 2));
                        break;
                    case 5:
                        xpos = (int)((img.Width * (float).50) - (watermark.Width / 2));
                        ypos = (int)((img.Height * (float).50) - (watermark.Height / 2));
                        break;
                    case 6:
                        xpos = (int)((img.Width * (float).99) - (watermark.Width));
                        ypos = (int)((img.Height * (float).50) - (watermark.Height / 2));
                        break;
                    case 7:
                        xpos = (int)(img.Width * (float).01);
                        ypos = (int)((img.Height * (float).99) - watermark.Height);
                        break;
                    case 8:
                        xpos = (int)((img.Width * (float).50) - (watermark.Width / 2));
                        ypos = (int)((img.Height * (float).99) - watermark.Height);
                        break;
                    case 9:
                        xpos = (int)((img.Width * (float).99) - (watermark.Width));
                        ypos = (int)((img.Height * (float).99) - watermark.Height);
                        break;
                }

                g.DrawImage(watermark, new Rectangle(xpos, ypos, watermark.Width, watermark.Height), 0, 0, watermark.Width, watermark.Height, GraphicsUnit.Pixel, imageAttributes);

                ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
                ImageCodecInfo ici = null;
                foreach (ImageCodecInfo codec in codecs)
                {
                    if (codec.MimeType.IndexOf("jpeg") > -1)
                        ici = codec;
                }
                EncoderParameters encoderParams = new EncoderParameters();
                long[] qualityParam = new long[1];
                if (quality < 0 || quality > 100)
                    quality = 80;

                qualityParam[0] = quality;

                EncoderParameter encoderParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, qualityParam);
                encoderParams.Param[0] = encoderParam;

                MemoryStream ms = new MemoryStream();

                if (ici != null)
                    img.Save(ms, ici, encoderParams);
                else
                    return null;
                g.Dispose();
                img.Dispose();
                watermark.Dispose();
                imageAttributes.Dispose();
                byte[] Data = ms.ToArray();
                ms.Write(Data, 0, (int)ms.Length);
                ms.Close();
                return Data;
            }
        }

        /// <summary>
        /// 文字水印
        /// </summary>
        /// <param name="imgPath">服务器图片相对路径</param>
        /// <param name="filename">保存文件名</param>
        /// <param name="watermarkText">水印文字</param>
        /// <param name="watermarkStatus">图片水印位置 0=不使用 1=左上 2=中上 3=右上 4=左中  9=右下</param>
        /// <param name="quality">附加水印图片质量,0-100</param>
        /// <param name="fontname">字体</param>
        /// <param name="fontsize">字体大小</param>
        public static void AddImageSignText(string imgPath, string filename, string watermarkText, int watermarkStatus, int quality, string fontname, int fontsize)
        {
            byte[] _ImageBytes = File.ReadAllBytes(HttpContext.Current.Server.MapPath(imgPath));
            Image img = Image.FromStream(new System.IO.MemoryStream(_ImageBytes));
            filename = HttpContext.Current.Server.MapPath(filename);

            Graphics g = Graphics.FromImage(img);
            Font drawFont = new Font(fontname, fontsize, FontStyle.Regular, GraphicsUnit.Pixel);
            SizeF crSize;
            crSize = g.MeasureString(watermarkText, drawFont);

            float xpos = 0;
            float ypos = 0;

            switch (watermarkStatus)
            {
                case 1:
                    xpos = (float)img.Width * (float).01;
                    ypos = (float)img.Height * (float).01;
                    break;
                case 2:
                    xpos = ((float)img.Width * (float).50) - (crSize.Width / 2);
                    ypos = (float)img.Height * (float).01;
                    break;
                case 3:
                    xpos = ((float)img.Width * (float).99) - crSize.Width;
                    ypos = (float)img.Height * (float).01;
                    break;
                case 4:
                    xpos = (float)img.Width * (float).01;
                    ypos = ((float)img.Height * (float).50) - (crSize.Height / 2);
                    break;
                case 5:
                    xpos = ((float)img.Width * (float).50) - (crSize.Width / 2);
                    ypos = ((float)img.Height * (float).50) - (crSize.Height / 2);
                    break;
                case 6:
                    xpos = ((float)img.Width * (float).99) - crSize.Width;
                    ypos = ((float)img.Height * (float).50) - (crSize.Height / 2);
                    break;
                case 7:
                    xpos = (float)img.Width * (float).01;
                    ypos = ((float)img.Height * (float).99) - crSize.Height;
                    break;
                case 8:
                    xpos = ((float)img.Width * (float).50) - (crSize.Width / 2);
                    ypos = ((float)img.Height * (float).99) - crSize.Height;
                    break;
                case 9:
                    xpos = ((float)img.Width * (float).99) - crSize.Width;
                    ypos = ((float)img.Height * (float).99) - crSize.Height;
                    break;
            }

            g.DrawString(watermarkText, drawFont, new SolidBrush(Color.White), xpos + 1, ypos + 1);
            g.DrawString(watermarkText, drawFont, new SolidBrush(Color.Black), xpos, ypos);

            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            ImageCodecInfo ici = null;
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.MimeType.IndexOf("jpeg") > -1)
                    ici = codec;
            }
            EncoderParameters encoderParams = new EncoderParameters();
            long[] qualityParam = new long[1];
            if (quality < 0 || quality > 100)
                quality = 80;

            qualityParam[0] = quality;

            EncoderParameter encoderParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, qualityParam);
            encoderParams.Param[0] = encoderParam;

            if (ici != null)
                img.Save(filename, ici, encoderParams);
            else
                img.Save(filename);

            g.Dispose();
            img.Dispose();
        }

    }
}
