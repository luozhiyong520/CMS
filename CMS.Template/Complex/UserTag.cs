using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using CMS.Template.Parser.AST;
using CMS.Template.Except;

using System.Collections;
using System.Data;
using System.Net;

using CMS.Model;
using CMS.BLL;
using Common;
using System.IO;

namespace CMS.Template.Complex
{
    /// <summary>
    /// 自己扩展的方法类
    /// </summary>
    public partial class TemplateManager
    {
        protected void ProcessUptag(Tag tag)
        {
            Expression tagtype = tag.AttributeValue("tagtype");
            if (tagtype == null)
            {
                throw new TemplateRuntimeException("uptag 语法中必须定义属性: tagtype.", tag.Line, tag.Col);
            }
          
            string uptagtype = ((StringLiteral)tagtype).Content;
            switch (uptagtype)
            {
                case "01": //文章
                    renderNews(tag);
                    break;
                case "02": //碎片
                    reunderFragment(tag);
                    break;
                case "03": //指定页面 
                    appointPage(tag);
                    break;
                case "04": //风向标数据表
                    directionFragment(tag);
                    break;
                case "05":
                    //抓取远程网页内容
                    renderRemoteSource(tag);
                    break;
                case "06": //直播模块
                    makeModule(tag);
                    break;
                case "07": //专题列表
                    Topiclist(tag);
                    break;
                case "08": //课堂列表
                    makeClass(tag);
                    break;
                case "09":
                    makeClassStatus(tag);
                    break;
            }
        }

        /// <summary>
        /// 得到详细页的内容列表
        /// </summary>
        /// <param name="loop"></param>
        /// <returns></returns>
        //public List<string> Process(int loop)
        //{
        //    List<string> result = new List<string>();
        //    StringWriter writer = null;
        //    SetValue("pagecount", loop.ToString());
        //    var m = 0;
        //    for (int i = 0; i < loop; i++)
        //    {
        //        m = i + 1;
        //        SetValue("curpage", m.ToString());
        //        writer = new StringWriter();
        //        Process(writer);
        //        result.Add(writer.ToString());              
        //    }
        //    return result;
        //}

      

        private object getNews(Tag tag)
        {
            if (!variables.IsDefined("curpage")) 
                throw new TemplateRuntimeException("Process()方法中必须设置curpage变量值",tag.Line,tag.Col);
            string clsid = ((StringLiteral)tag.AttributeValue("clsid")).Content;
            int pagesize = int.Parse(((StringLiteral)tag.AttributeValue("pagesize")).Content);
            int pagecount = int.Parse(((StringLiteral)tag.AttributeValue("pagecount")).Content);
            string dataId = ((StringLiteral)tag.AttributeValue("id")).Content;

            int newssort = 1000;//0-空文章,1-新文章,2-图片文章。值为1000时，就全选
            if (tag.AttributeValue("newssort") != null)
                newssort = int.Parse(((StringLiteral)tag.AttributeValue("newssort")).Content);

            var beginData = string.Empty; //查询新闻的开始时间
            if (GetValue("begindate") != null)
                beginData = GetValue("begindate").ToString();
            if (tag.AttributeValue("begindate") != null)
                beginData = ((StringLiteral)tag.AttributeValue("begindate")).Content;
            if (beginData == "now")
                beginData = DateTime.Now.ToString();
            if (beginData == "today")
                beginData = DateTime.Now.ToString("yyyy-MM-dd");
            if (beginData=="mdate")
                beginData = DateTime.Parse(GetValue("newsdate").ToString()).ToString("yyyy-MM-dd");
            
            object collection = null;
            int pageIndex = int.Parse(variables["curpage"].ToString());

