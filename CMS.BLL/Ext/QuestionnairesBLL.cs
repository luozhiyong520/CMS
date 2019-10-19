using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMS.Model;
using Common;
using CMS.DAL;
using Factory;

namespace CMS.BLL
{
    
    public partial class QuestionnairesBLL
    {
        /// <summary>
        /// 获取问题列表
        /// </summary>
        /// <returns></returns>
        [Cache("QuestionnairesBLL",30)]
        public virtual List<Questionnaires> GetQuestionnairesList(int parentId)
        {
            SqlWhereList sqlwhere = new SqlWhereList();
            sqlwhere.Add("ParentId", parentId);
            List<Questionnaires> list = dal.GetAll(sqlwhere);
            return list;
        }

        public virtual List<Questionnaires> GetQuestionnairesListNoCache(int parentId)
        {
            SqlWhereList sqlwhere = new SqlWhereList();
            sqlwhere.Add("ParentId", parentId);
            List<Questionnaires> list = dal.GetAll(sqlwhere);
            return list;
        }

        public int UpdateQuestionsOrder(int pId,string qo)
        {
            return dal.UpdateQuestionsOrder(pId,qo);
        }
    }
}
