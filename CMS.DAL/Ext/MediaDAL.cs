using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using CMS.Model;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;


namespace CMS.DAL
{
    public partial class MediaDAL
    {
         /// <summary>
         /// 文件搜索操作
         /// </summary>
         /// <param name="page"></param>
         /// <param name="rows"></param>
         /// <param name="txtKeyword"></param>
         /// <param name="txtStartDate"></param>
         /// <param name="txtEndDate"></param>
         /// <returns></returns>
        public PagedResult GetMediaList(int? page, int rows, string txtKeyword, string txtStartDate, string txtEndDate, int MediaClass)
        {

            if (!string.IsNullOrEmpty(txtEndDate))
            {
                txtEndDate = DateTime.Parse(txtEndDate).AddDays(1).ToShortDateString();
            }

            if (!string.IsNullOrEmpty(txtKeyword))
            {
                txtKeyword = "%" + txtKeyword + "%";
            }
   
            PagedResult pagest = new PagedResult();

            SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(SqlConnectFactory.CMS, "up_Media_GetPageList");
            commandParameters[0].Value = rows; //pageSize 每页显示记录数
            commandParameters[1].Value = page;//pageindex 当前页码
            commandParameters[2].Value = MediaClass;
            commandParameters[3].Value = txtKeyword; 
            commandParameters[4].Value = txtStartDate;
            commandParameters[5].Value = txtEndDate;


            DataSet st = SqlHelper.ExecuteDataset(SqlConnectFactory.CMS, CommandType.StoredProcedure, "up_Media_GetPageList", commandParameters);
            pagest.Result = st.Tables[0];
            pagest.Total = int.Parse(commandParameters[6].Value.ToString());
            return pagest;

        }


        /// <summary>
        /// 获取图片路径
        /// </summary>
        /// <param name="strMediaId"></param>
        /// <returns></returns>
        public List<Media> GetUploadList(string MediaBacthId, int? MediaId, string operateType)
        {
            SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(SqlConnectFactory.CMS, "up_Media_Operate");
            commandParameters[0].Value = MediaBacthId;
            commandParameters[1].Value = MediaId;
            commandParameters[2].Value = operateType;
            List<Media> list;
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlConnectFactory.CMS,CommandType.StoredProcedure,"up_Media_Operate",commandParameters))
            {
                list = EntityHelper.GetEntityListByDataReader<Media>(dr, null);
            }
            return list;
        }

        /// <summary>
        /// 文件细节操作
        /// </summary>
        /// <param name="MediaBacthId"></param>
        /// <param name="MediaId"></param>
        /// <param name="operateType"></param>
        public void MediaOperate(string MediaBacthId, int? MediaId,string operateType)
        {
            SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(SqlConnectFactory.CMS, "up_Media_Operate");
            commandParameters[0].Value = MediaBacthId;
            commandParameters[1].Value = MediaId;
            commandParameters[2].Value = operateType;
            SqlHelper.ExecuteNonQuery(SqlConnectFactory.CMS, CommandType.StoredProcedure, "up_Media_Operate", commandParameters);
        }
 

    }
}
