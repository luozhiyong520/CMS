using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMVC;
using CMS.BLL;
using System.IO;
using Common;

namespace CMS.Controller
{
    public class TestController
    {
        [Action]
        [PageUrl(Url = "/test/sso.aspx")]
        public object test()
        {
            new RedirectResult("http://www.baidu.com");
            //using (StreamWriter sw = new StreamWriter("E:/user.txt"))
            //{
            //    for (int i = 0; i < 33333; i++)
            //    {
            //        sw.WriteLine(13500001 + i + "," + getName(i));
            //    }

            //using (StreamWriter sw = new StreamWriter("E:/user.txt"))
            //{
            //    for (int i = 200; i < 500; i++)
            //    {
            //        sw.WriteLine(1949 + i + "," + "youny" + i);
            //    }
            //}


            //SsoServer.SsoPush(23);

            //SsoServer.SsoPushOne(19);


            string str = string.Empty;

            //using (StreamReader sr = new StreamReader("E:/13071017402474.docx"))
            //{
            //    str = sr.ReadToEnd();
            //}

            return new PageResult(null,null);
        }


        [Action]
        [PageUrl(Url = "/test/getval.aspx")]
        public string getStr(string val)
        {
            string res = Common.StringHelper.WipeHtml(val).Replace("\n","<br/>");
            return res;
        }

        private string getName(int a)
        {
            Random r = new Random(a);
            return getName(r.Next(4, 10),a);
        }


        private string getName(int ii, int a)
        {
            string name = string.Empty;

            Random r = new Random(a);

            for (int i = 0; i < ii; i++)
            {
                int aa = r.Next(65, 122);
                if (aa < 91 || aa > 96)
                    name += (char)aa;
                else
                {
                    i--;
                }
            }
            return name;
        }

    }
}
