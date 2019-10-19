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
    public partial class TopicBLL
    {
        public PagedResult<Topic> GetTopicList(string topicName, int topicTypeId, int? page, int rows)
        {
            PagedResult<Topic> st = dal.GetTopicList(topicName, topicTypeId, page, rows);
            return st;
        }
        
        /// <summary>
        /// 前台专题查询
        /// </summary>
        /// <param name="topicName"></param>
        /// <param name="topicTypeId"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public PagedResult<Topic> GetTopicFrontList(string topicName, string topicTypeId, int? page, int rows)
        {
            PagedResult<Topic> st = dal.GetTopicFrontList(topicName, topicTypeId, page, rows);
            return st;
        }


        public Topic GetTopic(int topicId)
        {
            return dal.GetTopic(topicId);
        }
    }
}
