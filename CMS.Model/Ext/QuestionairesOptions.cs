using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMS.Model
{
    public partial class QuestionnairesOptions
    {
        /// <summary>
        /// 选项的投票人数
        /// </summary>
        public int OptionsNum { get; set; }

        /// <summary>
        /// 选项投票人数，占该选项投票总人数的比例
        /// </summary>
        public double OptionsRate { get; set; }
    }
}
