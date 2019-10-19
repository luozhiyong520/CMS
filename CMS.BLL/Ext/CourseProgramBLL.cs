using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMS.DAL;
using CMS.Model;

namespace CMS.BLL
{
    public partial class CourseProgramBLL
    {
        /// <summary>
        /// 获取对应版本的课程计划
        /// </summary>
        /// <param name="softVersion">软件版本</param>
        public List<CourseProgram> GetCourseProgram(string softVersion)
        {
            return dal.GetCourseProgram(softVersion);
        }

        /// <summary>
        /// 获取所有版本
        /// </summary>
        public string GetVersion()
        {
            return dal.GetVersion();
        }

        /// <summary>
        /// 获取此版本下的所有类别课程
        /// </summary>
        public List<CourseProgram> GetCourseType(string softVersion)
        {
            return dal.GetCourseType(softVersion);
        }

        /// <summary>
        /// 更新此版本课程排序
        /// </summary>
        public int UpdateCourseType(string softVersion, string setVel)
        {
            return dal.UpdateCourseType(softVersion, setVel);
        }

        /// <summary>
        /// 删除单条记录
        /// </summary>
        public int Delete(int id)
        {
            return dal.Delete(id);
        }
    }
}
