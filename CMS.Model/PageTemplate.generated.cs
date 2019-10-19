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
	[Table(Name=@"dbo.PageTemplate")]
	public partial class PageTemplate 	{

		#region Construction
		public PageTemplate()
		{
		}
		#endregion

		#region Column Mappings
		private int _TemplateId;
		/// <summary>
		/// 编号
        /// </summary>
		[Column(DbType=@"Int NOT NULL IDENTITY", IsPrimaryKey=true)]
		public int TemplateId
		{
			get { return _TemplateId; }
			set {
				if (_TemplateId != value) {
					_TemplateId = value;
				}
			}
		}
		
		private string _TemplateName;
		/// <summary>
		/// 模板名称
        /// </summary>
		[Column(DbType=@"NVarChar(50)")]
		public string TemplateName
		{
			get { return _TemplateName; }
			set {
				if (_TemplateName != value) {
					_TemplateName = value;
				}
			}
		}
		
		private string _Remark;
		/// <summary>
		/// 简介
        /// </summary>
		[Column(DbType=@"NVarChar(200)")]
		public string Remark
		{
			get { return _Remark; }
			set {
				if (_Remark != value) {
					_Remark = value;
				}
			}
		}
		
		private string _TemplateFileName;
		/// <summary>
		/// 文件名
        /// </summary>
		[Column(DbType=@"NVarChar(50)")]
		public string TemplateFileName
		{
			get { return _TemplateFileName; }
			set {
				if (_TemplateFileName != value) {
					_TemplateFileName = value;
				}
			}
		}
		
		private string _HtmlPath;
		/// <summary>
		/// 目录路径
        /// </summary>
		[Column(DbType=@"NVarChar(100)")]
		public string HtmlPath
		{
			get { return _HtmlPath; }
			set {
				if (_HtmlPath != value) {
					_HtmlPath = value;
				}
			}
		}
		
		private int? _OrderNum;
		/// <summary>
		/// 排序值
        /// </summary>
		[Column(DbType=@"Int")]
		public int? OrderNum
		{
			get { return _OrderNum; }
			set {
				if (_OrderNum != value) {
					_OrderNum = value;
				}
			}
		}
		
		private bool? _Status;
		/// <summary>
		/// 
        /// </summary>
		[Column(DbType=@"Bit")]
		public bool? Status
		{
			get { return _Status; }
			set {
				if (_Status != value) {
					_Status = value;
				}
			}
		}
		
		private int? _TempleteType;
		/// <summary>
		/// 1)列表页2）终极页面3）其他
        /// </summary>
		[Column(DbType=@"Int")]
		public int? TempleteType
		{
			get { return _TempleteType; }
			set {
				if (_TempleteType != value) {
					_TempleteType = value;
				}
			}
		}
		
		private string _Encoding;
		/// <summary>
		/// 生成文件的编码
        /// </summary>
		[Column(DbType=@"VarChar(10)")]
		public string Encoding
		{
			get { return _Encoding; }
			set {
				if (_Encoding != value) {
					_Encoding = value;
				}
			}
		}
		
		private string _CreatedUser;
		/// <summary>
		/// 
        /// </summary>
		[Column(DbType=@"NVarChar(20)")]
		public string CreatedUser
		{
			get { return _CreatedUser; }
			set {
				if (_CreatedUser != value) {
					_CreatedUser = value;
				}
			}
		}
		
		private DateTime? _CreatedTime;
		/// <summary>
		/// 
        /// </summary>
		[Column(DbType=@"DateTime")]
		public DateTime? CreatedTime
		{
			get { return _CreatedTime; }
			set {
				if (_CreatedTime != value) {
					_CreatedTime = value;
				}
			}
		}
		
		private string _UpdatedUser;
		/// <summary>
		/// 
        /// </summary>
		[Column(DbType=@"NVarChar(20)")]
		public string UpdatedUser
		{
			get { return _UpdatedUser; }
			set {
				if (_UpdatedUser != value) {
					_UpdatedUser = value;
				}
			}
		}
		
		private DateTime? _UpdatedTime;
		/// <summary>
		/// 
        /// </summary>
		[Column(DbType=@"DateTime")]
		public DateTime? UpdatedTime
		{
			get { return _UpdatedTime; }
			set {
				if (_UpdatedTime != value) {
					_UpdatedTime = value;
				}
			}
		}
		
		private string _SiteName;
		/// <summary>
		/// 
        /// </summary>
		[Column(DbType=@"NVarChar(50)")]
		public string SiteName
		{
			get { return _SiteName; }
			set {
				if (_SiteName != value) {
					_SiteName = value;
				}
			}
		}
		
		#endregion
	}
}
#pragma warning restore 1591