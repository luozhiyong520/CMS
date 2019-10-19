using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Data;
using System.Data.SqlClient;
using Common;

namespace CMS.DAL
{
    /// <summary>
    /// 数据类基类
    /// </summary>
    /// <typeparam name="TClass"></typeparam>
    public abstract class BaseDAL<TClass> where TClass : class, new()
    {
        /// <summary>
        /// 表名
        /// </summary>
        private string tableName;
        /// <summary>
        /// 列名列表
        /// </summary>
        private List<ColumnDbType> colList = new List<ColumnDbType>();
        /// <summary>
        /// 属性列表
        /// </summary>
        private List<PropertyInfo> propList = new List<PropertyInfo>();
        /// <summary>
        /// 是否逻辑删除
        /// </summary>
        private bool isLogicDelete;

        private string typeNameSpace = "CMS.Model";

        /// <summary>
        ///  数据库链接配置字符串
        /// </summary>
        protected string ConnString;

        /// <summary>
        /// 构造函数
        /// </summary>
        public BaseDAL()
        {
            Type t = typeof(TClass);

            this.tableName = "[" + t.Name + "]";

            object[] tableAtts = t.GetCustomAttributes(typeof(TableAttribute), true);

            // 获取表名称
            if (tableAtts.Length > 0)
            {
                this.tableName = "[" + ((TableAttribute)tableAtts[0]).Name.Replace("dbo.", "") + "]";
            }
            PropertyInfo[] propertyInfos = ModelPropertyInfoCache.Instance.GetProperties<TClass>();
            // 获取实体的属性
            foreach (PropertyInfo pi in propertyInfos)
            {
                object[] colAtts = pi.GetCustomAttributes(typeof(ColumnAttribute), true);
                if (colAtts.Length == 0)
                {
                    continue;
                }
                ColumnAttribute colAtt = colAtts[0] as ColumnAttribute;
                ColumnDbType column = new ColumnDbType();
                column.GetDbType(colAtt.DbType);
                column.IsPrimaryKey = colAtt.IsPrimaryKey;
                column.ColumnName = pi.Name;

                colList.Add(column);
                propList.Add(pi);
            }

            isLogicDelete = t.GetProperty("DelStatus") != null;
        }

        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="entity"></param>
        public virtual int Add(TClass entity)
        {
            StringBuilder columnList = new StringBuilder();
            StringBuilder valuesList = new StringBuilder();
            List<SqlParameter> paras = new List<SqlParameter>();
            string identityColumnName = "";

            for (int i = 0; i < colList.Count; i++)
            {
                if (!colList[i].IsIdentity)
                {
                    columnList.Append(colList[i].ColumnName + ",");
                    valuesList.Append("@" + colList[i].ColumnName + ",");

                    SqlParameter para = new SqlParameter();
                    para.ParameterName = "@" + colList[i].ColumnName;
                    para.SqlDbType = colList[i].SqlDbType;
                    if (colList[i].Size > 0)
                    {
                        para.Size = colList[i].Size;
                    }
                    para.Value = GetValue(propList[i].GetValue(entity, null));
                    paras.Add(para);
                }
                else
                {
                    identityColumnName = colList[i].ColumnName;
                }
            }
            columnList.Remove(columnList.Length - 1, 1);
            valuesList.Remove(valuesList.Length - 1, 1);

            StringBuilder strSql = new StringBuilder(string.Format("Insert Into {0} ({1}) Values ({2})", tableName, columnList, valuesList));
            // 判断是否为自增长的主键
            if (identityColumnName != "")
            {
                strSql.Append(";Select @@IDENTITY");
            }

            int result = 0;

            using (DataAccess da = new DataAccess(ConnString))
            {
                if (identityColumnName != "")
                {
                    object obj = da.ExecuteScalar(strSql.ToString(), paras.ToArray());
                    if (obj != null)
                    {
                        result = Convert.ToInt32(obj);
                    }
                }
                else
                {
                    result = da.ExecuteNonQuery(strSql.ToString(), paras.ToArray());
                }
            }

            return result;
        }

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual int Update(TClass entity)
        {
            StringBuilder columnList = new StringBuilder();
            StringBuilder whereList = new StringBuilder();
            List<SqlParameter> paras = new List<SqlParameter>();

            for (int i = 0; i < colList.Count; i++)
            {
                if (!colList[i].IsPrimaryKey)
                {
                    columnList.Append(colList[i].ColumnName + "=@" + colList[i].ColumnName + ",");
                }
                else
                {
                    whereList.Append(colList[i].ColumnName + "=@" + colList[i].ColumnName + " And");
                }
                SqlParameter para = new SqlParameter();
                para.ParameterName = "@" + colList[i].ColumnName;
                para.SqlDbType = colList[i].SqlDbType;
                if (colList[i].Size > 0)
                {
                    para.Size = colList[i].Size;
                }
                para.Value = GetValue(propList[i].GetValue(entity, null));
                paras.Add(para);
            }
            columnList.Remove(columnList.Length - 1, 1);
            whereList.Remove(whereList.Length - 4, 4);

            string strSql = string.Format("Update {0} Set {1} Where {2}", tableName, columnList, whereList);

            int result = 0;
            using (DataAccess da = new DataAccess(ConnString))
            {
                result = da.ExecuteNonQuery(strSql, paras.ToArray());
            }
            return result;
        }

