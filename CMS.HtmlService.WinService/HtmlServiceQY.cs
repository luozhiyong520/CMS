using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using CMS.HtmlService.Contract;

namespace CMS.HtmlService.WinService
{
    public partial class HtmlServiceQY : ServiceBase
    {
        public HtmlServiceQY()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            IoC.Resolve<ServerManager<HtmlEngine, IHtmlEngine>>().Start();
            IoC.Resolve<ServerManager<FileReceive, IFileReceive>>().Start();
            IoC.Resolve<ServerManager<SsoOutWindow, ISsoOutWindow>>().Start();
        }

        protected override void OnStop()
        {
            IoC.Resolve<ServerManager<HtmlEngine, IHtmlEngine>>().Stop();
            IoC.Resolve<ServerManager<FileReceive, IFileReceive>>().Stop();
            IoC.Resolve<ServerManager<SsoOutWindow, ISsoOutWindow>>().Stop();
            IoC.Dispose();
        }
    }
}
