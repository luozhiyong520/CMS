using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Linq.Expressions;
using System.Web;

namespace SMVC
{
    public class DynamicMethodExecutor
    {
        private Func<object, object[], object> executor;

        public object MethodExecute(object instance, object[] parms)
        {
            //if (parms != null)
            //{
            //    foreach (object obj in parms)
            //    {
            //        if(obj==null)
            //            throw new HttpException("你输入的参数有误");
            //    }
            //}
            return this.executor(instance, parms);
        }

        public DynamicMethodExecutor(MethodInfo m)
        {
            this.executor = getDelegateMethod(m);
        }

        private Func<object, object[], object> getDelegateMethod(MethodInfo methodinfo)
        {
            ParameterExpression instance = Expression.Parameter(typeof(object), "instance");
            ParameterExpression parms = Expression.Parameter(typeof(object[]), "parms");
            List<Expression> parmsExpression = new List<Expression>();
            ParameterInfo[] paraminfos = methodinfo.GetParameters();
            for (int i = 0; i < paraminfos.Length; i++)
            {
                BinaryExpression valueObj = Expression.ArrayIndex(parms, Expression.Constant(i));
                UnaryExpression valueCast = Expression.Convert(valueObj, paraminfos[i].ParameterType);
                parmsExpression.Add(valueCast);
            }
            Expression instanceCast = methodinfo.IsStatic ? null : Expression.Convert(instance, methodinfo.ReflectedType);

            MethodCallExpression methodCall = Expression.Call(instanceCast, methodinfo, parmsExpression);

            if (methodCall.Type == typeof(void))
            {
                Expression<Action<object, object[]>> exp = Expression.Lambda<Action<object, object[]>>(methodCall, instance, parms);
                Action<object, object[]> action = exp.Compile();
                return (instanceObj, parmsObj) =>
                {
                    action(instanceObj, parmsObj);
                    return null;
                };
            }
            else
            {

                UnaryExpression call = Expression.Convert(methodCall, typeof(object));
                Expression<Func<object, object[], object>> func = Expression.Lambda<Func<object, object[], object>>(call, instance, parms);
                return func.Compile();
            }
        }

    }
}
