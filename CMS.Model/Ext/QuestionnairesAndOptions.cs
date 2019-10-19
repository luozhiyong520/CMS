using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMS.Model;

namespace CMS.Model
{
    public class QuestionnairesAndOptions
    {
        /// <summary>
        /// 问题
        /// </summary>
        public Questionnaires Questionnaires{get; set;}

        /// <summary>
        /// 问题的选项
        /// </summary>
        public List<QuestionnairesOptions> ListOptions { get; set; }
    }
}
