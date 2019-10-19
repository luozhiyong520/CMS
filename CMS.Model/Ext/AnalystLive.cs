using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using SMVC;
namespace CMS.Model
{
    public partial class AnalystLive
    {
        public decimal SuccessRate { set; get; } //成功率 战绩
        public int FansNum { set; get; }    //粉丝数
        public string Intro { set; get; }   //分析师简介
        public string AnalystImg { set; get; }  //分析师头像
        /// <summary>
        /// 页面显示的日期
        /// </summary>
        public string ShowTime { set; get; }  
        public string Content { set; get; }

        public int Id{ set; get; }
        public int CustomerId{ set; get; }  //用户ID
        public string CustomerName { set; get; }  //用户名
        public string Notice { set; get; }   //公告

        public int BigNum { set; get; }   //开多次数
        public int EmptyNum { set; get; } //开空次数
        public int ProfitNum { set; get; } //盈利次数
        public int LossNum { set; get; }  //亏损次数
        public string NickName { set; get; }  //别名
        public decimal VipProfitRate { get; set; }//VIP直播盈亏比例
    }
}
