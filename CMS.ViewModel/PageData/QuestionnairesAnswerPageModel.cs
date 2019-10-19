using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMS.Model;
using System.Data;


namespace CMS.ViewModel
{
    public class QuestionnairesAnswerPageModel
    {
        /// <summary>
        /// 选项列表
        /// </summary>
        public List<QuestionnairesAndOptions> QuestionnairesAndOptionsList { get; set; }
        /// <summary>
        /// 总人数
        /// </summary>
        public int UserNum { get; set; }
        public int AnswerUser { get; set; }
        //public float Proportion { get; set; }
        public QuestionnairesAnswerPageModel() { }
        public List<QuestionnairesAnswer> QuestionnairesanswerList { get; set; }
        public int CurrentQid { get; set; }
        /// <summary>
        /// 问卷
        /// </summary>
        public List<Questionnaires> QuestionnairesList { get; set; }
    }
}
