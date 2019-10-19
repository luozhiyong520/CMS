using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Drawing;

namespace UP.system.plug_ins.GetVerifyImage
{
    public partial class getverifyimage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CreateCheckCodeImage(GenerateCheckCode());
        }

        /// <summary>
        /// 生成5位验证码的字母和数字
        /// </summary>
        /// <returns>验证码的字符串</returns>
        private string GenerateCheckCode()
        {
            string number;
            string strCheckCode = String.Empty;
            System.Random random = new Random();

            for (int iCount = 0; iCount <5; iCount++)
            {
                string str = @"123456789abcdefghigklmnpqrstuvwxyzABCDEFGHIGKLMNPQRSTUVWXYZ";
                number = str.Substring(0 + random.Next(59), 1);
                strCheckCode += number.ToString();
            }
            Session["strValidityCode"] = strCheckCode;
            return strCheckCode;
        }
        /// <summary>
        /// 创建验证码图片,并将其写入内存流中
        /// </summary>
        /// <param name="CheckCode"></param>
        private void CreateCheckCodeImage(string CheckCode)
        {
            if (CheckCode == null || CheckCode.Trim() == String.Empty)
            {
                return;
            }
            Bitmap img = new Bitmap((int)Math.Ceiling((CheckCode.Length * 13.5)), 20);
            Graphics g = Graphics.FromImage(img);
            try
            {
                //生成随机生成器
                Random random = new Random();

                //清空图片背景色
                g.Clear(Color.Black);

                Font font = new Font("Arial", 13, (System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic));
                System.Drawing.Drawing2D.LinearGradientBrush brush = new System.Drawing.Drawing2D.LinearGradientBrush(new Rectangle(0, 0, img.Width, img.Height), Color.White, Color.WhiteSmoke, 2.0f, true);
                g.DrawString(CheckCode, font, brush, 2, 2);

                //画图片的前景噪音点
                for (int i = 0; i < 80; i++)
                {
                    int x = random.Next(img.Width);
                    int y = random.Next(img.Height);
                    img.SetPixel(x, y, Color.FromArgb(random.Next()));
                }
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                Response.ClearContent();
                Response.ContentType = "image/Gif";
                Response.BinaryWrite(ms.ToArray());
            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {
                g.Dispose();
                img.Dispose();
            }
        }
    }
}