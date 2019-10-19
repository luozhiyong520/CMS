using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Web;
using System.Configuration;

namespace SMVC
{
    internal class ModelHelper
    {
        public static object GetValueByNameAndTypeFromRequest(HttpRequest request, string name,Type paramsType,string paramPrefix)
        {
            object obj = GetHttpValue(request, name);
            if (obj == null && !string.IsNullOrEmpty(paramPrefix))
                obj = GetHttpValue(request, string.Format("{0}.{1}", paramPrefix, name));

            return SafeSetTypeChange(obj, paramsType);
        }

        public static object SafeSetTypeChange(object obj,Type paramsType)
        {
            if (obj == null) //没有传参数的情况
                return null;

            //if (string.IsNullOrWhiteSpace(obj.ToString())) //参数传空值的情况
            //    return "";
            try
            {
                if (paramsType.IsEnum)
                    return Enum.Parse(paramsType, obj.ToString());
                return ZBConvert.ChangeType(obj, paramsType);
            }
            catch
            {
                return null;
            }
        }

        public static object GetHttpValue(HttpRequest request, string name)
        {
            string obj = request.Form[name];
            if (string.IsNullOrEmpty(obj))
                obj = request.QueryString[name];
            return obj;           
        }

        public static void FillModel(HttpRequest request, object item, string paramName)
        {
            ModelDescription modelDesc = ReflectionHelper.GetModelDescription(item.GetType());
            if (modelDesc != null && modelDesc.Fields != null)
            {
                foreach (DataMember dm in modelDesc.Fields)
                {
                    Type paramRealType = dm.DataType.GetReallyType();
                    if (paramRealType.IsSimplyType())
                    {
                        object obj = GetValueByNameAndTypeFromRequest(request, dm.Name,dm.DataType, paramName);
                        dm.SetValue(item, obj);
                    }
                }
            }
        }
    }
}
