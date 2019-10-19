using CMS.HtmlService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Drawing;

namespace CMS.Test
{
    
    
    /// <summary>
    ///这是 FileReceiveTest 的测试类，旨在
    ///包含所有 FileReceiveTest 单元测试
    ///</summary>
    [TestClass()]
    public class FileReceiveTest
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
        ///GetPic 的测试
        ///</summary>
        [TestMethod()]
        public void GetPicTest()
        {
            //FileReceive target = new FileReceive(); // TODO: 初始化为适当的值
            //string picname = "test"; // TODO: 初始化为适当的值
            //Bitmap expected = null; // TODO: 初始化为适当的值
            //Bitmap actual;
            //actual = target.GetPic(picname);
            //Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///isCreateDir 的测试
        ///</summary>
        [TestMethod()]
        [DeploymentItem("CMS.HtmlService.dll")]
        public void isCreateDirTest()
        {
            FileReceive_Accessor target = new FileReceive_Accessor(); // TODO: 初始化为适当的值
            string fullPath = "E:\\aaa\\bbb ccc\\cccc_ddd\\alkf.xxx"; // TODO: 初始化为适当的值
            target.isCreateDir(fullPath);
            Assert.Inconclusive("无法验证不返回值的方法。");
        }
    }
}