        public virtual int Delete(TClass entity)
        {
            StringBuilder whereList = new StringBuilder();
            List<SqlParameter> paras = new List<SqlParameter>();

            for (int i = 0; i < colList.Count; i++)
            {
                if (colList[i].IsPrimaryKey)
                {
                    whereList.Append(colList[i].ColumnName + "=@" + colList[i].ColumnName + " And");
                    SqlParameter para = new SqlParameter();
                    para.ParameterName = "@" + colList[i].ColumnName;
                    para.SqlDbType = colList[i].SqlDbType;
                    if (colList[i].Size > 0)
                    {
                        para.Size = colList[i].Size;
                    }
                    para.Value = propList[i].GetValue(entity, null);
                    paras.Add(para);
                }
            }
            whereList.Remove(whereList.Length - 4, 4);

            string strSql = "";
            if (isLogicDelete)
            {
                strSql = string.Format("Update {0} Set DelStatus=1 Where {1}", tableName, whereList);
            }
            else
            {
                strSql = string.Format("Delete From {0} Where {1}", tableName, whereList);
            }

            int result = 0;
            using (DataAccess da = new DataAccess(ConnString))
            {
                result = da.ExecuteNonQuery(strSql, paras.ToArray());
            }

            return result;
        }

        /// <summary>
        /// 单笔删除（只有一个主键的）
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public int Delete(object key)
        {
            if (key is ICollection)
            {
                return Delete(key as ICollection);
            }

            ColumnDbType cdt = colList.Where(c => c.IsPrimaryKey).Single();
            List<SqlParameter> paras = new List<SqlParameter>();
            StringBuilder inParas = new StringBuilder();
            SqlParameter para = new SqlParameter();
            para.ParameterName = "@" + cdt.ColumnName;
            para.SqlDbType = cdt.SqlDbType;
            para.Value = key;
            paras.Add(para);
            string strSql = "";
            if (isLogicDelete)
            {
                strSql = string.Format("Update {0} Set DelStatus=1 Where {1}=@{1}", tableName, cdt.ColumnName);
            }
            else
            {
                strSql = string.Format("Delete From {0} Where {1}=@{1}", tableName, cdt.ColumnName);
            }
            return NonQuery(strSql, paras.ToArray());
        }

        /// <summary>
        /// 批量删除（只有一个主键的）
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public int Delete(ICollection keys)
        {
            ColumnDbType cdt = colList.Where(c => c.IsPrimaryKey).Single();
            List<SqlParameter> paras = new List<SqlParameter>();
            StringBuilder inParas = new StringBuilder();
            int i = 0;
            foreach (object obj in keys)
            {
                SqlParameter para = new SqlParameter();
                para.ParameterName = "@P" + i;
                para.SqlDbType = cdt.SqlDbType;
                para.Value = obj;
                inParas.Append(para.ParameterName + ",");
                paras.Add(para);
                i++;
            }
            inParas.Remove(inParas.Length - 1, 1);
            string strSql = "";
            if (isLogicDelete)
            {
                strSql = string.Format("Update {0} Set DelStatus=1 Where {1} in ({2})", tableName, cdt.ColumnName, inParas);
            }
            else
            {
                strSql = string.Format("Delete From {0} Where {1} in ({2})", tableName, cdt.ColumnName, inParas);
            }
            return NonQuery(strSql, paras.ToArray());
        }
        
