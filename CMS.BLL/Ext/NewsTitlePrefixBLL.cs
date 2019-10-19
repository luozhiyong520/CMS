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
   public partial class NewsTitlePrefixBLL
    {
       public PagedResult<NewsTitlePrefix> GetNewsTitlePrefixList(int? page, int rows)
       {
           PagedResult<NewsTitlePrefix> st = dal.GetNewsTitlePrefixList(page, rows);
           return st;
       }
    }
}
