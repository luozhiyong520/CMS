using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;


namespace CMS.HtmlService
{
    public class ErrorHandler : IErrorHandler
    {


        public ErrorHandler()
        {
        }

        #region IErrorHandler Members

        public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
        {
            Common.Loger.Error(error);
        }

        public bool HandleError(Exception error)
        {
            Common.Loger.Error(error);
            return true;
        }

        #endregion
    }
}
