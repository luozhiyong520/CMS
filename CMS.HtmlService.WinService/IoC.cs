using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Dispatcher;
using Autofac;
using Autofac.Builder;
using CMS.HtmlService;
using CMS.HtmlService.Contract;


namespace CMS.HtmlService.WinService
{
    public static class IoC
    {
        private static readonly IContainer Container;

        static IoC()
        {

            var builder = new ContainerBuilder();
            builder.Register(c => new ServerManager<HtmlEngine, IHtmlEngine>(Container)).SingleInstance();
            builder.Register(c => new ServerManager<FileReceive, IFileReceive>(Container)).SingleInstance();
            builder.Register(c => new ServerManager<SsoOutWindow, ISsoOutWindow>(Container)).SingleInstance();
            builder.RegisterType<HtmlEngine>().As<IHtmlEngine>();
            builder.RegisterType<FileReceive>().As<IFileReceive>();
            builder.RegisterType<SsoOutWindow>().As<ISsoOutWindow>();
            //builder.Register(c =>
            //                     {
            //                         ILog log = LogManager.GetLogger("log");
            //                         var fi = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "\\log4net.config");
            //                         XmlConfigurator.Configure(fi);
            //                         XmlConfigurator.ConfigureAndWatch(fi);
            //                         return log;
            //                     }).As<ILog>()
            //    .SingleInstance();
            //builder.Register(c =>
            //                    {
            //                        IErrorLog errorlog = (IErrorLog)LogManager.GetLogger("logerror");
            //                        return errorlog;
            //                    }).As<IErrorLog>()
            //                    .SingleInstance();
            builder.RegisterType<ErrorHandler>().As<IErrorHandler>();
            builder.Register(c => new HtmlServiceBehavior(c.Resolve<IErrorHandler>()));
            Container = builder.Build(ContainerBuildOptions.None);

        }

        public static T Resolve<T>()
        {
            return Container.Resolve<T>();
        }

        public static void Dispose()
        {
            Container.Dispose();
        }
    }
}
