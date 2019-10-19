using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMS.Model;

namespace CMS.BLL
{
    public partial class CourseRecommendBLL
    {
        /// <summary>
        /// 删除单条记录
        /// </summary>
        public int Delete(string version)
        {
            return dal.Delete(version);
        }

        /// <summary>
        /// 更新状态
        /// </summary>
        public int Update(int crId, int status)
        {
            return dal.Update(crId, status);
        }

        public List<CourseRecommend> GetCourse(string softVersion, int status)
        {
            return dal.GetCourseRecommend(softVersion, 1);
        }
    }
}
