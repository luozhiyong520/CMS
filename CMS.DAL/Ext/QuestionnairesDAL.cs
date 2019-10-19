using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMS.DAL
{
    public partial class QuestionnairesDAL
    {
        public int UpdateQuestionsOrder(int pId,string qo)
        {
            qo = qo.Replace("|", "  then ");
            string[] items = qo.Split(',');
            string sqlCase = "";
            foreach (var item in items)
            {
                sqlCase += " when QId = " + item + " ";
            }
            string sql = "update Questionnaires set Orders = " +
                            "case " +
                                sqlCase +
                            "end where ParentId= "+pId;
            using (var da = new DataAccess(ConnString))
            {
                return da.ExecuteNonQuery(sql);
            }
        }
    }
}
