using System;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using System.Text.RegularExpressions;

namespace Common
{
    public class ImageMake
    {
        #region 属性 枚举
        private int _err = 0;//错误类型  
        FileHelper fso = new FileHelper();
        /// <summary>
        /// 错误类型
        /// </summary>
        public int Err
        {
            get { return _err; }
            private set
            {
                _err = value;
            }
        }
        Bitmap _originBmp;
        /// <summary>
        /// 位图
        /// </summary>
        public Bitmap OriginBmp
        {
            get { return _originBmp; }
            set { _originBmp = value; }
        }

        public int WaterPostion { get; set; }
        public int WaterMark { get; set; }
        public string WaterPath { get; set; }
        public int ThumbNail_Width { get; set; }
        public int ThumbNail_Height { get; set; }
        public string MediaScale { get; set; }
   

        /// <summary>
        /// 色相类型
        /// </summary>
        public enum ColorFilterTypes
        {
            /// <summary>
            /// 
            /// </summary>
            Red,
            /// <summary>
            /// 
            /// </summary>
            Green,
            /// <summary>
            /// 
            /// </summary>
            Blue
        };

        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="oldImagePath">旧图片路径</param>
        public ImageMake(string oldImagePath)
        {
            try
            {
                using (FileStream stream = File.Open(oldImagePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    OriginBmp = new Bitmap(Image.FromStream(stream));
                }

            }
            catch
            {
                Dispose();
            }
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="oldImage">旧图片</param>
        public ImageMake(Image oldImage)
        {
            try
            {
                OriginBmp = new Bitmap(oldImage);
            }
            catch
            {
                Dispose();
            }

        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="oldImageStream">旧图片流</param>
        public ImageMake(FileStream oldImageStream)
        {
            try
            {
                OriginBmp = new Bitmap(oldImageStream);
            }
            catch
            {
                Dispose();
            }

        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="oldImageStream">旧图片流</param>
        public ImageMake(Stream oldImageStream)
        {
            try
            {
                OriginBmp = new Bitmap(oldImageStream);
            }
            catch
            {
                Dispose();
            }
        }
        #endregion

        #region 保存释放资源
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="saveFilePath">保存路径</param>
        public bool Save(string saveFilePath)
        {

            try
            {
                if (System.IO.File.Exists(saveFilePath))
                    System.IO.File.Delete(saveFilePath);

                OriginBmp.Save(saveFilePath);
            }
            catch
            {
                Dispose();
                return false;
            }
            if (!FileHelper.FileExists(saveFilePath))
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="stream">保存路径</param>
        public void Save(Stream stream)
        {
            try
            {
                OriginBmp.Save(stream, ImageFormat.Jpeg);
            }
            catch
            {
                Dispose();
            }
        }
        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            OriginBmp.Dispose();
        }
        #endregion

        #region 图片处理
        /// <summary>
        /// 缩略图
        /// </summary>
        /// <param name="newWidth">宽</param>
        /// <param name="newHeight">高</param>
        /// <param name="isFull">是否填充</param>
        public bool Resize(int newWidth, int newHeight, bool isFull)
        {
            try
            {
                Bitmap resizedBmp = new Bitmap(newWidth, newHeight);
                Graphics g = Graphics.FromImage(resizedBmp);
                g.Clear(Color.Transparent);//清除整个绘图面并以透明背景色填充
                g.InterpolationMode = InterpolationMode.High;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                if (isFull)
                {
                    //不够以白色填充
                    g.DrawImage(OriginBmp, new Rectangle((newWidth - OriginBmp.Width) / 2, (newHeight - OriginBmp.Height) / 2, newWidth, newHeight), new Rectangle(0, 0, newWidth, newHeight), GraphicsUnit.Pixel);
                }
                else
                {
                    g.DrawImage(OriginBmp, new Rectangle(0, 0, newWidth, newHeight), new Rectangle(0, 0, OriginBmp.Width, OriginBmp.Height), GraphicsUnit.Pixel);
                }
                OriginBmp = (Bitmap)resizedBmp.Clone();
                g.Dispose();
                resizedBmp.Dispose();
            }
            catch
            {
                Dispose();
                return false;
            }
            return true;
        }
        /// <summary>
        /// 裁剪图片
        /// </summary>
        /// <param name="xPosition">X轴</param>
        /// <param name="yPosition">Y轴</param>
        /// <param name="width">宽</param>
        /// <param name="height">高</param>
        public bool Crop(int xPosition, int yPosition, int width, int height)
        {
            try
            {
                Bitmap temp = (Bitmap)OriginBmp;
                Bitmap bmap = (Bitmap)temp.Clone();
                if (xPosition + width > OriginBmp.Width)
                    width = OriginBmp.Width - xPosition;
                if (yPosition + height > OriginBmp.Height)
                    height = OriginBmp.Height - yPosition;
                Rectangle rect = new Rectangle(xPosition, yPosition, width, height);
                OriginBmp = (Bitmap)bmap.Clone(rect, bmap.PixelFormat);
                temp.Dispose();
                bmap.Dispose();
            }
            catch (Exception)
            {
                Dispose();
                return false;
            }
            return true;
        }
        /// <summary>
        /// 设置色相
        /// </summary>
        /// <param name="colorFilterType">色相类型</param>
        public bool SetColorFilter(ColorFilterTypes colorFilterType)
        {
            try
            {
                Bitmap temp = (Bitmap)OriginBmp;
                Bitmap bmap = (Bitmap)temp.Clone();
                Color c;
                for (int i = 0; i < bmap.Width; i++)
                {
                    for (int j = 0; j < bmap.Height; j++)
                    {
                        c = bmap.GetPixel(i, j);
                        int nPixelR = 0;
                        int nPixelG = 0;
                        int nPixelB = 0;
                        if (colorFilterType == ColorFilterTypes.Red)
                        {
                            nPixelR = c.R;
                            nPixelG = c.G - 255;
                            nPixelB = c.B - 255;
                        }
                        else if (colorFilterType == ColorFilterTypes.Green)
                        {
                            nPixelR = c.R - 255;
                            nPixelG = c.G;
                            nPixelB = c.B - 255;
                        }
                        else if (colorFilterType == ColorFilterTypes.Blue)
                        {
                            nPixelR = c.R - 255;
                            nPixelG = c.G - 255;
                            nPixelB = c.B;
                        }

                        nPixelR = Math.Max(nPixelR, 0);
                        nPixelR = Math.Min(255, nPixelR);

                        nPixelG = Math.Max(nPixelG, 0);
                        nPixelG = Math.Min(255, nPixelG);

                        nPixelB = Math.Max(nPixelB, 0);
                        nPixelB = Math.Min(255, nPixelB);

                        bmap.SetPixel(i, j, Color.FromArgb((byte)nPixelR, (byte)nPixelG, (byte)nPixelB));
                    }
                }
                OriginBmp = (Bitmap)bmap.Clone();
                temp.Dispose();
                bmap.Dispose();
            }
            catch
            {
                Dispose();
                return false;
            }
            return true;
        }
        /// <summary>
        /// 设置曲线
        /// </summary>
        /// <param name="red"></param>
        /// <param name="green"></param>
        /// <param name="blue"></param>
        public bool SetGamma(double red, double green, double blue)
        {
            try
            {
                Bitmap temp = (Bitmap)OriginBmp;
                Bitmap bmap = (Bitmap)temp.Clone();
                Color c;
                byte[] redGamma = CreateGammaArray(red);
                byte[] greenGamma = CreateGammaArray(green);
                byte[] blueGamma = CreateGammaArray(blue);
                for (int i = 0; i < bmap.Width; i++)
                {
                    for (int j = 0; j < bmap.Height; j++)
                    {
                        c = bmap.GetPixel(i, j);
                        bmap.SetPixel(i, j, Color.FromArgb(redGamma[c.R], greenGamma[c.G], blueGamma[c.B]));
                    }
                }
                OriginBmp = (Bitmap)bmap.Clone();
                temp.Dispose();
                bmap.Dispose();
            }
            catch
            {
                Dispose();
                return false;
            }
            return true;
        }
        /// <summary>
        ///  获取曲线数组
        /// </summary>
        /// <param name="color">色彩</param>
        /// <returns>数组</returns>
        private byte[] CreateGammaArray(double color)
        {
            byte[] gammaArray = new byte[256];
            for (int i = 0; i < 256; ++i)
            {
                gammaArray[i] = (byte)Math.Min(255, (int)((255.0 * Math.Pow(i / 255.0, 1.0 / color)) + 0.5));
            }
            return gammaArray;
        }
        /// <summary>
        /// 设置亮度
        /// </summary>
        /// <param name="brightness">亮度,-255到+255之间的数值</param>
        public bool SetBrightness(int brightness)
        {
            try
            {
                Bitmap temp = (Bitmap)OriginBmp;
                Bitmap bmap = (Bitmap)temp.Clone();
                if (brightness < -255) brightness = -255;
                if (brightness > 255) brightness = 255;
                Color c;
                for (int i = 0; i < bmap.Width; i++)
                {
                    for (int j = 0; j < bmap.Height; j++)
                    {
                        c = bmap.GetPixel(i, j);
                        int cR = c.R + brightness;
                        int cG = c.G + brightness;
                        int cB = c.B + brightness;

                        if (cR < 0) cR = 1;
                        if (cR > 255) cR = 255;

                        if (cG < 0) cG = 1;
                        if (cG > 255) cG = 255;

                        if (cB < 0) cB = 1;
                        if (cB > 255) cB = 255;

                        bmap.SetPixel(i, j, Color.FromArgb((byte)cR, (byte)cG, (byte)cB));
                    }
                }
                OriginBmp = (Bitmap)bmap.Clone();
                temp.Dispose();
                bmap.Dispose();
            }
            catch
            {
                Dispose();
                return false;
            }
            return true;
        }
        /// <summary>
        /// 设置对比度
        /// </summary>
        /// <param name="contrast">对比度,-100到+100之间的数值</param>
        public bool SetContrast(double contrast)
        {
            try
            {
                Bitmap temp = (Bitmap)OriginBmp;
                Bitmap bmap = (Bitmap)temp.Clone();
                if (contrast < -100) contrast = -100;
                if (contrast > 100) contrast = 100;
                contrast = (100.0 + contrast) / 100.0;
                contrast *= contrast;
                Color c;
                for (int i = 0; i < bmap.Width; i++)
                {
                    for (int j = 0; j < bmap.Height; j++)
                    {
                        c = bmap.GetPixel(i, j);
                        double pR = c.R / 255.0;
                        pR -= 0.5;
                        pR *= contrast;
                        pR += 0.5;
                        pR *= 255;
                        if (pR < 0) pR = 0;
                        if (pR > 255) pR = 255;

                        double pG = c.G / 255.0;
                        pG -= 0.5;
                        pG *= contrast;
                        pG += 0.5;
                        pG *= 255;
                        if (pG < 0) pG = 0;
                        if (pG > 255) pG = 255;

                        double pB = c.B / 255.0;
                        pB -= 0.5;
                        pB *= contrast;
                        pB += 0.5;
                        pB *= 255;
                        if (pB < 0) pB = 0;
                        if (pB > 255) pB = 255;

                        bmap.SetPixel(i, j, Color.FromArgb((byte)pR, (byte)pG, (byte)pB));
                    }
                }
                OriginBmp = (Bitmap)bmap.Clone();
                temp.Dispose();
                bmap.Dispose();
            }
            catch
            {
                Dispose();
                return false;
            }
            return true;
        }

        /// <summary>
        /// 设置灰度
        /// </summary>
        public bool SetGrayscale()
        {
            try
            {
                Bitmap temp = (Bitmap)OriginBmp;
                Bitmap bmap = (Bitmap)temp.Clone();
                Color c;
                for (int i = 0; i < bmap.Width; i++)
                {
                    for (int j = 0; j < bmap.Height; j++)
                    {
                        c = bmap.GetPixel(i, j);
                        byte gray = (byte)(.299 * c.R + .587 * c.G + .114 * c.B);

                        bmap.SetPixel(i, j, Color.FromArgb(gray, gray, gray));
                    }
                }
                OriginBmp = (Bitmap)bmap.Clone();
                temp.Dispose();
                bmap.Dispose();
            }
            catch
            {
                Dispose();
                return false;
            }
            return true;
        }
        /// <summary>
        /// 底片效果
        /// </summary>
        public bool SetInvert()
        {
            try
            {
                Bitmap temp = (Bitmap)OriginBmp;
                Bitmap bmap = (Bitmap)temp.Clone();
                Color c;
                for (int i = 0; i < bmap.Width; i++)
                {
                    for (int j = 0; j < bmap.Height; j++)
                    {
                        c = bmap.GetPixel(i, j);
                        bmap.SetPixel(i, j, Color.FromArgb(255 - c.R, 255 - c.G, 255 - c.B));
                    }
                }
                OriginBmp = (Bitmap)bmap.Clone();
                temp.Dispose();
                bmap.Dispose();
            }
            catch
            {
                Dispose();
                return false;
            }
            return true;
        }
        /// <summary>
        /// 翻转
        /// </summary>
        /// <param name="rotateFlipType">图像的旋转方向和用于翻转图像的轴</param>
        public bool RotateFlip(RotateFlipType rotateFlipType)
        {
            try
            {
                Bitmap temp = (Bitmap)OriginBmp;
                Bitmap bmap = (Bitmap)temp.Clone();
                bmap.RotateFlip(rotateFlipType);
                OriginBmp = (Bitmap)bmap.Clone();
                temp.Dispose();
                bmap.Dispose();
            }
            catch
            {
                Dispose();
                return false;
            }
            return true;
        }

        /// <summary>
        /// 插入文字
        /// </summary>
        /// <param name="text">要插入的文字</param>
        /// <param name="xPosition">X位置</param>
        /// <param name="yPosition">Y位置</param>
        /// <param name="fontName">字体</param>
        /// <param name="fontSize">大小</param>
        /// <param name="fontStyle">类型</param>
        /// <param name="alpha">透明度</param>
        public bool InsertText(string text, int xPosition, int yPosition, string fontName, float fontSize, FontStyle fontStyle, int alpha)
        {
            try
            {
                Bitmap temp = (Bitmap)OriginBmp;
                Bitmap bmap = (Bitmap)temp.Clone();
                int imgPhotoWidth = bmap.Width;
                int imgPhotoHeight = bmap.Height;

                Bitmap bmPhoto = new Bitmap(imgPhotoWidth, imgPhotoHeight, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                bmPhoto.SetResolution(72, 72);
                Graphics gbmPhoto = Graphics.FromImage(bmPhoto);
                gbmPhoto.Clear(Color.White); //去背景色 
                gbmPhoto.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                gbmPhoto.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                gbmPhoto.DrawImage(bmap, new Rectangle(0, 0, imgPhotoWidth, imgPhotoHeight), 0, 0, imgPhotoWidth, imgPhotoHeight, GraphicsUnit.Pixel);
                if (string.IsNullOrEmpty(fontName))
                    fontName = "Times New Roman";
                if (fontSize.Equals(null))
                    fontSize = 10.0F;
                Font font = new Font(fontName, fontSize, fontStyle);

                System.Drawing.StringFormat StrFormat = new System.Drawing.StringFormat();
                // 画两次制造透明效果 
                System.Drawing.SolidBrush semiTransBrush2 = new System.Drawing.SolidBrush(Color.FromArgb(alpha, 0, 0, 0));
                gbmPhoto.DrawString(text, font, semiTransBrush2, new System.Drawing.PointF(xPosition, yPosition), StrFormat);

                System.Drawing.SolidBrush semiTransBrush = new System.Drawing.SolidBrush(Color.FromArgb(alpha, 255, 255, 255));
                gbmPhoto.DrawString(text, font, semiTransBrush, new System.Drawing.PointF(xPosition, yPosition), StrFormat);

                OriginBmp = (Bitmap)bmPhoto.Clone();
                temp.Dispose();
                bmap.Dispose();
                bmPhoto.Dispose();
                gbmPhoto.Dispose();
            }
            catch
            {
                Dispose();
                return false;
            }
            return true;
        }
        /// <summary>
        /// 插入文字
        /// </summary>
        /// <param name="text">要插入的文字</param>
        /// <param name="xPosition">X位置</param>
        /// <param name="yPosition">Y位置</param>
        /// <param name="fontName">字体</param>
        /// <param name="fontSize">大小</param>
        /// <param name="fontStyle">类型</param>
        /// <param name="colorName1">颜色</param>
        /// <param name="colorName2">颜色</param>
        public bool InsertText(string text, int xPosition, int yPosition, string fontName, float fontSize, FontStyle fontStyle, string colorName1, string colorName2)
        {
            try
            {
                Bitmap temp = (Bitmap)OriginBmp;
                Bitmap bmap = (Bitmap)temp.Clone();
                Graphics gr = Graphics.FromImage(bmap);
                if (string.IsNullOrEmpty(fontName))
                    fontName = "Times New Roman";
                if (fontSize.Equals(null))
                    fontSize = 10.0F;
                Font font = new Font(fontName, fontSize, fontStyle);
                if (string.IsNullOrEmpty(colorName1))
                    colorName1 = "Black";
                if (string.IsNullOrEmpty(colorName2))
                    colorName2 = colorName1;
                Color color1 = Color.FromName(colorName1);
                Color color2 = Color.FromName(colorName2);
                int gW = (int)(text.Length * fontSize);
                gW = gW == 0 ? 10 : gW;
                LinearGradientBrush LGBrush = new LinearGradientBrush(new Rectangle(0, 0, gW, (int)fontSize), color1, color2, LinearGradientMode.Vertical);
                gr.DrawString(text, font, LGBrush, xPosition, yPosition);
                OriginBmp = (Bitmap)bmap.Clone();

                temp.Dispose();
                bmap.Dispose();
                gr.Dispose();
            }
            catch
            {
                Dispose();
                return false;
            }
            return true;
        }

        /// <summary>
        /// 插入图片(并设置透明度)
        /// </summary>
        /// <param name="imagePath">图片地址</param>
        /// <param name="xPosition">X轴</param>
        /// <param name="yPosition">Y轴</param>
        ///   <param name="alpha"> 水印透明度设置 </param>  
        public bool InsertImage(string imagePath, int xPosition, int yPosition, float alpha)
        {
            return InsertImage(imagePath, xPosition, yPosition, alpha, 0, 0);
        }
        /// <summary>
        /// 插入图片（设置透明度并改变水印大小）
        /// </summary>
        /// <param name="imagePath">图片地址</param>
        /// <param name="xPosition">X轴</param>
        /// <param name="yPosition">Y轴</param>
        ///   <param name="alpha"> 水印透明度设置 </param> 
        ///   <param name="waterWidth">水印图宽 </param> 
        ///   <param name="waterHeight"> 水印图高 </param> 
        public bool InsertImage(string imagePath, int xPosition, int yPosition, float alpha, int waterWidth, int waterHeight)
        {
            try
            {
                Bitmap temp = (Bitmap)OriginBmp;
                Bitmap bmap = (Bitmap)temp.Clone();
                Graphics gr = Graphics.FromImage(bmap);
                if (!string.IsNullOrEmpty(imagePath))
                {
                    Bitmap i_bitmap = (Bitmap)Bitmap.FromFile(imagePath);//水印图片

                    ImageAttributes imageAttributes = new ImageAttributes();
                    // 设置两种颜色,达到合成效果 
                    ColorMap colorMap = new ColorMap();
                    colorMap.OldColor = Color.FromArgb(255, 0, 255, 0);
                    colorMap.NewColor = Color.FromArgb(0, 0, 0, 0);
                    System.Drawing.Imaging.ColorMap[] remapTable = { colorMap };
                    imageAttributes.SetRemapTable(remapTable, ColorAdjustType.Bitmap);

                    // 用矩阵设置水印图片透明度 
                    float[][] colorMatrixElements =  { 
                new   float [] { 1.0f ,   0.0f ,   0.0f ,   0.0f ,  0.0f },
                new   float [] { 0.0f ,   1.0f ,   0.0f ,   0.0f ,  0.0f },
                new   float [] { 0.0f ,   0.0f ,   1.0f ,   0.0f ,  0.0f },
                new   float [] { 0.0f ,   0.0f ,   0.0f ,  alpha,  0.0f },
                new   float [] { 0.0f ,   0.0f ,   0.0f ,   0.0f ,  1.0f }
            };

                    ColorMatrix wmColorMatrix = new ColorMatrix(colorMatrixElements);
                    imageAttributes.SetColorMatrix(wmColorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                    waterWidth = waterWidth == 0 ? i_bitmap.Width : waterWidth;
                    waterHeight = waterHeight == 0 ? i_bitmap.Height : waterHeight;
                    gr.DrawImage(i_bitmap, new Rectangle(xPosition, yPosition, waterWidth, waterHeight), 0, 0, i_bitmap.Width, i_bitmap.Height, GraphicsUnit.Pixel, imageAttributes);

                    gr.Dispose();
                    i_bitmap.Dispose();
                }
                OriginBmp = (Bitmap)bmap.Clone();

                temp.Dispose();
                bmap.Dispose();
            }
            catch
            {
                Dispose();
                return false;
            }
            return true;
        }
        /// <summary>
        /// 插入图片(不设置透明度)
        /// </summary>
        /// <param name="imagePath">图片地址</param>
        /// <param name="xPosition">X轴</param>
        /// <param name="yPosition">Y轴</param>
        public bool InsertImage(string imagePath, int xPosition, int yPosition)
        {
            try
            {
                Bitmap temp = (Bitmap)OriginBmp;
                Bitmap bmap = (Bitmap)temp.Clone();
                Graphics gr = Graphics.FromImage(bmap);
                if (!string.IsNullOrEmpty(imagePath))
                {
                    Bitmap i_bitmap = (Bitmap)Bitmap.FromFile(imagePath);
                    Rectangle rect = new Rectangle(xPosition, yPosition, i_bitmap.Width, i_bitmap.Height);
                    gr.DrawImage(Bitmap.FromFile(imagePath), rect);
                    gr.Dispose();
                    i_bitmap.Dispose();
                }
                OriginBmp = (Bitmap)bmap.Clone();

                temp.Dispose();
                bmap.Dispose();
            }
            catch
            {
                Dispose();
                return false;
            }
            return true;
        }
        /// <summary>
        /// 插入形状
        /// </summary>
        /// <param name="shapeType"></param>
        /// <param name="xPosition">X位置</param>
        /// <param name="yPosition">Y位置</param>
        /// <param name="width">宽</param>
        /// <param name="height">高</param>
        /// <param name="colorName">颜色</param>
        public bool InsertShape(string shapeType, int xPosition, int yPosition, int width, int height, string colorName)
        {
            try
            {
                Bitmap temp = (Bitmap)OriginBmp;
                Bitmap bmap = (Bitmap)temp.Clone();
                Graphics gr = Graphics.FromImage(bmap);
                if (string.IsNullOrEmpty(colorName))
                    colorName = "Black";
                Pen pen = new Pen(Color.FromName(colorName));
                switch (shapeType.ToLower())
                {
                    case "filledellipse":
                        gr.FillEllipse(pen.Brush, xPosition, yPosition, width, height);
                        break;
                    case "filledrectangle":
                        gr.FillRectangle(pen.Brush, xPosition, yPosition, width, height);
                        break;
                    case "ellipse":
                        gr.DrawEllipse(pen, xPosition, yPosition, width, height);
                        break;
                    case "rectangle":
                    default:
                        gr.DrawRectangle(pen, xPosition, yPosition, width, height);
                        break;

                }
                gr.Dispose();
                OriginBmp = (Bitmap)bmap.Clone();

                temp.Dispose();
                bmap.Dispose();
            }
            catch
            {
                Dispose();
                return false;
            }
            return true;
        }
        #endregion

        #region  具体设置

        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="width">缩略图宽度</param>
        /// <param name="height">缩略图高度</param>
        /// <param name="mode">生成缩略图的方式("hw"表示按指定高宽缩放，"w"按指定宽缩放，"h"按指定高缩放，"cut"按指定高宽裁剪)</param>  
        /// <returns>返回bool类型</returns>
        private bool MakeImage(int width, int height, string mode)
        {
            try
            {
                int towidth = width;
                int toheight = height;

                int x = 0;
                int y = 0;
                int ow = OriginBmp.Width;
                int oh = OriginBmp.Height;

                switch (mode.ToLower())
                {
                    case "hw"://指定高宽缩放（可能变形）                
                        break;
                    case "w"://指定宽，高按比例                    
                        toheight = OriginBmp.Height * width / OriginBmp.Width;
                        break;
                    case "h"://指定高，宽按比例
                        towidth = OriginBmp.Width * height / OriginBmp.Height;
                        break;
                    case "cut"://指定高宽裁减（不变形）  
                        if ((double)OriginBmp.Width / (double)OriginBmp.Height > (double)towidth / (double)toheight)
                        {
                            oh = OriginBmp.Height;
                            ow = OriginBmp.Height * towidth / toheight;
                            y = 0;
                            x = 0;
                        }
                        else
                        {
                            ow = OriginBmp.Width;
                            oh = OriginBmp.Width * height / towidth;
                            x = 0;
                            y = 0;
                        }
                        break;
                    default:
                        break;
                }
                if (!Crop(x, y, ow, oh))
                {
                    //裁剪
                    Dispose();
                    return false;
                }
                if (!Resize(towidth, toheight))
                {
                    //缩放图片
                    Dispose();
                    return false;
                }

            }
            catch
            {
                Dispose();
                return false;
            }
            return true;
        }

        /// <summary>
        /// 按比例伸缩图片
        /// </summary>
        /// <param name="width">宽</param>
        /// <param name="height">高</param>
        /// <returns></returns>
        public bool Make(int width, int height)
        {
            return MakeImage(width, height, "cut");//按比例伸缩
        }
        /// <summary>
        /// 裁剪图片
        /// </summary>
        /// <param name="width">宽</param>
        /// <param name="height">高</param>
        /// <param name="isProportion">是否按比例伸缩</param>
        /// <returns></returns>
        public bool Make(int width, int height, bool isProportion)
        {
            if (isProportion)
            {
                return MakeImage(width, height, "cut");//按比例伸缩
            }
            return MakeImage(width, height, "hw");
        }
        /// <summary>
        /// 按指定高或者宽缩放
        /// </summary>
        /// <param name="widthOrheight">高或者宽长度</param>
        /// <param name="isWidth">是否按宽缩放</param>
        /// <returns></returns>
        public bool Make(int widthOrheight, bool isWidth)
        {
            if (isWidth)
            {
                return MakeImage(widthOrheight, widthOrheight, "w");
            }
            return MakeImage(widthOrheight, widthOrheight, "h");
        }
        /// <summary>
        /// 按指定位置，指定高宽处理
        /// </summary>
        /// <param name="xPosition">X轴</param>
        /// <param name="yPosition">Y轴</param>
        /// <param name="width">宽</param>
        /// <param name="height">高</param>
        /// <returns></returns>
        public bool Make(int xPosition, int yPosition, int width, int height)
        {
            return Crop(xPosition, yPosition, width, height);
        }
        /// <summary>
        /// 缩略图（最大高宽）
        /// </summary>
        /// <param name="maxWidth">最大宽</param>
        /// <param name="maxHeight">最大高</param>
        public bool ResizeImg(int maxWidth, int maxHeight)
        {
            int ow = OriginBmp.Width;
            int oh = OriginBmp.Height;
            if (ow > maxWidth || oh > maxHeight)//如果原图小于最大限制则不处理
            {
                if ((double)maxWidth / (double)ow > (double)maxHeight / (double)oh)
                {
                    return Make(maxHeight, false);//按高伸缩
                }
                return Make(maxWidth, true);//按宽伸缩
            }
            return true;
        }

        /// <summary>
        /// 缩略图
        /// </summary>
        /// <param name="newWidth">宽</param>
        /// <param name="newHeight">高</param>
        /// <returns></returns>
        public bool Resize(int newWidth, int newHeight)
        {
            return Resize(newWidth, newHeight, false);
        }

        //  Eric 2010-5-22 ADD
        /// <summary>
        /// 格式化字符串
        /// </summary>
        /// <param name="strErr">错误信息</param>
        /// <param name="strInfo">错误描述</param>
        /// <returns>格式化后的字符串</returns>
        public static string FormatString(string strErr, string[] strInfo)
        {
            string sBuilder = "", strResult = strErr;
            for (int i = 0; i < strInfo.Length; i++)
            {
                string first = strResult.Substring(0, strResult.LastIndexOf("%s"));
                string last = strResult.Substring(strResult.LastIndexOf("%s"));
                last = last.Replace("%s", strInfo[strInfo.Length - i - 1]);
                sBuilder += first + last;
            }
            return sBuilder;
        }
        /// <summary>
        /// 格式化字符串
        /// </summary>
        /// <param name="str">要格式化的字符串</param>
        /// <param name="strDefault">默认字符串</param>
        /// <returns>格式化后的字符串</returns>
        public static string FormatString(object str, string strDefault)
        {
            if (str == null)
            {
                str = strDefault;
            }
            return str.ToString().Trim();
        }

        /// <summary>
        /// 判断字符串是否为数字
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNumeric(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }
            return Regex.IsMatch(value, @"^[+-]?\d*[.]?\d*$");
        }
        /// <summary>
        /// 返回数字类型，如果不是返回默认值
        /// </summary>
        /// <param name="numeric">对像数值</param>
        /// <param name="defaultNumeric">默认值</param>
        /// <returns></returns>
        public static int FormatNumeric(object numeric, int defaultNumeric)
        {
            if (numeric != null && numeric + "" != "" && IsNumeric(numeric.ToString()))
            {
                return int.Parse(numeric.ToString().Trim());
            }
            else
            {
                return defaultNumeric;
            }
        }

        /// <summary>
        /// 是否水印图片
        /// </summary>
        /// <param name="isThumbnailImg">是否缩略图</param>
        /// <param name="isAddWater">是否加水印</param>
        /// <returns></returns>
        public bool SaveWaterMark(bool isThumbnailImg)
        {
            try
            {
                if (!ResizeImg(600, 600))
                {
                    //限定图片大小
                    Dispose();
                    return false;
                }
                int WaterScale = 30;
                string WaterDistance = "0.05";
                int WaterTransparency = 30;
               
                int Resize_Type = 1;
                string strAddelement;
                int iPosition = FormatNumeric(WaterPostion, 1);//水印位置(坐上角，左下角，右上角，右下角)
                string strWatermarkPath = FileHelper.GetFileFold(FormatString(WaterPath, ""));//水印图片路径
  

                int xPosition = 0, yPosition = 0;
                int imgWatermarkWidthNew = 0, imgWatermarkHeightNew = 0;

                int imgPhotoWidth = OriginBmp.Width;//原图高宽
                int imgPhotoHeight = OriginBmp.Height;

                if (WaterMark==0)//不加水印
                {
                    strAddelement = "无";
                }
                else
                {
                    if (FileHelper.FileExists(strWatermarkPath))
                    {
                        if (imgPhotoWidth < 200 || imgPhotoHeight < 200)
                        {
                            strAddelement = "无";
                        }
                        else
                        {
                            strAddelement = "图片";
                        }
                    }
                    else
                    {
                        strAddelement = "无";
                    }
                }
                
               
                if (strAddelement != "无")
                  {
                    int imgWatermarkWidth = 0, imgWatermarkHeight = 0;//图片，水印长宽
                    if (strAddelement == "图片")
                    {
                        Bitmap bitm = new Bitmap(strWatermarkPath);//水印图片
                        imgWatermarkWidth = bitm.Width;
                        imgWatermarkHeight = bitm.Height;
                        bitm.Dispose();
                    }

                    // 计算水印图片尺寸
                    double aScale = Convert.ToDouble(FormatString(WaterScale, "20")) / 100;//水印比例
                    double d = Convert.ToDouble(FormatString(WaterDistance, "0.05"));
                    int intDistanceWidth = Convert.ToInt32(OriginBmp.Width * d);
                    int intDistanceHeight = Convert.ToInt32(OriginBmp.Height * d);
                    // 设置比例 
                    int tempWatermarkWidth = Convert.ToInt32(imgPhotoWidth * aScale);
                    int tempWatermarkHeight = Convert.ToInt32((imgPhotoWidth * aScale / imgWatermarkWidth) * imgWatermarkHeight);
                    if (imgWatermarkWidth > tempWatermarkWidth || imgWatermarkHeight > tempWatermarkHeight)
                    {
                        //如果实际尺寸大于压缩后的尺寸则按比例缩小水印大小，否则按原水印大小
                        imgWatermarkWidthNew = tempWatermarkWidth;
                        imgWatermarkHeightNew = tempWatermarkHeight;
                    }
                    else
                    {
                        imgWatermarkWidthNew = imgWatermarkWidth;
                        imgWatermarkHeightNew = imgWatermarkHeight;
                    }
                    switch (iPosition)
                    {
                        case 1://坐上角
                            xPosition = intDistanceWidth;
                            yPosition = intDistanceHeight;
                            break;
                        case 2://左下角
                            xPosition = OriginBmp.Width > intDistanceWidth ? intDistanceWidth : 0;
                            yPosition = OriginBmp.Height < (imgWatermarkHeightNew + intDistanceHeight) ? 0 : OriginBmp.Height - (imgWatermarkHeightNew + intDistanceHeight);
                            break;
                        case 3://右上角
                            xPosition = OriginBmp.Width < (imgWatermarkWidthNew + intDistanceWidth) ? 0 : OriginBmp.Width - (imgWatermarkWidthNew + intDistanceWidth);
                            yPosition = OriginBmp.Height > intDistanceHeight ? intDistanceHeight : 0; ;
                            break;
                        case 4: //右下角
                            xPosition = OriginBmp.Width < (imgWatermarkWidthNew + intDistanceWidth) ? 0 : OriginBmp.Width - (imgWatermarkWidthNew + intDistanceWidth);
                            yPosition = OriginBmp.Height < (imgWatermarkHeightNew + intDistanceHeight) ? 0 : OriginBmp.Height - (imgWatermarkHeightNew + intDistanceHeight);
                            break;
                        default:
                            break;
                    }
                }
                double alpha = Convert.ToDouble(FormatString(WaterTransparency, "30"));//透明度
                switch (strAddelement)
                {
                    case "图片":
                        if (!InsertImage(strWatermarkPath, xPosition, yPosition, (float)alpha / 100, imgWatermarkWidthNew, imgWatermarkHeightNew))
                        {
                            Dispose();
                            return false;
                        }
                        break;
                    default:
                        break;
                }
                if (isThumbnailImg)//是缩略图
                {
                    int newWidth = FormatNumeric(ThumbNail_Width, 0);//缩略图宽
                    int newHeight = FormatNumeric(ThumbNail_Height, 0);//缩略图高
                    MakeImage(newWidth, newHeight,MediaScale);
                }
            }
            catch
            {
                Dispose();
                return false;
            }
            return true;
        }

        /// <summary>
        /// 给图片填充白色背景
        /// </summary>
        /// <returns></returns>
        public bool AddWrite()
        {
            try
            {
                int iWidth = OriginBmp.Width;
                int iHeight = OriginBmp.Height;
                Bitmap resizedBmp = new Bitmap(iWidth, iHeight);
                Graphics g = Graphics.FromImage(resizedBmp);
                g.Clear(Color.White);//清除整个绘图面并以透明背景色填充
                g.InterpolationMode = InterpolationMode.High;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                //不够以白色填充
                g.DrawImage(OriginBmp, new Rectangle(0, 0, iWidth, iHeight), new Rectangle(0, 0, iWidth, iHeight), GraphicsUnit.Pixel);

                OriginBmp = (Bitmap)resizedBmp.Clone();
                g.Dispose();
                resizedBmp.Dispose();
            }
            catch
            {
                Dispose();
                return false;
            }
            return true;
        }
        #endregion

        
    }
}
