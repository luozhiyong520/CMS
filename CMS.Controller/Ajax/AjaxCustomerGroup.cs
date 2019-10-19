using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMS.BLL;
using SMVC;
using Common;
using System.Web;
using CMS.Model;
using CMS.ViewModel;
using CMS.Utility;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;

namespace CMS.Controller
{
    
    [Authorize]
    public class AjaxCustomerGroup
    {
        CustomerGroupBLL CustomerGroupBLL = Factory.BusinessFactory.CreateBll<CustomerGroupBLL>();
        SqlWhereList where = new SqlWhereList();
        /// <summary>
        /// 用户组导入
        /// </summary>
        /// <param name="CustomerGroup"></param>
        [Action]
        public void ImportData(CustomerGroup CustomerGroup)
        {
            if (string.IsNullOrEmpty(CustomerGroup.GroupName))
            {
                ScriptHelper.Alert("请添加一个用户组", "/popupwindow/customergroup.aspx");
            }
            HttpPostedFile upfile = HttpContext.Current.Request.Files["UploadPath"];

            string fileExt = upfile.FileName.Substring(upfile.FileName.LastIndexOf(".") + 1);
            if (!FileHelper.CheckFileExt("txt", fileExt))
            {
                ScriptHelper.Alert("文件类型不对", "/popupwindow/customergroup.aspx");
            }
 
           string filename = DateTime.Now.ToString("ddHHmmssfff") + Path.GetExtension(upfile.FileName);
           string localPath = HttpContext.Current.Request.PhysicalApplicationPath + ("tempfile/" + DateTime.Now.ToString("yyyyMM") + "/");
           //判断文件夹是否存在, 不存在则创建
           if (!System.IO.Directory.Exists(localPath))
               System.IO.Directory.CreateDirectory(localPath);

           try
           {
               //本地创建上传的图片文件
               upfile.SaveAs(Path.Combine(localPath, filename));
           }
           catch (Exception)
           {
               ScriptHelper.Alert("上传异常", "/popupwindow/customergroup.aspx");
           }
           string path = "/tempfile/" + DateTime.Now.ToString("yyyyMM") + "/" + filename;
           
            string[] strs = File.ReadAllLines(HttpContext.Current.Server.MapPath(path),Encoding.GetEncoding("gb2312"));
            CustomerGroupBLL.DeleteGroup(CustomerGroup.GroupName);
            DataTable dt = new DataTable();
            dt = GetTableSchema();
            int i = 0;
            foreach (string str in strs)
            {
                    string[] arr = Regex.Split(str, ",", RegexOptions.None);
                    DataRow r = dt.NewRow();
                    r[0] = 0;
                    r[1] = CustomerGroup.GroupName;
 
                    try
                    {
                        r[2] =int.Parse(arr[0]);
                    }
                    catch
                    {
                        ScriptHelper.Alert("类型不正确", "/popupwindow/customergroup.aspx");
                    }
                    r[3] = arr[1];

                    r[4] = 1;
                    r[5] = UserCookies.AdminName;
                    r[6] = DateTime.Now;
                    r[7] = CustomerGroup.RelevanceId;

                    dt.Rows.Add(r);
           
                    if ((i > 0 && i % 100000 == 0) || i == strs.Length - 1)
                    {
                        CustomerGroupBLL.BulkToDB(dt);
                        dt.Reset();
                        dt = GetTableSchema();
                    }
                    i++;
            }
              if (File.Exists(localPath + filename))
                  File.Delete(localPath + filename);
              ScriptHelper.Alert("操作成功", "/popupwindow/customergroup.aspx");
        }


        /// <summary>
        /// 构建和数据库ReceiveMsg表结构一样的DataTable对象
        /// </summary>
        /// <returns></returns>
        private DataTable GetTableSchema()
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[]{
                new DataColumn("Id",typeof(int)),
                new DataColumn("GroupName",typeof(string)),
                new DataColumn("CustomerId",typeof(int)),
                new DataColumn("CustomerName",typeof(string)),
                new DataColumn("GroupType",typeof(int)),
                new DataColumn("Editor",typeof(string)),
                new DataColumn("CreatedTime",typeof(DateTime)),
                new DataColumn("RelevanceId",typeof(string))
            });
            return dt;
        }

        /// <summary>
        /// 获取用户组
        /// </summary>
        /// <returns></returns>
        [Action]
        public string GetGroupList()
        {
            string returndata = CustomerGroupBLL.GetGroupList();
            if (returndata.IndexOf("result") >= 0)
            {
                return  "000001";
            }
            else if (string.IsNullOrEmpty(returndata))
            {
                return "000002";
            }
            return returndata;

        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <returns></returns>
        [Action]
        public string GetUserDataList(int groupId, string groupName)
        {       
            DataTable dt = new DataTable();
            dt = GetTableSchema();
            int i = 0;

           string returndata= CustomerGroupBLL.GetUserDataList(groupId);
           if (returndata.IndexOf("result") >= 0)
           {
               return "000001";
           }
           else if (string.IsNullOrEmpty(returndata))
           {
               return "000002";
           }

           if (!string.IsNullOrEmpty(returndata))
           {
               CustomerGroupBLL.DeleteGroup(groupName);
               string[] arr = Regex.Split(returndata, "\r\n", RegexOptions.None);
               for (i = 1; i < arr.Length-1; i++)
               {
                   string[] userarr = Regex.Split(arr[i], ",", RegexOptions.None);
                   DataRow r = dt.NewRow();
                   r[0] = 0;
                   r[1] = groupName;
                   try
                   {
                       r[2] = int.Parse(userarr[0]);
                   }
                   catch
                   {
                       ScriptHelper.Alert("类型不正确", "/popupwindow/customergroup.aspx");
                   }
                   r[3] = userarr[1];

                   r[4] = 2;
                   r[5] = UserCookies.AdminName;
                   r[6] = DateTime.Now;
                   try
                   {
                       r[7] = int.Parse(userarr[2]);
                   }
                   catch
                   {
                       ScriptHelper.Alert("没有获取到版本号", "/popupwindow/customergroup.aspx");
                   }
                   dt.Rows.Add(r);

                   if ((i > 1 && i % 100000 == 0) || i == arr.Length-2)
                   {
                       CustomerGroupBLL.BulkToDB(dt);
                       dt.Reset();
                       dt = GetTableSchema();
                   }
               }
           }
           
           return returndata;
        }

         

    }
}
