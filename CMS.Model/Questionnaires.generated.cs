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
	[Table(Name=@"dbo.Questionnaires")]
	public partial class Questionnaires 	{

		#region Construction
		public Questionnaires()
		{
		}
		#endregion

		#region Column Mappings
		private int _QId;
		/// <summary>
		/// 
        /// </summary>
		[Column(DbType=@"Int NOT NULL IDENTITY", IsPrimaryKey=true)]
		public int QId
		{
			get { return _QId; }
			set {
				if (_QId != value) {
					_QId = value;
				}
			}
		}
		
		private int? _ParentId;
		/// <summary>
		/// 所属问卷, 0代表问卷
        /// </summary>
		[Column(DbType=@"Int")]
		public int? ParentId
		{
			get { return _ParentId; }
			set {
				if (_ParentId != value) {
					_ParentId = value;
				}
			}
		}
		
		private string _Title;
		/// <summary>
		/// 标题
        /// </summary>
		[Column(DbType=@"NVarChar(100)")]
		public string Title
		{
			get { return _Title; }
			set {
				if (_Title != value) {
					_Title = value;
				}
			}
		}
		
		private string _Description;
		/// <summary>
		/// 描述
        /// </summary>
		[Column(DbType=@"NVarChar(500)")]
		public string Description
		{
			get { return _Description; }
			set {
				if (_Description != value) {
					_Description = value;
				}
			}
		}
		
		private string _OptType;
		/// <summary>
		/// 类型, 单选;多选;文字;
        /// </summary>
		[Column(DbType=@"NVarChar(20)")]
		public string OptType
		{
			get { return _OptType; }
			set {
				if (_OptType != value) {
					_OptType = value;
				}
			}
		}
		
		private int? _Orders;
		/// <summary>
		/// 
        /// </summary>
		[Column(DbType=@"Int")]
		public int? Orders
		{
			get { return _Orders; }
			set {
				if (_Orders != value) {
					_Orders = value;
				}
			}
		}
		
		#endregion
	}
}
#pragma warning restore 1591