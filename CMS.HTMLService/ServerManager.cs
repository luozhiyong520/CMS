using System;
using System.ServiceModel;
using Autofac;
using Autofac.Integration.Wcf;

namespace CMS.HtmlService
{
    /// <summary>
    /// WCF宿主管理器
    /// </summary>
    public class ServerManager<T, T1>
    {

        private readonly ServiceHost serviceHost;
        private readonly IContainer container;

        public ServerManager(IContainer container)
        {
            this.container = container;


            serviceHost = new ServiceHost(typeof(T));
            serviceHost.AddDependencyInjectionBehavior<T1>(container);
            serviceHost.Faulted += ServiceHostFaulted;
            serviceHost.UnknownMessageReceived += ServiceHostUnknownMessageReceived;
            serviceHost.Description.Behaviors.Add(container.Resolve<HtmlServiceBehavior>());
        }

        public bool IsStarted
        {
            get { return serviceHost.State == CommunicationState.Opened; }
        }

        public void Start()
        {
            try
            {
                serviceHost.Open();
                Common.Loger.Info(string.Format("己成功启动{0}服务。", GetTypeName()));
            }
            catch (Exception ex)
            {
                serviceHost.Abort();
                Common.Loger.Error(ex,string.Format("{0}服务启动失败：\r\n{1}", GetTypeName()));
            }
        }

        private static string GetTypeName()
        {
            return typeof(T).Name;
        }

        public void Stop()
        {
            Common.Loger.Info(string.Format("停止{0}服务。", GetTypeName()));

            try
            {
                serviceHost.Close();
            }
            catch (Exception ex)
            {
                serviceHost.Abort();
                Common.Loger.Error(ex,string.Format("停止{0}服务遇到错误", GetTypeName()));
            }

            Common.Loger.Info(string.Format("{0}服务已停止。", GetTypeName()));
        }

        private void ServiceHostUnknownMessageReceived(object sender, UnknownMessageReceivedEventArgs e)
        {
            Common.Loger.Warn(string.Format("收到无法识别的消息。\r\n{0}", e.Message));
        }

        private void ServiceHostFaulted(object sender, EventArgs e)
        {
            Common.Loger.Error(string.Format("{0}服务出错。", GetTypeName()));
        }
    }
}
