using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Reflection;

namespace SMVC
{
    internal class ActionExecutor
    {
        public static void ExecuteAction(HttpContext context, InvokeInfo invoke)
        {
            if (context == null)
                throw new ArgumentNullException("context");
            if (invoke == null)
                throw new ArgumentNullException("invoke");

            if (invoke.GetAuthorizeAttr() !=null && 
                !invoke.GetAuthorizeAttr().IsAuthorizedARequest(context))
                ExceptionHelper.Throw403Exception(context);

            object result = ExecuteActionInternal(context, invoke);

            if (invoke.GetOutputCacheAttr() != null)
                invoke.GetOutputCacheAttr().SetResponseCache(context);           

            if (invoke.ActionDesc.HaseReturn && result != null)
            {
                IActionResult action = result as IActionResult;
                if (action != null)
                    action.Output(context);
                else
                {
                    context.Response.ContentType = "text/plain";
                    context.Response.Write(result.ToString());
                }
            }
        }

        internal static object ExecuteActionInternal(HttpContext context, InvokeInfo invoke)
        {
            if (context == null)
                throw new ArgumentNullException("context");
            if (invoke == null)
                throw new ArgumentNullException("invoke");
            object[] objs = getActionCallParameters(context, invoke.ActionDesc.Parameters);
            //DynamicMethodExecutor method = ReflectionHelper.GetDynamicMethod(invoke); ;
            if (invoke.ActionDesc.HaseReturn)
            {
                //return method.MethodExecute(invoke.Instance, objs);
                return invoke.ActionDesc.Method.Invoke(invoke.Instance, objs);
            }
            else
            {
                //method.MethodExecute(invoke.Instance, objs);               
                invoke.ActionDesc.Method.Invoke(invoke.Instance, objs);
                return null;
            }
        }


        private static object[] getActionCallParameters(HttpContext context, ParameterInfo[] paramInfos)
        {
            if (paramInfos == null && paramInfos.Length == 0)
                return null;
            int count = paramInfos.Length;
            //List<object> objs = new List<object>(); //这里没有用泛型定义方法参数的原因是,反射调用方面里面的object[]参数的个数需要和实际方法中的参数个数，顺序一致。即使某些参数的值为空，参数的位置需要占住。泛型有时候碰到不需要赋值的参数，不调用Add方法，可能导致反射提供参数和实际方法参数不一致的情况出现。
            object[] objs = new object[count];
            object obj;
            ParameterInfo p;
            for (int i = 0; i < count;i++ )
            {
                p = paramInfos[i];
                if (p.IsOut)
                    continue;
                Type paramRealType = p.ParameterType.GetReallyType();
                if (paramRealType.IsSimplyType())
                {
                    obj = ModelHelper.GetValueByNameAndTypeFromRequest(context.Request, p.Name, paramRealType, null);
                    if (obj != null)
                        objs[i] = obj;
                }
                else
                {
                    object instance = Activator.CreateInstance(paramRealType);
                    ModelHelper.FillModel(context.Request, instance, p.Name);
                    objs[i] = instance;
                }
            }
            return objs;
        }
    }
}
