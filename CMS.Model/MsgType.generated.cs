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
	[Table(Name=@"dbo.MsgType")]
	public partial class MsgType 	{

		#region Construction
		public MsgType()
		{
		}
		#endregion

		#region Column Mappings
		private int _MsgTypeId;
		/// <summary>
		/// 类型Id
        /// </summary>
		[Column(DbType=@"Int NOT NULL", IsPrimaryKey=true)]
		public int MsgTypeId
		{
			get { return _MsgTypeId; }
			set {
				if (_MsgTypeId != value) {
					_MsgTypeId = value;
				}
			}
		}
		
		private string _TypeContent;
		/// <summary>
		/// 类型名称
        /// </summary>
		[Column(DbType=@"NVarChar(20)")]
		public string TypeContent
		{
			get { return _TypeContent; }
			set {
				if (_TypeContent != value) {
					_TypeContent = value;
				}
			}
		}
		
		#endregion
	}
}
#pragma warning restore 1591