using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMS.Model;
using CMS.BLL;
using Common;
using CMS.ViewModel;
using System.Data;
using SMVC;



namespace CMS.Controller
{
    public class QuestionnaAnswerController
    {
        QuestionnairesAnswerBLL questionnairesanswerBLL = Factory.BusinessFactory.CreateBll<QuestionnairesAnswerBLL>();
        QuestionnairesBLL questionnairesBLL = Factory.BusinessFactory.CreateBll<QuestionnairesBLL>(true);
        QuestionnairesOptionsBLL questionnaires_OptionsBLL = Factory.BusinessFactory.CreateBll<QuestionnairesOptionsBLL>(true);

        [Action]
        [PageUrl(Url = "/statistics/questionnairesanswer.aspx")]
        //[PageUrl(Url = "/sasweb/xysidkdydnhensydn_cdhds.dyshg/dsfyewlrndsfpoidsfewlkdsnf.cxgdsf_hdsfnew_gz/statistics/questionnairesanswer.aspx")]

        public object GetQuestionnairesAnswerList(int qId, string start, string end)
        {
            QuestionnairesAnswerPageModel model = new QuestionnairesAnswerPageModel();
            model.QuestionnairesList = questionnairesBLL.GetQuestionnairesList(0);

            qId = qId == 0 ? 1 : qId;
            model.CurrentQid = qId;
            int UserNum;
            UserNum = questionnairesanswerBLL.GetSurveyed(qId, start, end);
            if (UserNum == 0)
                return new PageResult(null, model);

            List<QuestionnairesAndOptions> listqo = new List<QuestionnairesAndOptions>();
            //参与调查人数
            model.UserNum = UserNum;

            //调查结果统计（主观题）
            model.QuestionnairesanswerList = questionnairesanswerBLL.GetyAllProposal(qId, start, end);
 
            List<Questionnaires> listQues = questionnairesBLL.GetQuestionnairesList(qId);
            foreach (var item in listQues)
            {
                QuestionnairesAndOptions quoptions = new QuestionnairesAndOptions();
                quoptions.Questionnaires = item;
                quoptions.ListOptions = GetQuestionnairesOptionsList(item.QId, model.UserNum, start, end);
                listqo.Add(quoptions);
            }
            model.QuestionnairesAndOptionsList = listqo;
            return new PageResult(null, model);
        }
        /// <summary>
        /// 获取问题选项列表及人数比例
        /// </summary>
        /// <param name="questionnairesId"></param>
        /// <returns></returns>
        public  List<QuestionnairesOptions> GetQuestionnairesOptionsList(int QId, int userNum, string strat, string end)
        {
            if(QId == 49)
               return null;
            List<QuestionnairesOptions> list = questionnaires_OptionsBLL.GetQuestionnairesOptionsList(QId);

            foreach (var item in list)
            {
                item.OptionsNum = questionnairesanswerBLL.GetAnswerCount(item.QoId, strat, end);
                item.OptionsRate = Convert.ToDouble(item.OptionsNum) / userNum * 100;
            }
            return list;
        }

        [Action]
        [PageUrl(Url = "/statistics/index.aspx")]
        [PageUrl(Url = "/statistics/questionnaireEdit.aspx")]
        public object GetQuestionnaires(int pId)
        {
            QuestionnairePageModel model = new QuestionnairePageModel();
            model.Questions = questionnairesBLL.GetQuestionnairesListNoCache(pId).OrderBy(o => o.Orders).ToList();
            if (pId != 0)
            {
                var obj= questionnairesBLL.Get("Qid", pId);
                model.QId = obj.QId;
                model.Tit = obj.Title;
                model.Desc = obj.Description;
            }

            return new PageResult(null, model);
 
        }
    }
}
