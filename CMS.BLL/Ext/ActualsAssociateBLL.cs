using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMS.DAL;
using System.Data;
using Common;
using CMS.Model;
using System.Text.RegularExpressions;

namespace CMS.BLL
{
    public partial class ActualsAssociateBLL
    {
        public PagedResult<ActualsAssociate> GetActualsAssociateList(int typeId, string actualsName, string stockName, int? page, int rows)
        {
            PagedResult<ActualsAssociate> st = dal.GetActualsAssociateList(typeId,actualsName,stockName,page,rows);
            return st;
        }

        public string InsertActualsAssociate(ActualsAssociate actualsAssociate)
        {
            return dal.InsertActualsAssociate(actualsAssociate);
        }

        public string UpdateActualsAssociate(ActualsAssociate actualsAssociate)
        {
            return dal.UpdateActualsAssociate(actualsAssociate);
        }
    }
}
