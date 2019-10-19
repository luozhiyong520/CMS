using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.SessionState;

namespace SMVC
{
    internal class ActionHandler : IHttpHandler
    {
        protected InvokeInfo invokeinfo;

        public static IHttpHandler CreateHandler(InvokeInfo invoke)
        {
            SessionStatus sessionStatus = invoke.GetSessionStatus();
            ActionHandler actionHandler = new ActionHandler();
            if (sessionStatus == SessionStatus.Support)
                actionHandler = new ActionRequiresSessionHandler();
            else if (sessionStatus == SessionStatus.ReadOnly)
                actionHandler = new ActionReadOnlySessionHandler();
            actionHandler.invokeinfo = invoke;
            return actionHandler;
        }

        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            ActionExecutor.ExecuteAction(context, invokeinfo);
        }
    }

    internal class ActionReadOnlySessionHandler : ActionHandler, IReadOnlySessionState 
    {
        
    }

    internal class ActionRequiresSessionHandler : ActionHandler, IRequiresSessionState 
    {
       
    }
}
