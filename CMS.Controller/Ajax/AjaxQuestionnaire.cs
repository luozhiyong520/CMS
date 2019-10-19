using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMVC;
using CMS.BLL;
using CMS.ViewModel;
using Common;
using CMS.Utility;
using CMS.Model;

namespace CMS.Controller
{
    [Authorize]
    public class AjaxQuestionnaire
    {
        QuestionnairesBLL questionnaireBLL = Factory.BusinessFactory.CreateBll<QuestionnairesBLL>();
        QuestionnairesOptionsBLL questionnaireOptionBLL = Factory.BusinessFactory.CreateBll<QuestionnairesOptionsBLL>();

        [Action]
        public int AddQuestionnaire(int pId, string tit, string desc, string type)
        {
            Questionnaires obj = new Questionnaires() { ParentId = pId, Title = tit, Description = desc, OptType = type };
            return questionnaireBLL.Add(obj);
        }

        [Action]
        public int UpdateQuestionnaire(int qId, int pId, string tit, string desc, string type)
        {
            Questionnaires obj = new Questionnaires() { QId = qId, ParentId = pId, Title = tit, Description = desc, OptType = type };
            return questionnaireBLL.Update(obj);
        }

        [Action]
        public int DeleteQuestionnaire(int qId)
        {
            Questionnaires obj = new Questionnaires() { QId = qId };
            return questionnaireBLL.Delete(obj);
        }


        /// <summary>
        /// 增加一个选项
        /// </summary>
        /// <param name="qId"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        [Action]
        public int AddQuestionnaireOption(int qId, string info)
        {
            QuestionnairesOptions obj = new QuestionnairesOptions() { QId = qId, Info = info };
            return questionnaireOptionBLL.Add(obj);

        }

        /// <summary>
        /// 更新选项信息
        /// </summary>
        /// <param name="qoId"></param>
        /// <param name="qId"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        [Action]
        public int UpdateQuestionnaireOption(int qoId, int qId, string info)
        {
            QuestionnairesOptions obj = new QuestionnairesOptions() { QoId = qoId, QId = qId, Info = info };
            return questionnaireOptionBLL.Update(obj);
        }

        [Action]
        public int UpdateQuestionnaireOptions(int qId, string options)
        {
            int count = 0;
            if (string.IsNullOrEmpty(options)) return 0;
            string[] option = options.Split(';');
            foreach (var item in option)
            {
                if (!item.Split(',')[2].Equals("undefined"))
                    count += UpdateQuestionnaireOption(Convert.ToInt32(item.Split(',')[2]), qId, item.Split(',')[0]);
                else
                    count += AddQuestionnaireOption(qId, item.Split(',')[0]);
            }

            return count;
 
        }

        /// <summary>
        /// 删除选项
        /// </summary>
        /// <param name="qoId"></param>
        /// <returns></returns>
        [Action]
        public int DeleteQuestionnaireOption(int qoId)
        {

            QuestionnairesOptions obj = new QuestionnairesOptions() { QoId = qoId };
            return questionnaireOptionBLL.Delete(obj);
        }

        [Action]
        public JsonResult GetOptions(int qId)
        {
            SqlWhereList sql = new SqlWhereList();
            sql.Add("QId", qId);
            List<QuestionnairesOptions> objs = questionnaireOptionBLL.GetAll(sql);
            return new JsonResult(objs);
        }

        [Action]
        public JsonResult GetQuestions(int pId)
        {
            List<Questionnaires> objs = questionnaireBLL.GetQuestionnairesListNoCache(pId);
            objs = objs.OrderBy(o => o.Orders).ToList();
            return new JsonResult(objs);
        }

        [Action]
        public int UpdateQuestionsOrder(int pId,string qo)
        {
            return questionnaireBLL.UpdateQuestionsOrder(pId,qo);
        }
    }
}
