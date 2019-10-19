using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMVC;
using CMS.Model;
using CMS.BLL;
using Common;
using System.Web;
using CMS.Template;
using CMS.Template.Parser.AST;

namespace CMS.Controller
{
    [Authorize]
    public class CourseProgramController
    {
        CourseProgramBLL cpBll = Factory.BusinessFactory.CreateBll<CourseProgramBLL>();
        CourseRecommendBLL crBll = Factory.BusinessFactory.CreateBll<CourseRecommendBLL>();


        /// <summary>
        /// 获取对应版本的课程计划
        /// </summary>
        [Action]
        [PageUrl(Url = "/CourseProgram/GetCourseProgram.aspx")]
        public string GetCourseProgram(string version)
        {
            version = HttpUtility.UrlDecode(version);
            if (string.IsNullOrEmpty(version))
                version = "金蝴蝶";

            List<CourseProgram> listCp = new List<CourseProgram>();
            listCp = cpBll.GetCourseProgram(version);
            if (listCp == null)
                return "[]";
            return JsonHelper.ToJson(listCp);
        }

        /// <summary>
        /// 新增/编辑
        /// </summary>
        [Action]
        [PageUrl(Url = "/CourseProgram/EidtCourseProgram.aspx")]
        public int EidtCourseProgram(string cpId, CourseProgram cp)
        {
            //return 123;
            CourseProgram myCp = new CourseProgram();
            SqlWhereList swl = new SqlWhereList();
            swl.Add("SoftVersion", cp.SoftVersion);
            swl.Add("CourseType", cp.CourseType);
            myCp = cpBll.GetAll(swl).FirstOrDefault();
            int orderNum = 99;
            if (myCp != null)
                orderNum = myCp.OrderNum ?? 99;
            int res;
            if (cpId == "add")
            {
                cp.CreatedTime = DateTime.Now;
                cp.Editor = UserCookies.AdminName;
                cp.Status = 1;
                cp.OrderNum = orderNum;
                cp.ProgramUrl = cp.ProgramUrl == null ? "" : cp.ProgramUrl;
                res = cpBll.Add(cp);
            }
            else
            {
                cp.CreatedTime = DateTime.Now;
                cp.Editor = UserCookies.AdminName;
                cp.Status = 1;
                cp.OrderNum = orderNum;
                cp.ProgramUrl = cp.ProgramUrl == null ? "" : cp.ProgramUrl;
                res = cpBll.Update(cp);
            }
            CreateJsonPage(cp.SoftVersion);
            return res;
        }

        /// <summary>
        /// 获取此版本下的所有类别课程
        /// </summary>
        [Action]
        [PageUrl(Url = "/CourseProgram/GetCourseType.aspx")]
        public string GetCourseType(string version)
        {

            List<CourseProgram> listCp = new List<CourseProgram>();
            listCp = cpBll.GetCourseType(version);
            if (listCp == null)
                return "[]";
            return JsonHelper.ToJson(listCp);
        }



        /// <summary>
        /// 排序变更
        /// </summary>
        [Action]
        [PageUrl(Url = "/CourseProgram/UpdateCourseType.aspx")]
        public int UpdateCourseType(string version, string cpo)
        {
            int i = cpBll.UpdateCourseType(version, cpo);
            CreateJsonPage(version);
            return i;
        }

        /// <summary>
        /// 删除对应id记录
        /// </summary>
        [Action]
        [PageUrl(Url = "/CourseProgram/DelCourseProgram.aspx")]
        public int DelCourseProgram(int cpId, string version)
        {
            int res= cpBll.Delete(cpId);
            CreateJsonPage(version);
            return res;
        }

        /// <summary>
        /// 获取所有版本
        /// </summary>
        [Action]
        [PageUrl(Url = "/CourseProgram/GetVersion.aspx")]
        public string GetVersion()
        {
            return cpBll.GetVersion();
        }

        /// <summary>
        /// 调用生成模版方法, 生成此版本所有课程json数据
        /// </summary>
        private void CreateJsonPage(string version)
        {
            if (version == "金蝴蝶")
                HtmlServer.CreateTemplatePage(93);
            else if (version == "百川")
                HtmlServer.CreateTemplatePage(95);
            else if (version == "金牡丹")
                HtmlServer.CreateTemplatePage(94);
        }

        /// <summary>
        /// 更新推荐课堂
        /// </summary>
        [Action]
        [PageUrl(Url = "/CourseProgram/UpDataCourseRecommend.aspx")]
        public int UpDataCourseRecommend(CourseRecommend cr) 
        {
            crBll.Delete(cr.SoftVersion);
            cr.RoomId = cr.RoomId == null ? "" : cr.RoomId;
            cr.Editor = UserCookies.AdminName;
            cr.EditTime = DateTime.Now;
            CreateJsonPageStatus(cr.SoftVersion);
            return crBll.Add(cr);
        }

        /// <summary>
        /// 获取推荐课堂
        /// </summary>
        [Action]
        [PageUrl(Url = "/CourseProgram/GetCourseRecommend.aspx")]
        public string GetCourseRecommend(string version)
        {
            SqlWhereList sw = new SqlWhereList();
            sw.Add("SoftVersion", version);
            List<CourseRecommend> cr = crBll.GetAll(sw);
            if (cr == null || cr.Count == 0)
                return "[]";
            return JsonHelper.ToJson(cr);
        }


        /// <summary>
        /// 更新推荐课堂状态
        /// </summary>
        [Action]
        [PageUrl(Url = "/CourseProgram/UpDataCourseRecommendStatus.aspx")]
        public int UpDataCourseRecommendStatus(int crId, int status, string version)
        {
            int crt = crBll.Update(crId, status);
            CreateJsonPageStatus(version);
            return crt;
        }

        /// <summary>
        /// 生成推荐课堂json数据文件
        /// </summary>
        private void CreateJsonPageStatus(string version)
        {
            if (version == "金蝴蝶")
                HtmlServer.CreateTemplatePage(96);
            else if (version == "百川")
                HtmlServer.CreateTemplatePage(98);
            else if (version == "金牡丹")
                HtmlServer.CreateTemplatePage(97);
        }
    }
}
