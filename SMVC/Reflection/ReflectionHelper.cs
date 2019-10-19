using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Data;
using System.Web.Compilation;
using System.Reflection;

namespace SMVC
{
    internal static class ReflectionHelper
    {
        //ajax服务类类型列表
        private static List<ControllerDescription> ajaxTypeList = new List<ControllerDescription>();

        //controller服务类类型列表
        private static List<ControllerDescription> controllerTypeList = new List<ControllerDescription>();

        private static Hashtable pageActionHash = Hashtable.Synchronized(new Hashtable());

        private static Hashtable pageUrlHash = Hashtable.Synchronized(new Hashtable());

        private static Hashtable ajaxActionHash = Hashtable.Synchronized(new Hashtable());

        private static Hashtable modelDescHash = Hashtable.Synchronized(new Hashtable());

        private static Hashtable dynamicMethodHash = Hashtable.Synchronized(new Hashtable());

        static ReflectionHelper()
        {
            initController();
        }

        private static void initController()
        {            
            var assemblies = BuildManager.GetReferencedAssemblies();
            try
            {
                foreach (Assembly m in assemblies)
                {
                    if (m.FullName.StartsWith("System", StringComparison.OrdinalIgnoreCase) || m.FullName.StartsWith("mscorlib",StringComparison.OrdinalIgnoreCase))
                        continue;
                    Type[] types = m.GetExportedTypes();
                    foreach (Type t in types)
                    {
                        if (t.Name.StartsWith("Ajax"))
                            ajaxTypeList.Add(new ControllerDescription(t));
                        else if (t.Name.EndsWith("Controller"))
                            controllerTypeList.Add(new ControllerDescription(t));
                    }
                }
            }
            catch{}

            //加载PageController的Action和PageUrl对应的Action
            foreach (ControllerDescription c in controllerTypeList)
            {
                MethodInfo[] methods = c.ControllerType.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static|BindingFlags.IgnoreCase);
                if (methods != null)
                {
                    ActionDescription actionDesc;
                    foreach (MethodInfo m in methods)
                    {
                        ActionAttribute[] actionAttrs = m.GetMyAttributes<ActionAttribute>();
                        if (actionAttrs != null && actionAttrs.Length > 0)
                        {
                            string key = c.ControllerType.FullName + "_" + m.Name;
                            actionDesc = new ActionDescription(m, actionAttrs[0]) { PageController = c };                           
                            pageActionHash[key] = actionDesc;
                            PageUrlAttribute[] pageurlAttrs = m.GetMyAttributes<PageUrlAttribute>();
                            if (pageurlAttrs != null)
                            {
                                foreach (PageUrlAttribute p in pageurlAttrs)
                                {
                                    if (!string.IsNullOrEmpty(p.Url) && !pageUrlHash.ContainsKey(p.Url.ToLower()))
                                        pageUrlHash.Add(p.Url.ToLower(), actionDesc);
                                }
                            }
                        }
                    }
                }
            }
            //AjaxController，运用延迟加载
        }

        public static InvokeInfo GetInvokeInfo(string virtualPath)
        {
            ActionDescription actionDesc = pageUrlHash[virtualPath.ToLower()] as ActionDescription;
            if (actionDesc == null)
                return null; //返回null的目的是在aspx页面没有找到的情况下，用aspx默认的httphandler处理
            InvokeInfo invoke = new InvokeInfo(actionDesc.PageController, actionDesc);
            if (!invoke.ActionDesc.Method.IsStatic)
                invoke.Instance = Activator.CreateInstance(actionDesc.PageController.ControllerType);
            return invoke;
        }

        public static InvokeInfo GetInvokeInfo(ControllerActionPair pair)
        {
            ControllerDescription controllerDesc = getControllerDescription(pair);
            if (controllerDesc == null)
                throw new ArgumentNullException("controllerDesc");

            ActionDescription actionDesc = getActionDescription(controllerDesc, pair);
            if (actionDesc == null)
                throw new ArgumentNullException("actionDesc");
            InvokeInfo invoke = new InvokeInfo(controllerDesc, actionDesc);
            if (!invoke.ActionDesc.Method.IsStatic)
                invoke.Instance = Activator.CreateInstance(controllerDesc.ControllerType);
            return invoke;
        }

