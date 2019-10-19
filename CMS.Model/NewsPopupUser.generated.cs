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
	[Table(Name=@"dbo.NewsPopupUser")]
	public partial class NewsPopupUser 	{

		#region Construction
		public NewsPopupUser()
		{
		}
		#endregion

		#region Column Mappings
		private int _Id;
		/// <summary>
		/// 
        /// </summary>
		[Column(DbType=@"Int NOT NULL IDENTITY", CanBeNull=false)]
		public int Id
		{
			get { return _Id; }
			set {
				if (_Id != value) {
					_Id = value;
				}
			}
		}
		
		private int? _ReceiverId;
		/// <summary>
		/// 用户Id
        /// </summary>
		[Column(DbType=@"Int")]
		public int? ReceiverId
		{
			get { return _ReceiverId; }
			set {
				if (_ReceiverId != value) {
					_ReceiverId = value;
				}
			}
		}
		
		private string _Receiver;
		/// <summary>
		/// 用户名
        /// </summary>
		[Column(DbType=@"NVarChar(20) NOT NULL", IsPrimaryKey=true)]
		public string Receiver
		{
			get { return _Receiver; }
			set {
				if (_Receiver != value) {
					_Receiver = value;
				}
			}
		}
		
		private string _NewsIds;
		/// <summary>
		/// 资讯Id, 逗号分隔
        /// </summary>
		[Column(DbType=@"Text")]
		public string NewsIds
		{
			get { return _NewsIds; }
			set {
				if (_NewsIds != value) {
					_NewsIds = value;
				}
			}
		}
		
		#endregion
	}
}
#pragma warning restore 1591