            if (!variables.IsDefined("pagecount"))
            {
                SetValue("pagecount", 1);
            }
            if (tag.AttributeValue("week") != null)
            {
                DateTime dt = DateTime.Now;
                if (!DateTime.TryParse(beginData, out dt))
                {
                    dt = DateTime.Now;
                }

                //如果模板里面未定义开始时间，那就以新闻的发布时间作为开始时间
                if (string.IsNullOrEmpty(beginData) && variables.IsDefined("newsdate"))
                {
                    if (!DateTime.TryParse(GetValue("newsdate").ToString(), out dt))
                    {
                        dt = DateTime.Now;
                    }
                }
                int weeknum = (int)dt.DayOfWeek;
                var week = ((StringLiteral)tag.AttributeValue("week")).Content;
                if (weeknum == 0) //周日
                {
                    switch (week)
                    {
                        case "week0": //周日
                            beginData = dt.ToString("yyyy-MM-dd");
                            break;
                        case "week1": //周一
                            beginData = dt.AddDays(-6).ToString("yyyy-MM-dd");
                            break;
                        case "week2": //周二
                            beginData = dt.AddDays(-5).ToString("yyyy-MM-dd");
                            break;
                        case "week3": //周三
                            beginData = dt.AddDays(-4).ToString("yyyy-MM-dd");
                            break;
                        case "week4": //周四
                            beginData = dt.AddDays(-3).ToString("yyyy-MM-dd");
                            break;
                        case "week5": //周五
                            beginData = dt.AddDays(-2).ToString("yyyy-MM-dd");
                            break;
                        case "week6": //周六
                            beginData = dt.AddDays(-1).ToString("yyyy-MM-dd");
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    switch (week)
                    {
                        case "week0": //周日
                            beginData = dt.AddDays(7 - weeknum).ToString("yyyy-MM-dd");
                            break;
                        case "week1": //周一
                            beginData = dt.AddDays(1 - weeknum).ToString("yyyy-MM-dd");
                            break;
                        case "week2": //周二
                            beginData = dt.AddDays(2 - weeknum).ToString("yyyy-MM-dd");
                            break;
                        case "week3": //周三
                            beginData = dt.AddDays(3 - weeknum).ToString("yyyy-MM-dd");
                            break;
                        case "week4": //周四
                            beginData = dt.AddDays(4 - weeknum).ToString("yyyy-MM-dd");
                            break;
                        case "week5": //周五
                            beginData = dt.AddDays(5 - weeknum).ToString("yyyy-MM-dd");
                            break;
                        case "week6": //周六
                            beginData = dt.AddDays(6 - weeknum).ToString("yyyy-MM-dd");
                            break;
                        default:
                            break;
                    }
                }
            }
           
            NewsBLL newsBll = Factory.BusinessFactory.CreateBll<NewsBLL>();

            PagedResult<News> result = newsBll.GetFrontNewsListIn(pageIndex, pagesize, beginData, beginData, clsid, pagecount > 1 ? 1 : 0, newssort);
            if (result.Result == null)
            {
                SetValue("total", 0);
                collection = new List<News>();
            }
            else
            {
                SetValue("total", result.Result.Count);
                collection = result.Result;
            }
            if (pagecount > 1)
            {
                //数据库真实的总页数
                int pageTotal = result.Total % pagesize > 0 ? result.Total / pagesize + 1 : result.Total / pagesize;
                if (pageTotal > 0 && pageTotal < pagecount)
                {
                    pagecount = pageTotal;                    
                }
                if (pagecount > int.Parse(variables["pagecount"].ToString()))
                    SetValue("pagecount", pagecount.ToString());
            }
            
            SetValue(dataId, collection);
            if (!(collection is IEnumerable))
            {
                throw new TemplateRuntimeException("循环中的集合对象必须是enumerable类型或继承至该类型.", tag.Line, tag.Col);
            }
            return collection;
        }

        private void renderNews(Tag tag)
        {
            object collection = getNews(tag);
            Expression expVar = tag.AttributeValue("var");
            if (expVar == null)
            {
                throw new TemplateRuntimeException("foreach 语法缺少 var 属性定义.", tag.Line, tag.Col);
            }
            object varObject = EvalExpression(expVar);
            if (varObject == null)
                varObject = "foreach";
            string varname = varObject.ToString();

            Expression expIndex = tag.AttributeValue("index");
            string indexname = null;
            if (expIndex != null)
            {
                object obj = EvalExpression(expIndex);
                if (obj != null)
                    indexname = obj.ToString();
            }

            //判断是否循环的最后一条标识
            Expression islastExp = tag.AttributeValue("islast");
            string islast = null;
            if (islastExp != null)
            {
                object obj = EvalExpression(islastExp);
                if (obj != null)
                    islast = obj.ToString();
            }
            IEnumerator ienum = ((IEnumerable)collection).GetEnumerator();
            int index = 0;
            if(islast!=null)
                variables[islast] = false;
            while (ienum.MoveNext())
            {                
                index++;
                if (index == ((List<News>)collection).Count && islast != null)
                    variables[islast] = true;
                object value = ienum.Current;
                variables[varname] = value;
                if (indexname != null)
                    variables[indexname] = index;

                ProcessElements(tag.InnerElements);
            }
        }