        ///// <summary>
        ///// 删除权限功能点的选项
        ///// </summary>
        ///// <param name="keys"></param>
        ///// <returns></returns>
        //public int Delete(int parentId)
        //{
        //    string sql = "Delete From " + tableName + " Where ParentId = " + parentId;
        //    using (var da = new DataAccess(ConnString))
        //    {
        //        return da.ExecuteNonQuery(sql);
        //    }
        //}

        /// <summary>
        /// 获取前500条数据
        /// </summary>
        /// <returns></returns>
        public virtual List<TClass> GetAll()
        {
            string strSql = string.Format("Select Top 500 * From {0} {1}", tableName, isLogicDelete ? "Where DelStatus=0" : "");
            DataSet ds = null;
            using (DataAccess da = new DataAccess(ConnString))
            {
                ds = da.ExecuteDataSet(strSql);
            }

            List<TClass> list = null;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                list = EntityHelper.GetEntityListByDT<TClass>(ds.Tables[0], null);
            }
            return list;
        }

        /// <summary>
        /// 获取表记录的总条数
        /// </summary>
        /// <returns></returns>
        public int GetCount()
        {
            string strSql = string.Format("select count(*) from " + tableName);
            DataSet ds = null;
            using (DataAccess da = new DataAccess(ConnString))
            {
                ds = da.ExecuteDataSet(strSql);
            }

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                return Convert.ToInt32(ds.Tables[0].Rows[0][0]);
            return 0;
        }

