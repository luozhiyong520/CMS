using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMS.HtmlService;
using CMS.HtmlService.Contract;
using CMS.HtmlService.WinService;

namespace CMS.HtmlService.WinConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            bindExceptionHandler();
            IoC.Resolve<ServerManager<HtmlEngine, IHtmlEngine>>().Start();
            IoC.Resolve<ServerManager<FileReceive, IFileReceive>>().Start();
            IoC.Resolve<ServerManager<SsoOutWindow, ISsoOutWindow>>().Start();
            Console.ReadKey();

            IoC.Resolve<ServerManager<HtmlEngine, IHtmlEngine>>().Stop();
            IoC.Resolve<ServerManager<FileReceive, IFileReceive>>().Stop();
            IoC.Resolve<ServerManager<SsoOutWindow, ISsoOutWindow>>().Stop();
            IoC.Dispose();
        }

        /// <summary>
        /// 绑定程序中的异常处理
        /// </summary>
        private static void bindExceptionHandler()
        {
            //处理未捕获的异常
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Common.Loger.Error(e.ExceptionObject as Exception);
        }
    }
}