        /// <summary>
        /// 生成特定模版页
        /// </summary>
        /// <param name="tag"></param>
        private void appointPage(Tag tag)
        {
            object collection=null;
            string clsid = ((StringLiteral)tag.AttributeValue("clsid")).Content;
            string dataId = ((StringLiteral)tag.AttributeValue("id")).Content;
            int count = int.Parse(((StringLiteral)tag.AttributeValue("count")).Content);
            if (count.ToString()==null)
            {
                count = 1;
            }
            NewsBLL newsBll = Factory.BusinessFactory.CreateBll<NewsBLL>();
            List<News> list = newsBll.GetNewsOneInfo(clsid, count);

            SetValue(dataId, list);
            collection = list;

            Expression expVar = tag.AttributeValue("var");
            if (expVar == null)
            {
                throw new TemplateRuntimeException("foreach 语法缺少 var 属性定义.", tag.Line, tag.Col);
            }

            object varObject = EvalExpression(expVar);
            if (varObject == null)
                varObject = "foreach";
            string varname = varObject.ToString();
            Expression expIndex = tag.AttributeValue("index");
            string indexname = null;
            if (expIndex != null)
            {
                object obj = EvalExpression(expIndex);
                if (obj != null)
                    indexname = obj.ToString();
            }
            IEnumerator ienum = ((IEnumerable)collection).GetEnumerator();
            int index = 0;
            while (ienum.MoveNext())
            {
                index++;
                object value = ienum.Current;
                variables[varname] = value;
                if (indexname != null)
                    variables[indexname] = index;

                ProcessElements(tag.InnerElements);
            }
          
          
        }

        /// <summary>
        /// 分析师直播
        /// </summary>
        /// <param name="tag"></param>
        private void makeModule(Tag tag)
        {
            object collection = null;
             List<AnalystLive> list;
            string modudle = ((StringLiteral)tag.AttributeValue("modudle")).Content;
            string dataId = ((StringLiteral)tag.AttributeValue("id")).Content;
            int count = int.Parse(((StringLiteral)tag.AttributeValue("count")).Content);
            if (count.ToString() == null)
            {
                count = 1;
            }
            AnalystLiveBLL analystLiveBLL = Factory.BusinessFactory.CreateBll<AnalystLiveBLL>();
            list = analystLiveBLL.GetAnalystLiveListNews(count,modudle);

            if (list == null)
            {
                return;
            }
            else
            {
                collection = list;
            }
             SetValue(dataId, collection);

            Expression expVar = tag.AttributeValue("var");
            if (expVar == null)
            {
                throw new TemplateRuntimeException("foreach 语法缺少 var 属性定义.", tag.Line, tag.Col);
            }

            object varObject = EvalExpression(expVar);
            if (varObject == null)
                varObject = "foreach";
            string varname = varObject.ToString();
            Expression expIndex = tag.AttributeValue("index");
            string indexname = null;
            if (expIndex != null)
            {
                object obj = EvalExpression(expIndex);
                if (obj != null)
                    indexname = obj.ToString();
            }
            //判断是否循环的最后一条标识
            Expression islastExp = tag.AttributeValue("islast");
            string islast = null;
            if (islastExp != null)
            {
                object obj = EvalExpression(islastExp);
                if (obj != null)
                    islast = obj.ToString();
            }
            IEnumerator ienum = ((IEnumerable)collection).GetEnumerator();
            int index = 0;
            if(islast!=null)
                variables[islast] = false;
            while (ienum.MoveNext())
            {                
                index++;
                if (index == ((List<AnalystLive>)collection).Count && islast != null)
                    variables[islast] = true;
                object value = ienum.Current;
                variables[varname] = value;
                if (indexname != null)
                    variables[indexname] = index;

                ProcessElements(tag.InnerElements);
            }


        }

