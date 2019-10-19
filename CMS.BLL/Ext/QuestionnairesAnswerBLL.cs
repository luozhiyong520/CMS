using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMS.DAL;
using CMS.Model;
using Common;

namespace CMS.BLL
{
    public partial class QuestionnairesAnswerBLL
    {

         
        /// <summary>
        /// 提交问卷
        /// </summary>
        /// <param name="optionId"></param>
        /// <param name="userId"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public string SubmitInfo(int qId, string optionId, int customerId, string userName)
        {

            return dal.SubmitInfo(qId, optionId, customerId, userName);
       
        }
        /// <summary>
        /// 检查用户是否存在
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int CheckUser(int customerId)
        {
            return dal.CheckUser(customerId);
        }

        public int GetSurveyed(int QId, string Start, string End)
        {
            return dal.GetSurveyed(QId,Start, End);
        }

        public List<QuestionnairesAnswer> GetyAllProposal(int QId, string Start, string End)
        {
            return dal.GetAllProposal(QId, Start, End);
        }

        public int GetAnswerCount(int QoId,string strat,string end)
        {
            return dal.GetAnswerCount(QoId, strat, end);
        }
        //public List<Model.CMS.Questionnaires_Answer> GetAll(SqlWhereList sqlwhere)
        //{
        //    throw new NotImplementedException();
        //}

    }
}
