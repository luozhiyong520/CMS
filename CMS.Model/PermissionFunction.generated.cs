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
	[Table(Name=@"dbo.PermissionFunction")]
	public partial class PermissionFunction 	{

		#region Construction
		public PermissionFunction()
		{
		}
		#endregion

		#region Column Mappings
		private int _FunctionId;
		/// <summary>
		/// 
        /// </summary>
		[Column(DbType=@"Int NOT NULL", CanBeNull=false)]
		public int FunctionId
		{
			get { return _FunctionId; }
			set {
				if (_FunctionId != value) {
					_FunctionId = value;
				}
			}
		}
		
		private int _PermissionId;
		/// <summary>
		/// 
        /// </summary>
		[Column(DbType=@"Int NOT NULL", CanBeNull=false)]
		public int PermissionId
		{
			get { return _PermissionId; }
			set {
				if (_PermissionId != value) {
					_PermissionId = value;
				}
			}
		}
		
		#endregion
	}
}
#pragma warning restore 1591