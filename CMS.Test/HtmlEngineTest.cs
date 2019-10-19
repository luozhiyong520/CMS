using CMS.HtmlService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CMS.Test
{
    
    
    /// <summary>
    ///这是 HtmlEngineTest 的测试类，旨在
    ///包含所有 HtmlEngineTest 单元测试
    ///</summary>
    [TestClass()]
    public class HtmlEngineTest
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
        ///CreateSingleNews 的测试
        ///</summary>
        [TestMethod()]
        public void CreateSingleNewsTest()
        {
            HtmlEngine target = new HtmlEngine(); // TODO: 初始化为适当的值
            int newsId = 360; // TODO: 初始化为适当的值
            string operateType = "edit"; // TODO: 初始化为适当的值
            bool expected = false; // TODO: 初始化为适当的值
            bool actual;
            actual = target.CreateSingleNews(newsId, operateType);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///CreateTemplatePageByChannelId 的测试
        ///</summary>
        [TestMethod()]
        public void CreateTemplatePageByChannelIdTest()
        {
            HtmlEngine target = new HtmlEngine(); // TODO: 初始化为适当的值
            string channelId = "001006"; // TODO: 初始化为适当的值
            bool expected = false; // TODO: 初始化为适当的值
            bool actual;
            actual = target.CreateTemplatePageByChannelId(channelId);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

       

        /// <summary>
        ///CreateTemplatePage 的测试
        ///</summary>
        [TestMethod()]
        public void CreateTemplatePageTest()
        {
            HtmlEngine target = new HtmlEngine(); // TODO: 初始化为适当的值
            int templateId = 63; // TODO: 初始化为适当的值
            bool expected = false; // TODO: 初始化为适当的值
            bool actual;
            actual = target.CreateTemplatePage(templateId);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }






        /// <summary>
        ///GetTemplagePreview 的测试
        ///</summary>
        [TestMethod()]
        public void GetTemplagePreviewTest()
        {
            HtmlEngine target = new HtmlEngine(); // TODO: 初始化为适当的值
            int templateId = 63; // TODO: 初始化为适当的值
            string expected = string.Empty; // TODO: 初始化为适当的值
            string actual;
            actual = target.GetTemplagePreview(templateId, null);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }


        /// <summary>
        ///CreateSingleNews 的测试
        ///</summary>
        [TestMethod()]
        public void CreateSingleNewsTest1()
        {
            HtmlEngine target = new HtmlEngine(); // TODO: 初始化为适当的值
            int newsId = 0; // TODO: 初始化为适当的值
            string operateType = string.Empty; // TODO: 初始化为适当的值
            bool expected = false; // TODO: 初始化为适当的值
            bool actual;
            actual = target.CreateSingleNews(newsId, operateType);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }
    }
}