        /// <summary>
        /// 专题列表
        /// </summary>
        /// <param name="tag"></param>
        private void Topiclist(Tag tag)
        {
            object collection = null;
            PagedResult<Topic> result;
            string topictype = ((StringLiteral)tag.AttributeValue("topictype")).Content;
            string dataId = ((StringLiteral)tag.AttributeValue("id")).Content;
           // int count = int.Parse(((StringLiteral)tag.AttributeValue("count")).Content);
            int pagesize = int.Parse(((StringLiteral)tag.AttributeValue("pagesize")).Content);
            int pagecount = int.Parse(((StringLiteral)tag.AttributeValue("pagecount")).Content);
            //if (count.ToString() == null)
            //{
            //    count = 1;
            //}
            TopicBLL topicBLL = Factory.BusinessFactory.CreateBll<TopicBLL>();
            result = topicBLL.GetTopicFrontList("", topictype, pagecount, pagesize);

            if (result.Result == null)
            { 
                SetValue("total", 0);
                collection = new List<News>();
            }
            else
            {
                SetValue("total", result.Result.Count);
                collection = result.Result;
            }
            if (pagecount > 1)
            {
                //数据库真实的总页数
                int pageTotal = result.Total % pagesize > 0 ? result.Total / pagesize + 1 : result.Total / pagesize;
                if (pageTotal > 0 && pageTotal < pagecount)
                {
                    pagecount = pageTotal;
                }
                if (pagecount > int.Parse(variables["pagecount"].ToString()))
                    SetValue("pagecount", pagecount.ToString());
            }

            SetValue(dataId, collection);

            Expression expVar = tag.AttributeValue("var");
            if (expVar == null)
            {
                throw new TemplateRuntimeException("foreach 语法缺少 var 属性定义.", tag.Line, tag.Col);
            }

            object varObject = EvalExpression(expVar);
            if (varObject == null)
                varObject = "foreach";
            string varname = varObject.ToString();
            Expression expIndex = tag.AttributeValue("index");
            string indexname = null;
            if (expIndex != null)
            {
                object obj = EvalExpression(expIndex);
                if (obj != null)
                    indexname = obj.ToString();
            }
            //判断是否循环的最后一条标识
            Expression islastExp = tag.AttributeValue("islast");
            string islast = null;
            if (islastExp != null)
            {
                object obj = EvalExpression(islastExp);
                if (obj != null)
                    islast = obj.ToString();
            }
            IEnumerator ienum = ((IEnumerable)collection).GetEnumerator();
            int index = 0;
            if (islast != null)
                variables[islast] = false;
            while (ienum.MoveNext())
            {
                index++;
                if (index == ((List<Topic>)collection).Count && islast != null)
                    variables[islast] = true;
                object value = ienum.Current;
                variables[varname] = value;
                if (indexname != null)
                    variables[indexname] = index;

                ProcessElements(tag.InnerElements);
            }


        }

        /// <summary>
        /// 风向标数据标页面
        /// </summary>
        /// <param name="tag"></param>
        private void directionFragment(Tag tag)
        {
            object collection = null;
            string clsid = ((StringLiteral)tag.AttributeValue("clsid")).Content;
            string dataId = ((StringLiteral)tag.AttributeValue("id")).Content;
            //int count = int.Parse(((StringLiteral)tag.AttributeValue("count")).Content);
            //if (count.ToString() == null)
            //{
            //    count = 1;
            //}
            DirectionBLL directionBLL = Factory.BusinessFactory.CreateBll<DirectionBLL>();
            SqlWhereList where = new SqlWhereList();
            where.Add("TypeId", clsid);
            List<Direction> list=  directionBLL.GetAll(where);
           // NewsBLL newsBll = Factory.BusinessFactory.CreateBll<NewsBLL>();
           // List<News> list = newsBll.GetNewsOneInfo(clsid, count);

            SetValue(dataId, list);
            collection = list;

            Expression expVar = tag.AttributeValue("var");
            if (expVar == null)
            {
                throw new TemplateRuntimeException("foreach 语法缺少 var 属性定义.", tag.Line, tag.Col);
            }

            object varObject = EvalExpression(expVar);
            if (varObject == null)
                varObject = "foreach";
            string varname = varObject.ToString();
            Expression expIndex = tag.AttributeValue("index");
            string indexname = null;
            if (expIndex != null)
            {
                object obj = EvalExpression(expIndex);
                if (obj != null)
                    indexname = obj.ToString();
            }
            IEnumerator ienum = ((IEnumerable)collection).GetEnumerator();
            int index = 0;
            while (ienum.MoveNext())
            {
                index++;
                object value = ienum.Current;
                variables[varname] = value;
                if (indexname != null)
                    variables[indexname] = index;

                ProcessElements(tag.InnerElements);
            }
        }

