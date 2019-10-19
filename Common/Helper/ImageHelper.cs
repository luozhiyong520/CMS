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
    public class ImageHelper
    {


        public static string WaterImagePath { get; set; }
        /// <summary>
        /// ��ȡͼƬ�еĸ�֡
        /// </summary>
        /// <param name="pPath">ͼƬ·��</param>
        /// <param name="pSavePath">����·��</param>
        public void GetFrames(string pPath, string pSavedPath)
        {
            Image gif = Image.FromFile(pPath);
            FrameDimension fd = new FrameDimension(gif.FrameDimensionsList[0]);

            //��ȡ֡��(gifͼƬ���ܰ�����֡��������ʽͼƬһ���һ֡)
            int count = gif.GetFrameCount(fd);

            //��Jpeg��ʽ�����֡
            for (int i = 0; i < count; i++)
            {
                gif.SelectActiveFrame(fd, i);
                gif.Save(pSavedPath + "\\frame_" + i + ".jpg", ImageFormat.Jpeg);
            }
        }

        /**/
        /// <summary>
        /// ��ȡͼƬ����ͼ
        /// </summary>
        /// <param name="pPath">ͼƬ·��</param>
        /// <param name="pSavePath">����·��</param>
        /// <param name="pWidth">����ͼ���</param>
        /// <param name="pHeight">����ͼ�߶�</param>
        /// <param name="pFormat">�����ʽ��ͨ��������jpeg</param>
        public void GetSmaller(string pPath, string pSavedPath,string pSaveName, int pWidth, int pHeight)
        {
            string fileSaveUrl = pSavedPath + "\\" + pSaveName;
            using (FileStream fs = new FileStream(pPath, FileMode.Open))
            {
                MakeSmallImg(fs, fileSaveUrl, pWidth, pHeight);
            }
        }


        //��ģ�������������ͼ�������ķ�ʽ��ȡԴ�ļ���  
        //��������ͼ����  
        //˳�������Դͼ�ļ���������ͼ��ŵ�ַ��ģ���ģ���  
        //ע������ͼ��С������ģ��������  
        public static void MakeSmallImg(System.IO.Stream fromFileStream, string fileSaveUrl, System.Double templateWidth, System.Double templateHeight)
        {
            //���ļ�ȡ��ͼƬ���󣬲�ʹ������Ƕ�����ɫ������Ϣ  
            System.Drawing.Image myImage = System.Drawing.Image.FromStream(fromFileStream, true);

            //����ͼ����  
            System.Double newWidth = myImage.Width, newHeight = myImage.Height;
            //�����ģ��ĺ�ͼ  
            if (myImage.Width > myImage.Height || myImage.Width == myImage.Height)
            {
                if (myImage.Width > templateWidth)
                {
                    //��ģ�棬�߰���������  
                    newWidth = templateWidth;
                    newHeight = myImage.Height * (newWidth / myImage.Width);
                }
            }
            //�ߴ���ģ�����ͼ
            else
            {
                if (myImage.Height > templateHeight)
                {
                    //�߰�ģ�棬����������  
                    newHeight = templateHeight;
                    newWidth = myImage.Width * (newHeight / myImage.Height);
                }
            }

            //ȡ��ͼƬ��С  
            System.Drawing.Size mySize = new Size((int)newWidth, (int)newHeight);
            //�½�һ��bmpͼƬ  
            System.Drawing.Image bitmap = new System.Drawing.Bitmap(mySize.Width, mySize.Height);
            //�½�һ������  
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);
            //���ø�������ֵ��  
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            //���ø�����,���ٶȳ���ƽ���̶�  
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            //���һ�»���  
            g.Clear(Color.White);
            //��ָ��λ�û�ͼ  
            g.DrawImage(myImage, new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height),
            new System.Drawing.Rectangle(0, 0, myImage.Width, myImage.Height),
            System.Drawing.GraphicsUnit.Pixel);

            ///����ˮӡ  
            //System.Drawing.Graphics G = System.Drawing.Graphics.FromImage(bitmap);
            //System.Drawing.Font f = new Font("Lucida Grande", 6);
            //System.Drawing.Brush b = new SolidBrush(Color.Gray);
            //G.DrawString("wlstock.com", f, b, 0, 0);
            //G.Dispose();
            
            // ͼƬˮӡ  
            if (!string.IsNullOrEmpty(WaterImagePath))
            {
                System.Drawing.Image copyImage = System.Drawing.Image.FromFile(System.Web.HttpContext.Current.Server.MapPath(WaterImagePath));  
                Graphics   a   =   Graphics.FromImage(bitmap);  
                a.DrawImage(copyImage,   new   Rectangle(bitmap.Width-copyImage.Width,bitmap.Height-copyImage.Height,copyImage.Width,   copyImage.Height),0,0,   copyImage.Width,   copyImage.Height,   GraphicsUnit.Pixel);

                copyImage.Dispose();
                a.Dispose();
                copyImage.Dispose();  
            }

            //��������ͼ  
            if (File.Exists(fileSaveUrl))
            {
                File.SetAttributes(fileSaveUrl, FileAttributes.Normal);
                File.Delete(fileSaveUrl);
            }
          
            bitmap.Save(fileSaveUrl, System.Drawing.Imaging.ImageFormat.Jpeg);

            g.Dispose();
            myImage.Dispose();
            bitmap.Dispose();
        }



        /**/
        /// <summary>
        /// ��ȡͼƬָ������
        /// </summary>
        /// <param name="pPath">ͼƬ·��</param>
        /// <param name="pSavePath">����·��</param>
        /// <param name="pPartStartPointX">Ŀ��ͼƬ��ʼ���ƴ�������Xֵ(ͨ��Ϊ)</param>
        /// <param name="pPartStartPointY">Ŀ��ͼƬ��ʼ���ƴ�������Yֵ(ͨ��Ϊ)</param>
        /// <param name="pPartWidth">Ŀ��ͼƬ�Ŀ��</param>
        /// <param name="pPartHeight">Ŀ��ͼƬ�ĸ߶�</param>
        /// <param name="pOrigStartPointX">ԭʼͼƬ��ʼ��ȡ��������Xֵ</param>
        /// <param name="pOrigStartPointY">ԭʼͼƬ��ʼ��ȡ��������Yֵ</param>
        /// <param name="pFormat">�����ʽ��ͨ��������jpeg</param>
        public void GetPart(string pPath, string pSavedPath, string pSaveName, int pPartStartPointX, int pPartStartPointY, int pPartWidth, int pPartHeight, int pOrigStartPointX, int pOrigStartPointY)
        {
            string normalJpgPath = pSavedPath + "\\" + pSaveName;

            using (Image originalImg = Image.FromFile(pPath))
            {
                Bitmap partImg = new Bitmap(pPartWidth, pPartHeight);
                Graphics graphics = Graphics.FromImage(partImg);
                Rectangle destRect = new Rectangle(new Point(pPartStartPointX, pPartStartPointY), new Size(pPartWidth, pPartHeight));//Ŀ��λ��
                Rectangle origRect = new Rectangle(new Point(pOrigStartPointX, pOrigStartPointY), new Size(pPartWidth, pPartHeight));//ԭͼλ�ã�Ĭ�ϴ�ԭͼ�н�ȡ��ͼƬ��С����Ŀ��ͼƬ�Ĵ�С��


                ///����ˮӡ  
                System.Drawing.Graphics G = System.Drawing.Graphics.FromImage(partImg);
                //System.Drawing.Font f = new Font("Lucida Grande", 6);
                //System.Drawing.Brush b = new SolidBrush(Color.Gray);
                G.Clear(Color.White);
                graphics.DrawImage(originalImg, destRect, origRect, GraphicsUnit.Pixel);
                //G.DrawString("wlstock.com", f, b, 0, 0);
                G.Dispose();

                originalImg.Dispose();
                if (File.Exists(normalJpgPath))
                {
                    File.SetAttributes(normalJpgPath, FileAttributes.Normal);
                    File.Delete(normalJpgPath);
                }
                partImg.Save(normalJpgPath, ImageFormat.Jpeg);
            }
        }
        /**/
        /// <summary>
        /// ��ȡ���������ŵ�ͼƬָ������
        /// </summary>
        /// <param name="pPath">ͼƬ·��</param>
        /// <param name="pSavePath">����·��</param>
        /// <param name="pPartStartPointX">Ŀ��ͼƬ��ʼ���ƴ�������Xֵ(ͨ��Ϊ)</param>
        /// <param name="pPartStartPointY">Ŀ��ͼƬ��ʼ���ƴ�������Yֵ(ͨ��Ϊ)</param>
        /// <param name="pPartWidth">Ŀ��ͼƬ�Ŀ��</param>
        /// <param name="pPartHeight">Ŀ��ͼƬ�ĸ߶�</param>
        /// <param name="pOrigStartPointX">ԭʼͼƬ��ʼ��ȡ��������Xֵ</param>
        /// <param name="pOrigStartPointY">ԭʼͼƬ��ʼ��ȡ��������Yֵ</param>
        /// <param name="imageWidth">���ź�Ŀ��</param>
        /// <param name="imageHeight">���ź�ĸ߶�</param>
        public void GetPart(string pPath, string pSavedPath,string pSaveName, int pPartStartPointX, int pPartStartPointY, int pPartWidth, int pPartHeight, int pOrigStartPointX, int pOrigStartPointY, int imageWidth, int imageHeight)
        {
            string normalJpgPath = pSavedPath + "\\" + pSaveName;
            using (Image originalImg = Image.FromFile(pPath))
            {
                if (originalImg.Width == imageWidth && originalImg.Height == imageHeight)
                {
                    GetPart(pPath, pSavedPath, pSaveName, pPartStartPointX, pPartStartPointY, pPartWidth, pPartHeight, pOrigStartPointX, pOrigStartPointY);
                    return;
                }

                Image.GetThumbnailImageAbort callback = new Image.GetThumbnailImageAbort(ThumbnailCallback);
                Image zoomImg = originalImg.GetThumbnailImage(imageWidth, imageHeight, callback, IntPtr.Zero);//����
                Bitmap partImg = new Bitmap(pPartWidth, pPartHeight);

                Graphics graphics = Graphics.FromImage(partImg);
                Rectangle destRect = new Rectangle(new Point(pPartStartPointX, pPartStartPointY), new Size(pPartWidth, pPartHeight));//Ŀ��λ��
                Rectangle origRect = new Rectangle(new Point(pOrigStartPointX, pOrigStartPointY), new Size(pPartWidth, pPartHeight));//ԭͼλ�ã�Ĭ�ϴ�ԭͼ�н�ȡ��ͼƬ��С����Ŀ��ͼƬ�Ĵ�С��

                ///����ˮӡ  
                System.Drawing.Graphics G = System.Drawing.Graphics.FromImage(partImg);
                //System.Drawing.Font f = new Font("Lucida Grande", 6);
                //System.Drawing.Brush b = new SolidBrush(Color.Gray);
                G.Clear(Color.White);

                graphics.DrawImage(zoomImg, destRect, origRect, GraphicsUnit.Pixel);
                //G.DrawString("wlstock.com", f, b, 0, 0);
                G.Dispose();

                originalImg.Dispose();
                if (File.Exists(normalJpgPath))
                {
                    File.SetAttributes(normalJpgPath, FileAttributes.Normal);
                    File.Delete(normalJpgPath);
                }
                partImg.Save(normalJpgPath, ImageFormat.Jpeg);
            }
        }

        /// <summary>
        /// ���ͼ��߿���Ϣ
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public ImageInformation GetImageInfo(string path)
        {
            using (Image image = Image.FromFile(path))
            {
                ImageInformation imageInfo = new ImageInformation();
                imageInfo.Width = image.Width;
                imageInfo.Height = image.Height;
                return imageInfo;
            }
        }
        public bool ThumbnailCallback()
        {
            return false;
        }


        /// <summary>
        /// �ϴ�ͷ��
        /// </summary>
        /// <param name="postedFile"></param>
        /// <param name="fileName">�ļ���</param>
        /// <param name="savePath">�洢��·��</param>
        /// <returns></returns>
        public static string UploadImage(System.Web.HttpPostedFile postedFile, string fileName, string savePath)
        {
            string localFile = "";
            if (postedFile.ContentLength > 0)//������
            {
                try
                {
                    System.IO.FileInfo fileInfo = new System.IO.FileInfo(postedFile.FileName);
                    string localFold = savePath;
                    localFile = localFold + fileName;
                    System.IO.FileInfo file = new System.IO.FileInfo(localFile);
                    if (!file.Directory.Exists)
                        file.Directory.Create();
                    postedFile.SaveAs(localFile);
                }
                catch
                {
                    return localFile;
                }
            }
            return localFile;
        }
    }

    public struct ImageInformation
    {
        public int Width { get; set; }

        public int Height { get; set; }


    }


}
