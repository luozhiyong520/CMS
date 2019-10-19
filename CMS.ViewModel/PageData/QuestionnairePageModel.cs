using System;
using System.Collections.Generic;
using CMS.Model;

namespace CMS.ViewModel
{
   public class QuestionnairePageModel
    {
        public int QId { get; set; }

        public string Tit { get; set; }

        public string Desc { get; set; }

        public List<Questionnaires> Questions { get; set; }
    }
}