        private void reunderFragment(Tag tag)
        {
            string clsid = ((StringLiteral)tag.AttributeValue("clsid")).Content;
            bool isViewEdit = ((StringLiteral)tag.AttributeValue("viewedit")).Content == "1";
            if (isViewEdit && isPreview)
            {
                WriteValue("<div deit=\"deit\" channelID=\"" + clsid + "\">");
                //StringBuilder layer = new StringBuilder();
                //layer.AppendFormat("<div id=\"makelistv2peripheral{0}\" onmouseover=\"makeListV2SetBG1(this,'makelistv2editregional{0}')\" onmouseout=\"makeListV2SetBG2(this,'makelistv2editregional{0}')\">", clsid);
                //layer.AppendFormat("<div style=\"display: none;\" id=\"makelistv2editregional{0}\" onclick=\"makeListV2editspec('{0}','0')\"></div>", clsid);
                //WriteValue(layer.ToString());
            }
            object collection = getFragment(tag);
            Expression expVar = tag.AttributeValue("var");
            if (expVar == null)
            {
                throw new TemplateRuntimeException("foreach 语法缺少 var 属性定义.", tag.Line, tag.Col);
            }
            object varObject = EvalExpression(expVar);
            string varname = varObject.ToString();
            IEnumerator ienum = ((IEnumerable)collection).GetEnumerator();

            while (ienum.MoveNext())
            {
                object value = ienum.Current;
                variables[varname] = value;
                ProcessElements(tag.InnerElements);
            }
            if (isViewEdit && isPreview)
            {
                WriteValue("</div>");
            }

        }

        private void renderRemoteSource(Tag tag)
        {
            string strSiteUrl = ((StringLiteral)tag.AttributeValue("infourl")).Content;
            WebClient myWebClient = new WebClient
            {
                Credentials = CredentialCache.DefaultCredentials,
                Encoding = Encoding.Default
            };
            string strResult;
            try
            {
                strResult = myWebClient.DownloadString(strSiteUrl);
                writer.Write(strResult);
            }
            catch (Exception)
            {
                DisplayError(tag.Name + "标签执行错误,infourl 指向的页面不存在", tag.Line, tag.Col);
            }
        }

        private object getFragment(Tag tag)
        {
            string clsid = ((StringLiteral)tag.AttributeValue("clsid")).Content;
            string dataId = ((StringLiteral)tag.AttributeValue("id")).Content;
            object collection = null;
            FragmentBLL fragmentBLL = Factory.BusinessFactory.CreateBll<FragmentBLL>();
            Fragment fragment = fragmentBLL.GetLastOneFrgment(clsid);
            List<Fragment> list = new List<Fragment>();
            if (fragment != null)
                list.Add(fragment);
            SetValue(dataId, list);
            collection = list;
            return collection;
        }

