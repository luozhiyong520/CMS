using System;
using System.Text;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.ServiceModel;
using CMS.BLL;
using CMS.HtmlService.Contract;
using CMS.Model;
using CMS.Template.Complex;
using CMS.Template.Lexer;
using CMS.Template.Parser;
using CMS.Template.Parser.AST;
using Common;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;

namespace CMS.HtmlService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, Namespace = "CMS.HtmlService")]
    public class HtmlEngine : IHtmlEngine
    {
        private readonly string templateBasePath = AppConfig.TemplateBasePath;
        private readonly string saveBasePath = AppConfig.HtmlSaveBasePath;
        PageTemplateBLL pageTemplateBLL = Factory.BusinessFactory.CreateBll<PageTemplateBLL>();
        TemplateDetailBLL templateDetailBLL = Factory.BusinessFactory.CreateBll<TemplateDetailBLL>();
        NewsBLL newsBLL = Factory.BusinessFactory.CreateBll<NewsBLL>();

        /// <summary>
        /// 生成单篇文章的静态页
        /// </summary>
        /// <param name="newsId"></param>
        /// <param name="operateType"></param>
        /// <returns></returns>
        public bool CreateSingleNews(int newsId, string operateType)
        {
            Loger.Info(newsId.ToString());
            var news = getNews(newsId);
            if (news == null)
                throw new ArgumentNullException(string.Format("newsid:{0}",newsId.ToString()));
            try
            {               
                PageTemplate template = pageTemplateBLL.GetDetailPageTemplateIdByChannelId(news.ChannelId);
                if (template != null && template.Status.Value)
                {                  
                    TemplateManager manager = createManager(template, false);
                    //manager.RegisterCustomTag("include", new IncludeTagHandler());   //注册文件引用标签
                    var prefixUrl = news.Url.Substring(0, news.Url.LastIndexOf(".html"));
                    manager.SetValue("art", news);
                    manager.SetValue("curdate", DateTime.Now);
                    manager.SetValue("maketype", 0);
                    manager.SetValue("prefixurl", prefixUrl);
                    int loop = 1;
                    if (news.ContentList != null && news.ContentList.Count > 1)
                        loop = news.ContentList.Count;
                    manager.SetValue("pagecount", loop.ToString());
                    var listHtml = new List<string>();
                    for (int m = 0; m < loop; m++)
                    {
                        manager.SetValue("curpage",(m+1).ToString());
                        manager.SetValue("Content",news.ContentList[m]);//新闻每页的内容

                        manager.SetValue("curdate", DateTime.Now);
                        manager.SetValue("maketype", 0);
                        manager.SetValue("begindate", null);  //需要生成的对应日期
                        manager.Process();
                        listHtml.Add(manager.SingleProcess());
                    }
                   


                    if (listHtml != null && listHtml.Count > 0)
                    {
                        for (int i = 0; i < listHtml.Count; i++)
                        {
                            var newsUrl = news.Url;
                            if (i > 0)
                                newsUrl = string.Format("{0}_{1}.html", prefixUrl, (i+1).ToString());
                            var targetFilePath = saveBasePath + "/" + template.SiteName + newsUrl;
                           // Loger.Info(targetFilePath);
                            //保存文章页静态页文件
                            FileHelper.WriteFile(targetFilePath, listHtml[i], template.Encoding);
                            Loger.Info(string.Format("方法名：{0}，{1}新闻，生成NewsId:{2}文件成功，文件路径:{3}", "CreateSingleNews", operateType == "add" ? "新增" : "修改", news.NewsId.ToString(), newsUrl));
                            //ftp上传操作
                            string errorinfo;
                            bool flag = new FileReceive().FtpUpload(targetFilePath, "/" + newsUrl, template.SiteName, out errorinfo);
                        }
                    }





                    





                }
                else
                {
                    logPageTemplate(template);
                }
                //生成和ChannelId相关的内容，即使在栏目文章的详细页不需要生成最终页的时候也执行该操作
                Dictionary<string,string> dic = new Dictionary<string,string>();
                dic.Add("newsdate",news.CreatedTime.ToString());
                return CreateTemplatePageByChannelId(news.ChannelId, dic);
            }
            catch (Exception ex)
            {
                Loger.Error(ex, string.Format("方法名：{0}，生成NewsId:{1}失败", "CreateSingleNews", news.NewsId.ToString()));
                return false;
            }
        }

        

        /// <summary>
        /// 生成模板页面静态页面
        /// </summary>
        /// <param name="templateId"></param>
        /// <param name="paramDic">模板里面的动态参数设置</param>
        /// <returns></returns>
        public bool CreateTemplatePage(int templateId, Dictionary<string, string> paramDic = null)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("TemplateId", templateId);
            PageTemplate template = pageTemplateBLL.Get(dictionary);
            try
            {
                string errorinfo;
                bool flag;
                if (template != null && template.Status.Value)
                {
                    TemplateManager manager = createManager(template, false);
                    //manager.RegisterCustomTag("include", new IncludeTagHandler());   //注册文件引用标签
                    
                    manager.SetValue("curdate", DateTime.Now);
                    manager.SetValue("maketype", 0);
                    manager.SetValue("begindate", paramDic == null ? null : (paramDic.ContainsKey("reDate") ? paramDic["reDate"] : null));  //需要生成的对应日期
                    if (paramDic != null && paramDic.Count > 0)
                    {
                        foreach (KeyValuePair<string, string> dic in paramDic)
                        {
                            manager.SetValue(dic.Key, dic.Value);
                        }
                    }
                    
                    

                    List<string> htmlList = manager.Process();
                    string targetFilePath = saveBasePath + "/" + template.SiteName + template.HtmlPath;
                    string ftpUploadPath = template.HtmlPath;
                    MatchCollection mc = Regex.Matches(template.HtmlPath,"{[^/]+}",RegexOptions.IgnoreCase);

                    
                    //让生成的页面，可以支持以时间格式命名的页面
                    if (paramDic != null && paramDic.ContainsKey("newsdate") && mc.Count>0)
                    {
                        DateTime dt = DateTime.Now;
                        if (!DateTime.TryParse(paramDic["newsdate"], out dt))
                            dt = DateTime.Now;
                        if (mc[0].ToString() == "{week}")
                        {
                            int weeknum = (int)dt.DayOfWeek;
                            DateTime monday = dt.AddDays(1 - weeknum);
                            if (weeknum == 0)//周日
                                monday = dt.AddDays(weeknum - 6);
                            var weekPath = string.Format(template.HtmlPath.Replace("{week}","week"),monday);
                            targetFilePath = saveBasePath + "/" + template.SiteName + weekPath;
                            ftpUploadPath = weekPath;
                        }
                        else if (mc[0].ToString() == "{day}")
                        {
                            var dayPath = string.Format(template.HtmlPath.Replace("{day}", "{0:yyyyMM}"), dt);
                            targetFilePath = saveBasePath + "/" + template.SiteName + dayPath;
                            ftpUploadPath = dayPath;
                        }
                    }

                    if (htmlList.Count == 1)
                    {
                        FileHelper.WriteFile(targetFilePath, htmlList[0], template.Encoding);
                        Loger.Info(string.Format("方法名：{0}，生成模板Id：{1}，模板名称：{2}，生成文件路径：{3}成功！", "CreateTemplatePage", template.TemplateId.ToString(), template.TemplateName, targetFilePath));
                        //ftp上传操作                        
                        flag = new FileReceive().FtpUpload(targetFilePath, ftpUploadPath, template.SiteName, out errorinfo);
                    }
                    else
                    {
                        for (int i = 0; i < htmlList.Count; i++)
                        {
                            var htmlPath = template.HtmlPath;
                            var targetlistFilePath = targetFilePath;
                            if (i > 0)//非第一页
                            {
                                //targetlistFilePath = targetFilePath.Replace(".html", "") + "_" + (i + 1).ToString() + ".html";
                                htmlPath = htmlPath.Replace(".html", "") + "_" + (i + 1).ToString() + ".html";
                                targetlistFilePath = saveBasePath+"/" + template.SiteName + htmlPath;
                            }
                            FileHelper.WriteFile(targetlistFilePath, htmlList[i], template.Encoding);
                            Loger.Info(string.Format("方法名：{0}，生成模板Id：{1}，模板名称：{2}，第{3}页文件，生成文件路径：{4}成功！", "CreateTemplatePage", template.TemplateId.ToString(), template.TemplateName, (i + 1).ToString(), targetlistFilePath));
                            //ftp上传操作
                            flag = new FileReceive().FtpUpload(targetlistFilePath, htmlPath, template.SiteName, out errorinfo);
                        }
                    }
                    return true;
                }
                else
                {
                    logPageTemplate(template);
                    return false;
                }
            }
            catch (Exception ex)
            {
                string info = string.Empty;
                if (template != null)
                    info = string.Format("方法名：{0}，生成模板Id：{1}，模板名称：{2}，模板路径：{3}失败！", "CreateTemplatePage", template.TemplateId.ToString(), template.TemplateName, template.TemplateFileName);
                Loger.Error(ex, info);
                return false;
            }
        }

        /// <summary>
        /// 生成和channelId有关的模板页面
        /// </summary>
        /// <param name="channelId"></param>
        /// <returns></returns>
        public bool CreateTemplatePageByChannelId(string channelId,Dictionary<string,string> paramDic = null)
        {
            SqlWhereList condition = new SqlWhereList();
            condition.Add("ChannelId", channelId);
            condition.Add("RelationType", 1);//涉及
            List <TemplateDetail> detailList = templateDetailBLL.GetAll(condition);
            foreach (TemplateDetail detail in detailList)
            {
                CreateTemplatePage(detail.TemplateId.Value,paramDic);
            }
            return true;
        }

        /// <summary>
        /// 得到模板的预览内容
        /// </summary>
        /// <param name="templateId"></param>
        /// <returns></returns>
        public string GetTemplagePreview(int templateId, DateTime? reDate)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("TemplateId", templateId);
            PageTemplate template = pageTemplateBLL.Get(dictionary);
            try
            {
                TemplateManager manager = createManager(template, true);
                //manager.RegisterCustomTag("include", new IncludeTagHandler());   //注册文件引用标签
                manager.SetValue("maketype", 1);
                manager.SetValue("curdate", DateTime.Now);
                manager.SetValue("begindate", reDate);  //需要预览的对应日期
                List<string> htmlList = manager.Process();
                string result = htmlList[0];
                Loger.Info(string.Format("方法名：{0}，预览模块Id：{1}，模板名称：{2}，模板路径：{3}成功", "GetTemplagePreview", template.TemplateId, template.TemplateName, template.TemplateFileName));
                return result;
            }
            catch (Exception ex)
            {
                Loger.Error(ex, string.Format("方法名：{0}，预览模块Id：{1}，模板名称：{2}，模板路径：{3}失败", "GetTemplagePreview", template.TemplateId, template.TemplateName, template.TemplateFileName));
                return ex.Message + ex.StackTrace;
            }
        }

        private TemplateManager createManager(PageTemplate pageTemplate, bool isPreview)
        {
            if (pageTemplate == null)
                throw new ArgumentNullException("pageTemplate");
            string path = templateBasePath + pageTemplate.TemplateFileName;
            TemplateManager manager = TemplateManager.FromFile(path, isPreview);
            if (manager == null)
                throw new ArgumentNullException("manager");
            return manager;
        }

        private void logPageTemplate(PageTemplate template)
        {
            if (template == null)
            {
                Loger.Error("模板不存在");
            }
            else
            {
                if (!template.Status.Value)
                    Loger.Error(string.Format("模板{0},状态不可用", template.TemplateFileName));
            }
        }

        private News getNews(int newsId)
        {
            NewsBLL newsBll = Factory.BusinessFactory.CreateBll<NewsBLL>();
            return newsBll.GetNewsInfo(newsId);
        }


        //private TemplateManager CreateManager(string path, bool isPreview)
        //{
        //    string data = File.ReadAllText(path, Encoding.GetEncoding("gb2312"));

        //    TemplateLexer lexer = new TemplateLexer(data);
        //    TemplateParser parser = new TemplateParser(lexer);
        //    List<Element> elems = parser.Parse();//将token流转化为element流

        //    TagParser tagParser = new TagParser(elems);
        //    elems = tagParser.CreateHierarchy();//构建语法树，明确element父子关系

        //    UserTemplate template = new UserTemplate("", elems);

        //    return new TemplateManager(template, isPreview);
        //}

        //public void MakeAnalystJson(int top)
        //{
        //    string fulePath = saveBasePath + "file\\AnalystLive.txt";
        //    AnalystLiveBLL analystLiveBLL = Factory.BusinessFactory.CreateBll<AnalystLiveBLL>();
        //    List<AnalystLive> list = analystLiveBLL.GetAnalystLiveList(top);

        //    FileHelper.WriteFile(fulePath, JsonHelper.ToJson(list),"gb2312");
        //    Loger.Info(string.Format("生成文件{0}成功", fulePath));
        //}

    }
}
