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
using Common;
using System.Data.Linq;
using System.ComponentModel;

namespace CMS.Model
{	
	[Table(Name=@"dbo.ActualsAssociate")]
	public partial class ActualsAssociate 	{

		#region Construction
		public ActualsAssociate()
		{
		}
		#endregion

		#region Column Mappings
		private int _Id;
		/// <summary>
		/// 主键id
        /// </summary>
		[Column(DbType=@"Int NOT NULL IDENTITY", IsPrimaryKey=true)]
		public int Id
		{
			get { return _Id; }
			set {
				if (_Id != value) {
					_Id = value;
				}
			}
		}
		
		private string _ActualsCode;
		/// <summary>
		/// 渤海现货代码
        /// </summary>
		[Column(DbType=@"VarChar(10) NOT NULL", CanBeNull=false)]
		public string ActualsCode
		{
			get { return _ActualsCode; }
			set {
				if (_ActualsCode != value) {
					_ActualsCode = value;
				}
			}
		}
		
		private string _ActualsName;
		/// <summary>
		/// 渤海现货名称
        /// </summary>
		[Column(DbType=@"VarChar(20) NOT NULL", CanBeNull=false)]
		public string ActualsName
		{
			get { return _ActualsName; }
			set {
				if (_ActualsName != value) {
					_ActualsName = value;
				}
			}
		}
		
		private string _StockCode;
		/// <summary>
		/// 关联品种代码
        /// </summary>
		[Column(DbType=@"VarChar(10) NOT NULL", CanBeNull=false)]
		public string StockCode
		{
			get { return _StockCode; }
			set {
				if (_StockCode != value) {
					_StockCode = value;
				}
			}
		}
		
		private string _StockName;
		/// <summary>
		/// 关联品种名称
        /// </summary>
		[Column(DbType=@"VarChar(20) NOT NULL", CanBeNull=false)]
		public string StockName
		{
			get { return _StockName; }
			set {
				if (_StockName != value) {
					_StockName = value;
				}
			}
		}
		
		private int _TypeId;
		/// <summary>
		/// 关联品种类型1：A股2：国内期货3：外盘

        /// </summary>
		[Column(DbType=@"Int NOT NULL", CanBeNull=false)]
		public int TypeId
		{
			get { return _TypeId; }
			set {
				if (_TypeId != value) {
					_TypeId = value;
				}
			}
		}
		
		private string _Exchange;
		/// <summary>
		/// 交易所
        /// </summary>
		[Column(DbType=@"VarChar(50)")]
		public string Exchange
		{
			get { return _Exchange; }
			set {
				if (_Exchange != value) {
					_Exchange = value;
				}
			}
		}
		
		private string _CreatedUser;
		/// <summary>
		/// 创建人
        /// </summary>
		[Column(DbType=@"VarChar(20) NOT NULL", CanBeNull=false)]
		public string CreatedUser
		{
			get { return _CreatedUser; }
			set {
				if (_CreatedUser != value) {
					_CreatedUser = value;
				}
			}
		}
		
		private DateTime _CreatedTime;
		/// <summary>
		/// 创建时间
        /// </summary>
		[Column(DbType=@"DateTime NOT NULL", CanBeNull=false)]
		public DateTime CreatedTime
		{
			get { return _CreatedTime; }
			set {
				if (_CreatedTime != value) {
					_CreatedTime = value;
				}
			}
		}
		
		#endregion
	}
}
#pragma warning restore 1591