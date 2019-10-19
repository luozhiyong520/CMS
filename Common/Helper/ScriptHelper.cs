using System;
using System.Collections.Generic;
using System.Web;
using System.Text;
using System.Web.UI;

namespace Common
{
    public class ScriptHelper
    {
        #region 使用Response输出脚本
        /// <summary>
        /// 输出脚本
        /// </summary>
        /// <param name="pMsg"></param>
        public static void Alert(string pMsg)
        {
            pMsg = pMsg.Replace("\r\n", "。");
            WriteScript(string.Format("alert('{0}');", pMsg));
        }

        /// <summary>
        /// 弹出alert窗口
        /// </summary>
        /// <param name="msg">显示内容</param>
        /// <param name="url">跳转地址</param>
        public static void Alert(string msg, string url)
        {
            msg = msg.Replace("\r\n", "。");
            WriteScript(string.Format("alert('{0}');location.href='{1}';", msg, url));
        }

        /// <summary>
        /// 弹出alert窗口
        /// </summary>
        /// <param name="msg">显示内容</param>
        /// <param name="url">跳转地址</param>
        /// <param name="isSelf">是否为本身弹出</param>
        public static void Alert(string msg, string url, bool isSelf)
        {
            msg = msg.Replace("\r\n", "。");
            string content = string.Format("alert('{0}');location.href='{1}';", msg, url);
            if (!isSelf)
            {
                content = string.Format("alert('{0}');top.location.href='{1}';", msg, url);
            }
            WriteScript(content);
        }


        /// <summary>
        /// 输出脚本
        /// </summary>
        /// <param name="pScriptCode"></param>
        public static void WriteScript(string pScriptCode)
        {
            StringBuilder myStr = new StringBuilder(60);
            myStr.AppendFormat(@"<script language=javascript>{0}", Environment.NewLine);
            myStr.AppendFormat(@"{0}{0}{2}{1}", "\t", Environment.NewLine, pScriptCode);
            myStr.AppendFormat(@"</script>{0}", Environment.NewLine);
            HttpContext.Current.Response.Write(myStr.ToString());
        }
        /// <summary>
        /// 输出脚本
        /// </summary>
        /// <param name="pNewUrl"></param>
        public static void ChangeLocation(string pNewUrl)
        {
            HttpContext.Current.Response.Write("<html>\r\n<body>\r\n");
            WriteScript("window.location='" + pNewUrl + "';");
            HttpContext.Current.Response.Write("</body>\r\n</html>\r\n");
        }

        /// <summary>
        /// 输出脚本
        /// </summary>
        /// <param name="pNewUrl"></param>
        public static void ChangeTopLocation(string pNewUrl)
        {
            HttpContext.Current.Response.Write("<html>\r\n<body>\r\n");
            WriteScript("window.top.location='" + pNewUrl + "';");
            HttpContext.Current.Response.Write("</body>\r\n</html>\r\n");
        }

        /// <summary>
        /// 输出脚本
        /// </summary>
        /// <param name="pNewUrl"></param>
        public static void TopLocation(string pNewUrl)
        {
            WriteScript("window.top.location='" + pNewUrl + "';");
        }
        #endregion

        #region 使用Page注册脚本
        /// <summary>
        /// 输出脚本
        /// </summary>
        /// <param name="pPage"></param>
        /// <param name="pMsg"></param>
        public static void Alert(System.Web.UI.Page pPage, string pMsg)
        {
            pMsg = pMsg.Replace("\r\n", "。");
            WriteScript(pPage, string.Format("alert('{0}');", pMsg));
        }

        /// <summary>
        /// 输出脚本
        /// </summary>
        /// <param name="pPage"></param>
        /// <param name="pMsg"></param>
        /// <param name="toUrl"></param>
        public static void Alert(System.Web.UI.Page pPage, string pMsg,string toUrl)
        {
            pMsg = pMsg.Replace("\r\n", "。");
            WriteScript(pPage, string.Format("alert('{0}');window.location.href = '{1}';", pMsg, toUrl));
        }

        /// <summary>
        /// 输出脚本
        /// </summary>
        /// <param name="pPage"></param>
        /// <param name="pScriptCode"></param>
        public static void WriteScript(System.Web.UI.Page pPage, string pScriptCode)
        {
            StringBuilder myStr = new StringBuilder(60);
            myStr.AppendFormat(@"<script language=javascript>{0}", Environment.NewLine);
            myStr.AppendFormat(@"{0}{0}{2}{1}", "\t", Environment.NewLine, pScriptCode);
            myStr.AppendFormat(@"</script>{0}", Environment.NewLine);
            ClientScriptManager csm = pPage.ClientScript;
            //csm.RegisterClientScriptBlock(this.GetType(),"_t_"+ DateTime.Now.Ticks ,myStr.ToString());
            csm.RegisterStartupScript(pPage.GetType(), "_t_" + DateTime.Now.Ticks, myStr.ToString());
        }

        #endregion
        /// <summary>
        /// showModelessDialog
        /// </summary>
        /// <param name="pUrl"></param>
        /// <param name="pWidth"></param>
        /// <param name="pHeight"></param>
        public static void ShowDialog(string pUrl, int pWidth, int pHeight)
        {
            WriteScript(string.Format("window.showModelessDialog('{0}','','width={1};height={2}');", pUrl, pWidth, pHeight));
            //window.showModelessDialog('','','');
        }

        public static void OpenWindowInNewPage(Page curPage, string destUrl)
        {
            string scriptString = string.Format("<script language='JavaScript'>window.open('" + destUrl + "','_new');<", destUrl);
            scriptString += "/";
            scriptString += "script>";
            ClientScriptManager csm = curPage.ClientScript;
            if (!csm.IsStartupScriptRegistered("Startup"))
            {
                csm.RegisterStartupScript(curPage.GetType(), "_t_" + DateTime.Now.Ticks, scriptString);
            }
        }
    }
}
