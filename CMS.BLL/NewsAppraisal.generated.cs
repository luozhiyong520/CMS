#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by LINQ to SQL template for T4 C#
//     Modified by ZEN 6/7/2010 removed LINQ to SQL feathers.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Linq;
using Common;
using CMS.Model;
using CMS.DAL;

namespace CMS.BLL
{	
	public partial class NewsAppraisalBLL 	{
		private static readonly NewsAppraisalDAL dal = Factory.DataFactory.CreateDAL<NewsAppraisalDAL>();
		
		#region 成员方法
		
        /// <summary>
        /// 添加单条数据
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int Add(NewsAppraisal obj)
        {
            return dal.Add(obj);
        }

        /// <summary>
        /// 更新单条数据
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int Update(NewsAppraisal obj)
        {
            return dal.Update(obj);
        }

        /// <summary>
        /// 删除单条数据
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int Delete(NewsAppraisal obj)
        {
            return dal.Delete(obj);
        }

        /// <summary>
        /// 批量删除（只有一个主键的）
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public int Delete(ICollection keys)
        {
            return dal.Delete(keys);
        }

		/// <summary>
        /// 条件删除
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
		public int Delete(SqlWhereList condition)
        {
            return dal.Delete(condition);
        }

		/// <summary>
        /// 获取一条实体数据
        /// </summary>
        /// <returns></returns>
        public NewsAppraisal Get(string colName,object value)
        {
            return dal.Get(colName,value);
        }

		/// <summary>
        /// 查询多键实体
        /// </summary>
        /// <returns></returns>
        public NewsAppraisal Get(Dictionary<string, object> keyProps)
        {
            return dal.Get(keyProps);
        }
        
        /// <summary>
        /// 获取全部数据
        /// </summary>
        /// <returns></returns>
        public List<NewsAppraisal> GetAll()
        {
            return dal.GetAll();
        }

        /// <summary>
        /// 获取条件数据
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public List<NewsAppraisal> GetAll(SqlWhereList where)
        {
            return dal.GetAll(where);
        }

        /// <summary>
        /// 分页读取数据
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        public PagedResult GetPaged(int pageIndex, int pageSize)
        {
            return dal.GetPaged(pageIndex, pageSize);
        }

        /// <summary>
        /// 分页读取数据
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="whereCollection">条件值，例如：key为UserId>=@UserId，值为20</param>
        /// <param name="orderBy">排序字段</param>
        /// <returns></returns>
        public PagedResult GetPaged(int pageIndex, int pageSize, Dictionary<string, object> whereCollection, string orderBy)
        {
            return dal.GetPaged(pageIndex, pageSize, whereCollection, orderBy);
        }
		
		#endregion

	}
}
#pragma warning restore 1591