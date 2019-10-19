using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Web;

namespace SMVC
{
    internal class ControllerActionPair
    {
        public string Controller { get; set; }
        public string Action { get; set; }
    }

    internal class BaseDescription
    {
        public AuthorizeAttribute AuthorizeAtt { get; protected set; }
        public SessionModeAttribute SessionModeAtt { get; protected set; }
        public OutputCacheAttribute OutputCacheAtt { get; protected set; }

        protected BaseDescription(MemberInfo m)
        {
            AuthorizeAtt = m.GetMyAttribute<AuthorizeAttribute>();
            SessionModeAtt = m.GetMyAttribute<SessionModeAttribute>();
            OutputCacheAtt = m.GetMyAttribute<OutputCacheAttribute>();
        }

        protected BaseDescription(Type t)
        {
            AuthorizeAtt = t.GetMyAttribute<AuthorizeAttribute>();
            SessionModeAtt = t.GetMyAttribute<SessionModeAttribute>();
        }
    }

    internal class ControllerDescription : BaseDescription
    {
        public Type ControllerType { get; private set; }

        public ControllerDescription(Type t) : base(t)
        {
            this.ControllerType = t;
        }
    }

    internal class ActionDescription : BaseDescription
    {
        //为PageAction预留，AjaxAction暂时用不到这个属性
        public ControllerDescription PageController { get; set; }
        public ActionAttribute ActionAtt { get; private set; }
        public MethodInfo Method { get; private set; }
        public ParameterInfo[] Parameters { get; private set; }
        public bool HaseReturn { get; private set; }

        public ActionDescription(MethodInfo m, ActionAttribute a)
            : base(m)
        {
            this.ActionAtt = a;
            this.Method = m;
            this.Parameters = m.GetParameters();
            this.HaseReturn = m.ReturnType != typeof(void);
        }
    }

    internal class InvokeInfo
    {
        /// <summary>
        /// 控制器类
        /// </summary>
        public ControllerDescription ControllerDesc { get; set; }

        /// <summary>
        /// 控制器类里面的方法
        /// </summary>
        public ActionDescription ActionDesc { get; set; }

        public object Instance { get; set; }

        public InvokeInfo(ControllerDescription controller, ActionDescription action)
        {
            this.ControllerDesc = controller;
            this.ActionDesc = action;
        }

        public SessionStatus GetSessionStatus()
        {
            if (ActionDesc != null && ActionDesc.SessionModeAtt != null)
                return ActionDesc.SessionModeAtt.SessionModeStatus;
            if (ControllerDesc != null && ControllerDesc.SessionModeAtt != null)
                return ControllerDesc.SessionModeAtt.SessionModeStatus;
            return SessionStatus.NoSupport;
        }

        public AuthorizeAttribute GetAuthorizeAttr()
        {
            if (ActionDesc != null && ActionDesc.AuthorizeAtt != null)
                return ActionDesc.AuthorizeAtt;
            if (ControllerDesc != null && ControllerDesc.AuthorizeAtt != null)
                return ControllerDesc.AuthorizeAtt;
            return null;
        }

        public OutputCacheAttribute GetOutputCacheAttr()
        {
            if (ActionDesc != null && ActionDesc.OutputCacheAtt != null)
                return ActionDesc.OutputCacheAtt;
            if (ControllerDesc != null && ControllerDesc.OutputCacheAtt != null)
                return ControllerDesc.OutputCacheAtt;
            return null;
        }
    }

    internal class ModelDescription
    {
        public List<DataMember> Fields { get; set; }
    }

}
