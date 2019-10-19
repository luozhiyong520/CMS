using CMS.Controller;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using SMVC;

namespace CMS.Test
{
    
    
    /// <summary>
    ///这是 AjaxPageTemplateTest 的测试类，旨在
    ///包含所有 AjaxPageTemplateTest 单元测试
    ///</summary>
    [TestClass()]
    public class AjaxPageTemplateTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///获取或设置测试上下文，上下文提供
        ///有关当前测试运行及其功能的信息。
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region 附加测试特性
        // 
        //编写测试时，还可使用以下特性:
        //
        //使用 ClassInitialize 在运行类中的第一个测试前先运行代码
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //使用 ClassCleanup 在运行完类中的所有测试后再运行代码
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //使用 TestInitialize 在运行每个测试前先运行代码
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //使用 TestCleanup 在运行完每个测试后运行代码
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///SearchPageTemplate 的测试
        ///</summary>
        [TestMethod()]
        public void SearchPageTemplateTest()
        {
            AjaxPageTemplate target = new AjaxPageTemplate(); // TODO: 初始化为适当的值
            int templateType = 0; // TODO: 初始化为适当的值
            string templateName = string.Empty; // TODO: 初始化为适当的值
            string fileName = string.Empty; // TODO: 初始化为适当的值
            Nullable<int> templateId = new Nullable<int>(); // TODO: 初始化为适当的值
            Nullable<int> page =1; // TODO: 初始化为适当的值
            int rows = 10; // TODO: 初始化为适当的值
            JsonResult expected = null; // TODO: 初始化为适当的值
            JsonResult actual;
            actual = target.SearchPageTemplate(templateType, templateName, fileName, templateId, page, rows);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void FtpTest()
        {
            string errorinfo;
            Common.FtpHelper ftp = new Common.FtpHelper();
            ftp.FtpUpDown("192.168.240.63", "vsftp", "/.,qwe123");
            ftp.MakeDir("/upimg/201308/");
            bool flag = ftp.Upload("E:\\test\\/upimg/201308/09133937235.jpg", "/upimg/201308/231.jpg", out errorinfo);
        }
    }
}