        private static ControllerDescription getControllerDescription(ControllerActionPair pair)
        {
            ControllerDescription controllerDesc;
            if (pair.Controller.IndexOf('.') > 0)
            {
                controllerDesc = ajaxTypeList.FirstOrDefault(c => c.ControllerType.FullName.Equals(pair.Controller));
            }
            else
            {
                controllerDesc = ajaxTypeList.FirstOrDefault(c => c.ControllerType.Name.Equals(pair.Controller));
            }
            return controllerDesc;
        }


        /// <summary>
        /// 获取AjaxController
        /// </summary>
        /// <param name="controllerDesc"></param>
        /// <param name="pair"></param>
        /// <returns></returns>
        private static ActionDescription getActionDescription(ControllerDescription controllerDesc, ControllerActionPair pair)
        {
            string hashKey = string.Format("{0}_{1}", controllerDesc.ControllerType.FullName, pair.Action).ToLower();
            ActionDescription actionDesc = ajaxActionHash[hashKey] as ActionDescription;
            if (actionDesc == null)
            {
                MethodInfo method = controllerDesc.ControllerType.GetMethod(pair.Action, BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.IgnoreCase);
                if (method != null)
                {
                    ActionAttribute[] actionAttrs = method.GetMyAttributes<ActionAttribute>();
                    if (actionAttrs != null && actionAttrs.Length > 0)
                    {
                        actionDesc = new ActionDescription(method, actionAttrs[0]);
                        actionDesc.PageController = controllerDesc;
                        ajaxActionHash[hashKey] = actionDesc;
                    }
                }
            }
            return actionDesc;
        }

        /// <summary>
        /// 获取表达式树创建的动态方法
        /// </summary>
        /// <returns></returns>
        public static DynamicMethodExecutor GetDynamicMethod(InvokeInfo invoke)
        {
            if (invoke == null)
                throw new ArgumentNullException("invoke");
            string hashKey = string.Format("{0}_{1}", invoke.ControllerDesc.ControllerType.FullName, invoke.ActionDesc.Method.Name);
            DynamicMethodExecutor executor = dynamicMethodHash[hashKey] as DynamicMethodExecutor;
            if (executor == null)
            {
                executor = new DynamicMethodExecutor(invoke.ActionDesc.Method);
                dynamicMethodHash[hashKey] = executor;
            }
            return executor;
        }


        /// <summary>
        /// 获取自定义类型的公共属性和公共字段
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static ModelDescription GetModelDescription(Type type)
        {
            if (type == null)
                throw new ArgumentNullException("type");
            string key = type.FullName;
            ModelDescription modelDesc = modelDescHash[key] as ModelDescription;
            if (modelDesc == null)
            {
                modelDesc = new ModelDescription();
                modelDesc.Fields = new List<DataMember>();
                (from pi in type.GetProperties(BindingFlags.Public | BindingFlags.Instance )
                 let noreadonly = pi.GetCustomAttributes(typeof(ReadOnlyAttribute), false)
                 where noreadonly.Length==0
                 select new PropertyMember(pi)).ToList().ForEach(pm => modelDesc.Fields.Add(pm));

                (from fi in type.GetFields(BindingFlags.Public | BindingFlags.Instance)
                 let noreadonly = fi.GetCustomAttributes(typeof(ReadOnlyAttribute), false)
                 where noreadonly.Length == 0
                 select new FieldMember(fi)).ToList().ForEach(fi => modelDesc.Fields.Add(fi));
                modelDescHash[key] = modelDesc;
            }
            return modelDesc;
        }

        /// <summary>
        /// 获取用户的自定义属性的自定义方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="m"></param>
        /// <returns></returns>
        public static T GetMyAttribute<T>(this MemberInfo m) where T : Attribute
        {
            if (m == null)
                return null;
            T[] attributes = m.GetCustomAttributes(typeof(T), false) as T[];
            if (attributes != null && attributes.Length > 0)
                return attributes[0];
            return null;
        }

        public static T[] GetMyAttributes<T>(this MemberInfo m) where T : Attribute
        {
            if (m == null)
                return null;
            return m.GetCustomAttributes(typeof(T), false) as T[];
        }

        public static Type GetReallyType(this Type type)
        {
            if (type.IsGenericType)
                return Nullable.GetUnderlyingType(type) ?? type;
            else
                return type;
        }

        public static bool IsSimplyType(this Type type)
        {
            if (type.IsPrimitive || type == typeof(decimal) || type == typeof(string) || type == typeof(DateTime) || type.IsEnum)
                return true;
            return false;
        }
    }
}
