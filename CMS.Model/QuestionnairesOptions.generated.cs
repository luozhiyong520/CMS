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
	[Table(Name=@"dbo.QuestionnairesOptions")]
	public partial class QuestionnairesOptions 	{

		#region Construction
		public QuestionnairesOptions()
		{
		}
		#endregion

		#region Column Mappings
		private int _QoId;
		/// <summary>
		/// 
        /// </summary>
		[Column(DbType=@"Int NOT NULL IDENTITY", IsPrimaryKey=true)]
		public int QoId
		{
			get { return _QoId; }
			set {
				if (_QoId != value) {
					_QoId = value;
				}
			}
		}
		
		private int? _QId;
		/// <summary>
		/// 
        /// </summary>
		[Column(DbType=@"Int")]
		public int? QId
		{
			get { return _QId; }
			set {
				if (_QId != value) {
					_QId = value;
				}
			}
		}
		
		private string _Info;
		/// <summary>
		/// 
        /// </summary>
		[Column(DbType=@"NVarChar(200)")]
		public string Info
		{
			get { return _Info; }
			set {
				if (_Info != value) {
					_Info = value;
				}
			}
		}
		
		#endregion
	}
}
#pragma warning restore 1591