        /// <summary>
        /// 例: select (ago) from table (after)
        /// </summary>
        /// <param name="ago"></param>
        /// <param name="after"></param>
        /// <returns></returns>
        public virtual List<TClass> GetAll(string ago, string after)
        {
            string strSql = "Select " + ago + " From " + tableName + " " + after;
            DataSet ds = null;
            using (DataAccess da = new DataAccess(ConnString))
            {
                ds = da.ExecuteDataSet(strSql);
            }

            List<TClass> list = null;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                list = EntityHelper.GetEntityListByDT<TClass>(ds.Tables[0], null);
            }
            return list;
        }

        public virtual List<TClass> GetAll(SqlWhereList sqlWhereList)
        {
            string strSql = string.Format("Select Top 500 * From {0} {1}", tableName,
                                          isLogicDelete ? "Where DelStatus=0" : "");
            strSql += isLogicDelete ? "" : " WHERE 1=1 ";
            strSql = sqlWhereList.Aggregate(strSql,
                                            (current, field) =>
                                            current + (" AND " + field.ParaName + " = @" + field.ParaName));
            return ExecuteDataSet(strSql, sqlWhereList.ToParas()).ToList<TClass>();
        }

        /// <summary>
        /// 查询单键实体
        /// </summary>
        /// <param name="keyPropName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual TClass Get(string keyPropName, object value)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add(keyPropName, value);
            return Get(dic);
        }

        /// <summary>
        /// 查询多键实体
        /// </summary>
        /// <param name="keyProps"></param>
        /// <returns></returns>
        public virtual TClass Get(Dictionary<string, object> keyProps)
        {
            List<ColumnDbType> cList = new List<ColumnDbType>();
            foreach (string key in keyProps.Keys)
            {
                ColumnDbType cdt = null;
                for (int i = 0; i < colList.Count; i++)
                {
                    if (colList[i].ColumnName.ToLower() == key.ToLower())
                    {
                        cdt = colList[i];
                        break;
                    }
                }
                if (cdt == null)
                {
                    throw new Exception(string.Format("表{0}未找到属性{1}", tableName, key));
                }
                cdt.Value = keyProps[key];
                cList.Add(cdt);
            }

            StringBuilder whereList = new StringBuilder();
            List<SqlParameter> paras = new List<SqlParameter>();

            for (int i = 0; i < cList.Count; i++)
            {
                whereList.Append(" " + cList[i].ColumnName + "=@" + cList[i].ColumnName + " And");
                SqlParameter para = new SqlParameter();
                para.ParameterName = "@" + cList[i].ColumnName;
                para.SqlDbType = cList[i].SqlDbType;
                if (cList[i].Size > 0)
                {
                    para.Size = cList[i].Size;
                }
                para.Value = cList[i].Value;
                paras.Add(para);
            }
            whereList.Remove(whereList.Length - 4, 4);

            string strSql = string.Format("Select * From {0} Where {1} {2}", tableName, isLogicDelete ? "DelStatus=0 And " : "", whereList);

            DataSet ds = null;
            using (DataAccess da = new DataAccess(ConnString))
            {
                ds = da.ExecuteDataSet(strSql, paras.ToArray());
            }

            List<TClass> list = new List<TClass>();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                list = EntityHelper.GetEntityListByDT<TClass>(ds.Tables[0], null);
            }
            if (list.Count > 1)
            {
                throw new Exception(string.Format("表{0}返回不只一条记录", tableName));
            }
            return list.Count == 1 ? list[0] : null;
        }

        /// <summary>
        /// 根据多键实体判断是否存在记录
        /// </summary>
        /// <param name="keyProps"></param>
        /// <returns></returns>
        public virtual bool IsHas(Dictionary<string, object> keyProps)
        {
            List<ColumnDbType> cList = new List<ColumnDbType>();
            foreach (string key in keyProps.Keys)
            {
                ColumnDbType cdt = null;
                for (int i = 0; i < colList.Count; i++)
                {
                    if (colList[i].ColumnName.ToLower() == key.ToLower())
                    {
                        cdt = colList[i];
                        break;
                    }
                }
                if (cdt == null)
                {
                    throw new Exception(string.Format("表{0}未找到属性{1}", tableName, key));
                }
                cdt.Value = keyProps[key];
                cList.Add(cdt);
            }

            StringBuilder whereList = new StringBuilder();
            List<SqlParameter> paras = new List<SqlParameter>();

            for (int i = 0; i < cList.Count; i++)
            {
                whereList.Append(" " + cList[i].ColumnName + "=@" + cList[i].ColumnName + " And");
                SqlParameter para = new SqlParameter();
                para.ParameterName = "@" + cList[i].ColumnName;
                para.SqlDbType = cList[i].SqlDbType;
                if (cList[i].Size > 0)
                {
                    para.Size = cList[i].Size;
                }
                para.Value = cList[i].Value;
                paras.Add(para);
            }
            whereList.Remove(whereList.Length - 4, 4);

            string strSql = string.Format("Select 1 From {0} Where {1} {2}", tableName, isLogicDelete ? "DelStatus=0 And " : "", whereList);


            using (DataAccess da = new DataAccess(ConnString))
            {
                SqlDataReader sdr = da.ExecuteReader(strSql, paras.ToArray());
                return sdr.HasRows;
            }
        }

        /// <summary>
        /// 分页读取数据
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        public virtual PagedResult GetPaged(int pageIndex, int pageSize)
        {
            StringBuilder orderby = new StringBuilder();

            for (int i = 0; i < colList.Count; i++)
            {
                if (colList[i].IsPrimaryKey)
                {
                    orderby.Append(colList[i].ColumnName + ",");
                }
            }
            orderby.Remove(orderby.Length - 1, 1);

            return this.GetPaged(pageIndex, pageSize, new Dictionary<string, object>(), orderby.ToString());
        }


        /// <summary>
        /// 分页读取数据
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="whereCollection">条件值，例如：key为UserId>=@UserId，值为20</param>
        /// <param name="orderBy">排序字段</param>
        /// <returns></returns>
        public virtual PagedResult GetPaged(int pageIndex, int pageSize, Dictionary<string, object> whereCollection, string orderBy)
        {
            List<ColumnDbType> cList = new List<ColumnDbType>();
            StringBuilder whereList = new StringBuilder();
            List<SqlParameter> paras = new List<SqlParameter>();
            if (whereCollection != null)
            {
                foreach (string key in whereCollection.Keys)
                {
                    ColumnDbType cdt = null;
                    for (int i = 0; i < colList.Count; i++)
                    {
                        string colName = this.GetFieldName(key.Trim());
                        if (colList[i].ColumnName.ToLower() == colName.ToLower())
                        {
                            cdt = colList[i];
                            break;
                        }
                    }
                    if (cdt == null)
                    {
                        throw new Exception(string.Format("表{0}未找到属性{1}", tableName, key));
                    }
                    whereList.Append(" " + key + " = @" + key + " And");
                    SqlParameter para = new SqlParameter();
                    para.ParameterName = "@" + key;
                    para.SqlDbType = cdt.SqlDbType;
                    if (cdt.Size > 0)
                    {
                        para.Size = cdt.Size;
                    }
                    para.Value = whereCollection[key];
                    paras.Add(para);
                }
            }
            if (whereList.Length > 0)
            {
                whereList.Remove(whereList.Length - 4, 4);
                whereList.Insert(0, "Where ");
            }

            if (whereList.Length > 0)
            {
                if (isLogicDelete)
                {
                    whereList.Append(" And DelStatus=0");
                }
            }
            else
            {
                if (isLogicDelete)
                {
                    whereList.Append("Where DelStatus=0");
                }
            }

            string strSql = "";
            if (pageIndex == 1)
            {
                strSql = string.Format("Select Count(1) From {1} {2};Select Top {0} * From {1} {2} Order By {3}", pageSize, tableName, whereList, orderBy);
            }
            else
            {
                int startRowNum = (pageIndex - 1) * pageSize;

                StringBuilder innerSql = new StringBuilder();
                innerSql.AppendFormat("Select ROW_NUMBER() OVER (Order By {0}) AS [ROW_NUMBER],* From {1} {2}", orderBy, tableName, whereList);

                strSql = string.Format("Select Count(1) From {1} {2};Select * From ({0}) as [t1] Where [t1].[ROW_NUMBER] BETWEEN @CRMPageIndex + 1 AND @CRMPageIndex + @CRMPageSize ORDER BY [t1].[ROW_NUMBER]", innerSql, tableName, whereList);

                SqlParameter para = new SqlParameter();
                para.ParameterName = "@CRMPageIndex";
                para.SqlDbType = SqlDbType.Int;
                para.Size = 4;
                para.Value = startRowNum;
                paras.Add(para);

                para = new SqlParameter();
                para.ParameterName = "@CRMPageSize";
                para.SqlDbType = SqlDbType.Int;
                para.Size = 4;
                para.Value = pageSize;
                paras.Add(para);
            }

            DataSet ds = null;
            using (DataAccess da = new DataAccess(this.ConnString))
            {
                ds = da.ExecuteDataSet(strSql, paras.ToArray());
            }
            if (ds != null && ds.Tables.Count == 2)
            {
                return new PagedResult(ds.Tables[1], Convert.ToInt32(ds.Tables[0].Rows[0][0]));
            }

            return new PagedResult();
        }

        /// <summary>
        /// 分页读取数据
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="whereCollection">条件值</param>
        /// <param name="orderBy">排序字段</param>
        /// <returns></returns>
        public virtual PagedResult GetPaged(int pageIndex, int pageSize, string querySql, SqlWhereList whereList, string orderBy)
        {
            List<SqlParameter> paras = new List<SqlParameter>();

            foreach (TableField tf in whereList)
            {
                SqlParameter para = new SqlParameter();
                para.ParameterName = "@" + tf.ParaName;
                para.Value = tf.Value;
                paras.Add(para);
            }
            string strSql = "";

            string countSql = string.Format("Select count(1) From ({0}) AS A", querySql);
            if (pageIndex == 1)
            {
                strSql = string.Format("{0};Select Top {1} t1.* From ({2}) AS t1 Order By {3}", countSql, pageSize, querySql, orderBy);
            }
            else
            {
                int startRowNum = (pageIndex - 1) * pageSize;

                StringBuilder innerSql = new StringBuilder();
                innerSql.AppendFormat("Select ROW_NUMBER() OVER (Order By {0}) AS [ROW_NUMBER],t2.* From ({1}) AS t2", orderBy, querySql);

                strSql = string.Format("{1};Select * From ({0}) as [t1] Where [t1].[ROW_NUMBER] BETWEEN @CRMPageIndex + 1 AND @CRMPageIndex + @CRMPageSize ORDER BY [t1].[ROW_NUMBER]", innerSql, countSql);

                SqlParameter para = new SqlParameter();
                para.ParameterName = "@CRMPageIndex";
                para.SqlDbType = SqlDbType.Int;
                para.Size = 4;
                para.Value = startRowNum;
                paras.Add(para);

                para = new SqlParameter();
                para.ParameterName = "@CRMPageSize";
                para.SqlDbType = SqlDbType.Int;
                para.Size = 4;
                para.Value = pageSize;
                paras.Add(para);
            }

            DataSet ds = null;
            using (DataAccess da = new DataAccess(ConnString))
            {
                ds = da.ExecuteDataSet(strSql, paras.ToArray());
            }

            if (ds != null && ds.Tables.Count == 2)
            {
                return new PagedResult(ds.Tables[1], Convert.ToInt32(ds.Tables[0].Rows[0][0]));
            }

            return new PagedResult();
        }


        /// <summary>
        /// 执行带参数的查询
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        protected int NonQuery(string sql, SqlParameter[] parameters)
        {
            using (var da = new DataAccess(ConnString))
            {
                return da.ExecuteNonQuery(sql, parameters);
            }
        }
        /// <summary>
        /// 执行带参数的存储过程
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        protected int StoredProcedureNonQuery(string spName, SqlParameter[] parameters)
        {
            using (var da = new DataAccess(ConnString))
            {
                return da.ExecuteNonQuery(CommandType.StoredProcedure, spName, parameters);
            }
        }

        /// <summary>
        /// 执行带参数的查询，返回对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        protected T Scalar<T>(string sql, SqlParameter[] parameters)
        {
            object obj = null;
            using (var da = new DataAccess(ConnString))
            {
                obj = da.ExecuteScalar(sql, parameters);
            }
            if (obj == null)
            {
                return default(T);
            }
            return (T)obj;
        }

        /// <summary>
        /// 执行带参数的查询，返回DataSet
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        protected DataSet ExecuteDataSet(string sql, SqlParameter[] parameters)
        {
            DataSet ds = null;
            using (var da = new DataAccess(ConnString))
            {
                ds = da.ExecuteDataSet(sql, parameters);
            }

            return ds;
        }
        /// <summary>
        /// 执行带参数的查询，返回DataSet(适用于存储过程)
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        protected DataSet StoredProcedureExecuteDataSet(string spName, SqlParameter[] parameters)
        {
            DataSet ds = null;
            using (var da = new DataAccess(ConnString))
            {
                ds = da.ExecuteDataSet(CommandType.StoredProcedure, spName, parameters);
            }

            return ds;
        }

        #region Methods

        private ColumnDbType GetColumnDbType(string paraName, string tableColumn)
        {
            Type t = typeof(TClass);
            string columnName = "";
            if (tableColumn == "")
            {
                t = typeof(TClass);
                columnName = paraName;
            }
            else
            {
                string[] tabs = tableColumn.Split('.');
                if (tabs.Length != 1 && tabs.Length != 2)
                {
                    return null;
                }
                if (tabs.Length == 2)
                {
                    t = t.Assembly.GetType(typeNameSpace + "." + tabs[0], false, true);
                    columnName = tabs[1];
                }
                else
                {
                    t = typeof(TClass);
                    columnName = tabs[0];
                }
            }
            PropertyInfo pi = t.GetProperty(columnName);
            if (pi == null)
            {
                return null;
            }
            object[] colAtts = pi.GetCustomAttributes(typeof(ColumnAttribute), true);

            if (colAtts.Length == 0)
            {
                return null;
            }
            ColumnAttribute colAtt = colAtts[0] as ColumnAttribute;
            ColumnDbType column = new ColumnDbType();
            column.GetDbType(colAtt.DbType);
            column.IsPrimaryKey = colAtt.IsPrimaryKey;
            column.ColumnName = pi.Name;

            return column;
        }

        /// <summary>
        /// 获取字段名
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        private string GetFieldName(string condition)
        {
            StringBuilder field = new StringBuilder();
            foreach (char c in condition)
            {
                if (c == ' ' || c == '>' || c == '!' || c == '<' || c == '=' || c == '(' || c == ')')
                {
                    break;
                }
                field.Append(c);
            }
            return field.ToString();
        }

        /// <summary>
        /// 获取参数名
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        private string GetParaName(string condition)
        {
            StringBuilder para = new StringBuilder();
            foreach (char c in condition)
            {
                if (para.Length == 0 && c != '@')
                {
                    continue;
                }
                if (c == ' ' || c == '>' || c == '!' || c == '<' || c == '=' || c == '(' || c == ')')
                {
                    break;
                }
                para.Append(c);
            }
            return para.ToString();
        }

        /// <summary>
        /// 获取对象DBNull值
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private object GetValue(object obj)
        {
            if (obj == null)
            {
                return DBNull.Value;
            }
            return obj;
        }
        #endregion
    }
}

