using CMS.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CMS.Test
{
    
    
    /// <summary>
    ///这是 NewsTest 的测试类，旨在
    ///包含所有 NewsTest 单元测试
    ///</summary>
    [TestClass()]
    public class NewsTest
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
        ///FullLink 的测试
        ///</summary>
        [TestMethod()]
        public void FullLinkTest()
        {
            News target = new News(); // TODO: 初始化为适当的值
            target.SecondTitle = "七省市碳排放交易试点密集推进 或今年6月开锣交易[WL]好，";
            target.SecondUrl = "http:///hyxw/201303/26165146022.html[WL]http://";
            target.TitleColor = "FFFFFF[WL]";
            target.IsBold = "0[WL]0";
            string actual;
            actual = target.FullLink;
            Assert.Inconclusive("验证此测试方法的正确性。");
        }
    }
}
