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
        /// ��ȡĿ¼·��
        /// </summary>
        /// <param name="pGroup"></param>
        /// <returns></returns>
        public static string GetFileFold(string pFile)
        {
            return HttpContext.Current.Server.MapPath(pFile);
        }


        /// <summary>
        /// ��ȡĿ¼�������ļ�
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
        /// ��ȡ��ҵͼƬ�ļ�����
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
        /// ��ȡĿ¼���ļ�
        /// </summary>
        /// <param name="pGroup"></param>
        /// <param name="pPageIndex"></param>
        /// <param name="pPageSize"></param>
        /// <returns>�ļ����б�null��ʾ�Ҳ�����Ӧ��·��</returns>
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
        /// ��ȡweb�ļ�������
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
                throw new Exception("��ȡ�ʼ�ģ��ʱ����", ee);
            }
            finally
            {
                if (read != null)
                    read.Close();
            }
            return str;
        }

        /// <summary>
        /// ��ȡwinform�ļ�����
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
                throw new Exception("��ȡ�ļ�����", ee);
            }
            finally
            {
                if (read != null)
                    read.Close();
            }
            return str;
        }

        /// <summary>
        ///  ȡ��ĳ�ļ�����������ݣ�������
        /// </summary>
        /// <param name="fileFullPath"></param>
        /// <param name="bufferSize">��������С</param>
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
                throw new Exception("��ȡ�ļ�ʱ����", ee);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
            return content.ToString();
        }

        /// <summary>
        /// ȡ��ĳ�ļ������������<br />
        /// �������Ĵ�СΪ512�ֽڣ��������е��ڻ�������С����ʹ�ô˷��������ذ汾
        /// </summary>
        /// <param name="fileFullPath"></param>
        /// <returns></returns>
        public static string GetTextContent(string fileFullPath)
        {
            return GetTextContent(fileFullPath, 512, System.Text.Encoding.GetEncoding("GBK"));
        }

        /// <summary>
        /// ȡ��ĳ�ļ�����������ݣ�Ĭ�ϱ���Ϊgbk
        /// </summary>
        /// <param name="fileFullPath"></param>
        /// <param name="bufferSize"></param>
        /// <returns></returns>
        public static string GetTextContent(string fileFullPath, int bufferSize)
        {
            return GetTextContent(fileFullPath, bufferSize, System.Text.Encoding.GetEncoding("GBK"));
        }

        /// <summary>
        /// �ϴ��ļ�
        /// </summary>
        /// <param name="postedFile"></param>
        /// <param name="savePath">�洢��·��</param>
        /// <returns></returns>
        public static string UploadFile(System.Web.HttpPostedFile postedFile,string savePath)
        {
            string logFile = null;
            if (postedFile.ContentLength > 0)//������
            {
                System.IO.FileInfo fileInfo = new System.IO.FileInfo(postedFile.FileName);
                //�����ļ���
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
        /// ����ļ����Ƿ���ڣ�����������򴴽�֮
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
        /// ����ļ��Ƿ���ڣ�����������򴴽�֮
        /// </summary>
        /// <param name="path"></param>
        public static void CreateFile(string path)
        {
            System.IO.FileInfo info = new FileInfo(path);
            if (!info.Exists)
                info.Create();
        }

        /// <summary>
        /// д���ַ�������
        /// </summary>
        /// <param name="fullPath"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static void WriteFile(string fullPath, string content)
        {
            WriteFile(fullPath, content, "utf-8");
        }

        /// <summary>
        /// д���ַ�������
        /// </summary>
        /// <param name="fullPath"></param>
        /// <param name="content"></param>
        /// <param name="encoding"></param>
        public static void WriteFile(string fullPath, string content,string encoding)
        {
            if(string.IsNullOrEmpty(fullPath))
                throw new ArgumentNullException("fullPath");
           
            
            fullPath = fullPath.Replace("/", "\\");
            string dir = string.Empty;//�ļ����ڵ��ļ���
            
            int slashIndex = fullPath.LastIndexOf("/");//"/"����λ��
            int backslashIndex = fullPath.LastIndexOf("\\");//"\"����λ��
            if (backslashIndex > 0)
            {
                dir = fullPath.Substring(0, backslashIndex);
            }
            else
            {
                if (slashIndex > 0)
                    dir = fullPath.Substring(0, slashIndex);
            }
            CreateDirectory(dir);//����ļ���·���Ƿ���ڣ������ھʹ���
            if (encoding == "utf-8-1") //���ѡ�����UTF-8(����ǩ��)�������ɲ���ǩ����utf-8�ļ���������֤shtml�����include��û�п��У�����ǩ���Ļ����������������������
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
        /// ���ɲ���BOM��־��utf-8�ļ�(����ǩ����utf-8�ļ���
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
        /// �����ļ�·��ȡ�ļ���׺(ͼƬ��ʽ���������ؿ�ֵ)
        /// </summary>
        /// <param name="filepath">�ļ�·��</param>
        /// <returns>�ļ���׺����bmp��jpg��</returns>
        public string GetExt(string filepath)
        {
            filepath = CheckLastIndex(filepath);
            string strExt = "";
            if (!FileExists(filepath))//�ж��ļ��Ƿ����
            {
                return strExt + filepath;
            }
            FileStream fs = new FileStream(filepath, FileMode.Open, FileAccess.Read);
            return GetExt(fs);
        }


        /// <summary>
        /// �����ļ�·��ȡ�ļ���׺(ͼƬ��ʽ���������ؿ�ֵ)
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public string GetExt(Stream stream)
        {
            string strExt = "";
            if (stream == null)//�ж��ļ��Ƿ����
            {
                return strExt;
            }
            byte[] imagebytes = new byte[stream.Length];
            BinaryReader br = new BinaryReader(stream);//�������ļ���ȡ��
            imagebytes = br.ReadBytes(2);//�ӵ�ǰ���н�2���ֽڶ����ֽ�������
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
        /// �ж��ļ��Ƿ���� 
        /// </summary>
        /// <param name="strSourceFilePath">�ļ�·��</param>
        /// <returns>�жϽ��</returns>
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
        /// ���ݲ���ϵͳ�����͵����ж�
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
        /// ��ȡ�ļ�·��������ļ���
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
        /// ��ȡ�ļ���С
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
        /// ɾ���ļ�
        /// </summary>
        /// <param name="strSourceFilePath">Ҫɾ�����ļ�·��</param>
        /// <returns>ɾ�����</returns>
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
        /// ����Ƿ�Ϊ�Ϸ����ϴ��ļ�
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

        #region ��õ�ǰ����·��
        /// <summary>
        /// ��õ�ǰ����·��
        /// </summary>
        /// <param name="strPath">ָ����·��</param>
        /// <returns>����·��</returns>
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
            else //��web��������
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
        /// ͼƬˮӡ
        /// </summary>
        /// <param name="imgPath">������ͼƬ���·��</param>
        /// <param name="filename">�����ļ���</param>
        /// <param name="watermarkFilename">ˮӡ�ļ����·��</param>
        /// <param name="watermarkStatus">ͼƬˮӡλ�� 0=��ʹ�� 1=���� 2=���� 3=���� 4=����  9=����</param>
        /// <param name="quality">����ˮӡͼƬ����,0-100</param>
        /// <param name="watermarkTransparency">ˮӡ��͸���� 1--10 10Ϊ��͸��</param>
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
                //���ø�������ֵ��
                //g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                //���ø�����,���ٶȳ���ƽ���̶�
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
        /// ����ˮӡ
        /// </summary>
        /// <param name="imgPath">������ͼƬ���·��</param>
        /// <param name="filename">�����ļ���</param>
        /// <param name="watermarkText">ˮӡ����</param>
        /// <param name="watermarkStatus">ͼƬˮӡλ�� 0=��ʹ�� 1=���� 2=���� 3=���� 4=����  9=����</param>
        /// <param name="quality">����ˮӡͼƬ����,0-100</param>
        /// <param name="fontname">����</param>
        /// <param name="fontsize">�����С</param>
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
