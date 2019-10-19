using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using SMVC;

namespace CMS.Model
{
    public partial class News
    {
        /// <summary>
        /// 正文内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 点击数
        /// </summary>
        public int ClickNum { get; set; }
 

        /// <summary>
        /// 频道名称
        /// </summary>
        public string ChannelName { get; set; }

        /// <summary>
        /// 频道链接
        /// </summary>
        public string ChannelLink { get; set; }

        /// <summary>
        /// 点击次数
        /// </summary>
        public int Num { get; set; }

        /// <summary>
        /// 编辑次数
        /// </summary>
        public int Con { get; set; }

        public string lmid { get; set; }

        /// <summary>
        /// 每条新闻的完整标题和超链接
        /// </summary>
        [ReadOnly]
        public string FullLink
        {
            get
            {
                string str = string.Empty;
                string secondTitle = SecondTitle == null ? "" : SecondTitle;
                string secondUrl = SecondUrl == null ? "" : SecondUrl;
                string titleColor = TitleColor == null ? "" : TitleColor;
                string isBold = IsBold == null ? "" : IsBold;
                string[] titles = Regex.Split(secondTitle, @"\[WL\]", RegexOptions.IgnoreCase);
                string[] urls = Regex.Split(secondUrl, @"\[WL\]", RegexOptions.IgnoreCase);
                string[] titleColors = Regex.Split(titleColor, @"\[WL\]", RegexOptions.IgnoreCase);
                string[] isBlods = Regex.Split(isBold, @"\[WL\]", RegexOptions.IgnoreCase);
                int urlLength = urls.Length;
                string fullurl = "";
                for (int i = 0; i < titles.Length; i++)
                {
                    StringBuilder style = new StringBuilder();
                    style.Append("style=\"");
                    if (urlLength > i)  //当超链接数等于标题数的时候
                    {
                        if (titleColors.Length > i)
                        {
                            if (!string.IsNullOrEmpty(titleColors[i]))
                                style.Append("color:" + titleColors[i] + ";");
                        }
                        if (isBlods.Length > i)
                        {
                            if (!string.IsNullOrEmpty(isBlods[i]))
                                if (isBlods[i] == "1")
                                    style.Append("font-weight:bold");
                        }
                        style.Append("\"");
                        if (!urls[i].StartsWith("http://"))
                        {
                            fullurl = CMS.Utility.Constant.NEWS_DETAIL_HOST + urls[i];
                        }
                        else
                        {
                            fullurl = urls[i];
                        }
                        if (i == 0)
                        {
                            str += string.Format("<a href=\"{0}\" target=\"_blank\" {2}>{1}</a>", fullurl, titles[i], style.ToString());
                        }
                        else
                        {
                            str += string.Format("&nbsp;&nbsp;<a href=\"{0}\" target=\"_blank\" {2}>{1}</a>", fullurl, titles[i], style.ToString());
                        }
                    }
                    else //标题数大于超链接数的时候
                    {
                        if (titleColors.Length > i)
                        {
                            if (!string.IsNullOrEmpty(titleColors[i]))
                                style.Append("color:" + titleColors[i] + ";");
                        }
                        if (isBlods.Length > i)
                        {
                            if (!string.IsNullOrEmpty(isBlods[i]))
                                if (isBlods[i] == "1")
                                    style.Append("font-weight:bold");
                        }
                        style.Append("\"");
                        str += string.Format("<span {0}>{1}</span>", style.ToString(), titles[i]);
                    }

                }
                return str.Replace(" style=\"\"", "");
            }
        }

        /// <summary>
        /// 新闻内容的分页列表
        /// </summary>
        [ReadOnly]
        public List<string> ContentList
        {
            get
            {
                if (string.IsNullOrEmpty(Content))
                    return null;
                Regex reg = new Regex(Regex.Escape("_baidu_page_break_tag_"), RegexOptions.IgnoreCase);
                string[] contentList = reg.Split(Content);
                return contentList.ToList();
            }
        }
    }
}
