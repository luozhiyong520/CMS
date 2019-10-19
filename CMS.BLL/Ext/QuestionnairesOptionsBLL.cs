using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMS.DAL;
using CMS.Model;
using Common;
using Factory;

namespace CMS.BLL
{
    public partial class QuestionnairesOptionsBLL
    {
         /// <summary>
         /// 获取问题选项列表
         /// </summary>
         /// <param name="questionnairesId"></param>
         /// <returns></returns>
         [Cache("QuestionnairesOptionsBLL",30)]
         public virtual List<QuestionnairesOptions> GetQuestionnairesOptionsList(int QId)
         {
             SqlWhereList sqlwhere = new SqlWhereList();
             sqlwhere.Add("QId", QId);
             List<QuestionnairesOptions> list = dal.GetAll(sqlwhere);
             return list;
         }

    }
}