        /// <summary>
        /// 课程计划页面
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        private object makeClass(Tag tag)
        {
            object collection = null;
            List<CourseProgram> list;
            string softVersion = ((StringLiteral)tag.AttributeValue("softVersion")).Content;
            //if (softVersion == "jhd")
            //{
            //    softVersion = "金蝴蝶";
            //}
            //else if (softVersion == "jmd")
            //{
            //    softVersion = "金牡丹";
            //}
            //else {
            //    softVersion = "百川";
            //}
            CourseProgramBLL courseProgramBLL = Factory.BusinessFactory.CreateBll<CourseProgramBLL>();
            list = courseProgramBLL.GetCourseProgram(softVersion);
            collection = list;
            //Common.Loger.Info(softVersion);
            Expression expVar = tag.AttributeValue("var");
            if (expVar == null)
            {
                throw new TemplateRuntimeException("foreach 语法缺少 var 属性定义.", tag.Line, tag.Col);
            }

            object varObject = EvalExpression(expVar);
            if (varObject == null)
                varObject = "foreach";
            string varname = varObject.ToString();
            Expression expIndex = tag.AttributeValue("index");
            string indexname = null;
            if (expIndex != null)
            {
                object obj = EvalExpression(expIndex);
                if (obj != null)
                    indexname = obj.ToString();
            }
            //判断是否循环的最后一条标识
            Expression islastExp = tag.AttributeValue("islast");
            string islast = null;
            if (islastExp != null)
            {
                object obj = EvalExpression(islastExp);
                if (obj != null)
                    islast = obj.ToString();
            }
            IEnumerator ienum = ((IEnumerable)collection).GetEnumerator();
            int index = 0;
            if (islast != null)
                variables[islast] = false;
            while (ienum.MoveNext())
            {
                index++;
                if (index == ((List<CourseProgram>)collection).Count && islast != null)
                    variables[islast] = true;
                object value = ienum.Current;
                variables[varname] = value;
                if (indexname != null)
                    variables[indexname] = index;

                ProcessElements(tag.InnerElements);
            }

            return collection;        
        }


        public object makeClassStatus(Tag tag)
        {
            object collection = null;
            List<CourseRecommend> list;
            string softVersion = ((StringLiteral)tag.AttributeValue("softVersion")).Content;

            CourseRecommendBLL crBLL = Factory.BusinessFactory.CreateBll<CourseRecommendBLL>();
            list= crBLL.GetCourse(softVersion, 1);
            if (list != null)
                collection = list;
            else
                return "";
            //Common.Loger.Info(softVersion);
            Expression expVar = tag.AttributeValue("var");
            if (expVar == null)
            {
                throw new TemplateRuntimeException("foreach 语法缺少 var 属性定义.", tag.Line, tag.Col);
            }

            object varObject = EvalExpression(expVar);
            if (varObject == null)
                varObject = "foreach";
            string varname = varObject.ToString();
            Expression expIndex = tag.AttributeValue("index");
            string indexname = null;
            if (expIndex != null)
            {
                object obj = EvalExpression(expIndex);
                if (obj != null)
                    indexname = obj.ToString();
            }
            //判断是否循环的最后一条标识
            Expression islastExp = tag.AttributeValue("islast");
            string islast = null;
            if (islastExp != null)
            {
                object obj = EvalExpression(islastExp);
                if (obj != null)
                    islast = obj.ToString();
            }
            IEnumerator ienum = ((IEnumerable)collection).GetEnumerator();
            int index = 0;
            if (islast != null)
                variables[islast] = false;
            while (ienum.MoveNext())
            {
                index++;
                if (index == ((List<CourseRecommend>)collection).Count && islast != null)
                    variables[islast] = true;
                object value = ienum.Current;
                variables[varname] = value;
                if (indexname != null)
                    variables[indexname] = index;

                ProcessElements(tag.InnerElements);
            }

            return collection;        
        }

        /// <summary>
        /// 获取模板中最大的pagecount数，用来在Process()方法里面的循环上限数
        /// </summary>
        /// <returns></returns>
        private int getUpTagMaxPagecount()
        {
            List<Element> list = mainTemplate.Elements;
            if (list == null)
                return 1; //默认循环上限数为1
            List<Tag> tagList = new List<Tag>();
            list.FindAll(a => (a as Tag) != null).FindAll(a => ((Tag)a).AttributeValue("pagecount") != null).ForEach(a => tagList.Add((Tag)a));
            if (tagList.Count == 0)
                return 1;
            else
            {
                string str = tagList.Max(t => ((StringLiteral)t.AttributeValue("pagecount")).Content);
                return int.Parse(str);
            }
            
        }
    }
}
