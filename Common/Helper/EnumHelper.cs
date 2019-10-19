using System;
using System.Linq;
using System.Reflection;

namespace Common
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class EnumHelpAttribute : Attribute
    {
        public string DisplayValue { get; set; }
    }

    public class EnumHelper
    {
        public static string GetEnumDisplayValue<TEnum>(TEnum enumObj)
        {
            FieldInfo fieldInfo = typeof(TEnum).GetField(enumObj.ToString());

            object[] attribArray = fieldInfo.GetCustomAttributes(false);
            if (attribArray.Length == 0)
            {
                return String.Empty;
            }
            else
            {
                EnumHelpAttribute attrib = attribArray[0] as EnumHelpAttribute;

                return attrib.DisplayValue;
            }
        }

        /// <summary>
        /// 根据displayvalue 获取枚举
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static TEnum GetByDisplayValue<TEnum>(string value)
        {
            if (!typeof(TEnum).IsEnum)
            {
                throw new Exception("类型TEnum必须为枚举类型...");
            }

            var values = Enum.GetValues(typeof(TEnum)).Cast<TEnum>();
            if (values.Count() == 0)
            {
                throw new Exception("枚举TEnum不存在任何枚举数...");
            }

            string displayValue=string.Empty;
            TEnum result=default(TEnum);

            foreach (TEnum field in values)
            {

                displayValue = GetEnumDisplayValue((TEnum)Enum.Parse(typeof(TEnum), field.ToString()));
                if (displayValue == value)
                {
                    result=field;
                    break;
                }
            }

            return result;            
        }

        /// <summary>
        /// 1:根据枚举对应的数值返回枚举
        /// 2:根据枚举对应的字符串返回枚举
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static TEnum Get<TEnum>(object value)
        {
            if (!typeof(TEnum).IsEnum)
            {
                throw new Exception("类型TEnum必须为枚举类型...");
            }
            var values = Enum.GetValues(typeof(TEnum)).Cast<TEnum>();
            if (values.Count() == 0)
            {
                throw new Exception("枚举TEnum不存在任何枚举数...");
            }
            if (!Enum.IsDefined(typeof(TEnum), value))
            {
                throw new Exception("类型TEnum中不存在对应的枚举...");
            }

            if (value.GetType() == typeof(string))
            {
                return (TEnum)Enum.Parse(typeof(TEnum), value.ToString());
            }

            return (TEnum)value;
        }       
    }
}
