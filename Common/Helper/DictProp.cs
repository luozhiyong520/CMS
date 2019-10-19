using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Common
{
    /// <summary>
    /// 从字段中取自定义属性的操作类
    /// </summary>
    public class DictProp
    {
        /// <summary>
        /// 包含属性的字符串
        /// </summary>
        private String PropStr;

        /// <summary>
        /// 包含属性的Hash表
        /// </summary>
        private Dictionary<string, string> propDict;

        /// <summary>
        /// 行分隔符
        /// </summary>
        String Split1 = "\r\n";

        /// <summary>
        /// 行内属性名称和值之间的分隔符
        /// </summary>
        String Split2 = "=";

        /// <summary>
        /// 根据要分析的字符串，第一分隔符和第二分隔符新建一个哈希对象
        /// </summary>
        /// <param name="propStr">待分解的字符串</param>
        /// <param name="split1">第一分隔符</param>
        /// <param name="split2">第二分隔符</param>
        public DictProp(String propStr, string split1, string split2)
        {
            PropStr = propStr;
            Split1 = split1;
            Split2 = split2;

            Str2Hash();
        }

        /// <summary>
        /// 键-值对
        /// </summary>
        public Dictionary<string, string> KeyValues
        {
            get { return propDict; }
        }

        /// <summary>
        /// 将字符串转成Hash表
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, string> Str2Hash()
        {
            if (propDict == null)
            {
                propDict = new Dictionary<string, string>();
            }
            else
            {
                propDict.Clear();
            }
            char s1 = '\n';
            char s2 = '=';
            String propstr = PropStr.Replace(Split1, s1.ToString()).Replace(Split2, s2.ToString());

            string[] propArr = propstr.Split(s1);

            foreach (String s in propArr)
            {
                string[] sArr = s.Split(new char[] { s2 }, 2);
                if (sArr.Length > 1)
                {
                    SetProp(sArr[0].Trim(), sArr[1].Trim());
                }
                else
                {
                    SetProp(sArr[0].Trim(), "");
                }
            }
            return propDict;
        }

        /// <summary>
        /// 当字典表为空时才将字符串转换成Hash表
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> Str2HashWhenEmpty()
        {
            if (propDict == null || propDict.Count == 0) Str2Hash();
            return propDict;
        }

        /// <summary>
        /// 强制字典表为空,再将字符串转换成Hash表
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> Str2HashReset()
        {
            propDict = null;
            Str2Hash();
            return propDict;
        }

        /// <summary>
        /// 将字典表转成字符串
        /// </summary>
        /// <returns></returns>
        public String Hash2Str()
        {
            if (propDict == null) return null;
            PropStr = "";

            foreach (KeyValuePair<String, String> k in propDict)
            {
                String s = Split1 + k.Key.ToString() + Split2 + k.Value.ToString();
                PropStr += s;
            }

            if (PropStr != "") PropStr = PropStr.Substring(Split1.Length);
            return PropStr;
        }

        /// <summary>
        /// 设置一个属性
        /// </summary>
        /// <param name="propName">属性名称</param>
        /// <param name="o">属性值</param>
        public void SetProp(String propName, object o)
        {
            propDict[propName] = Convert.ToString(o);
        }

        /// <summary>
        /// 获取字符型属性
        /// </summary>
        /// <param name="propName"></param>
        /// <returns></returns>
        public String GetPropStr(String propName)
        {
            return PropExists(propName) ? Convert.ToString(propDict[propName]) : String.Empty;
        }

        /// <summary>
        /// 获取整型属性
        /// </summary>
        /// <param name="propName"></param>
        /// <returns></returns>
        public int GetPropInt(String propName)
        {
            return PropExists(propName) ? Convert.ToInt32(propDict[propName]) : 0;
        }

        /// <summary>
        /// 是否存在指定属性名称
        /// </summary>
        /// <param name="propName"></param>
        /// <returns></returns>
        public bool PropExists(String propName)
        {
            return propDict.ContainsKey(propName);
        }

        /// <summary>
        /// 获取浮点型属性
        /// </summary>
        /// <param name="propName"></param>
        /// <returns></returns>
        public float GetPropFloat(String propName)
        {
            return PropExists(propName) ? (float)Convert.ToDouble(propDict[propName]) : 0;
        }

        /// <summary>
        /// 获取双精度数
        /// </summary>
        /// <param name="propName"></param>
        /// <returns></returns>
        public double GetPropDouble(String propName)
        {
            return PropExists(propName) ? Convert.ToDouble(propDict[propName]) : 0;
        }

        /// <summary>
        /// 获取日期
        /// </summary>
        /// <param name="propName"></param>
        /// <returns></returns>
        public DateTime GetPropDate(String propName)
        {
            return PropExists(propName) ? Convert.ToDateTime(propDict[propName]) : DateTime.MinValue;
        }

        /// <summary>
        /// 获取布尔型属性
        /// </summary>
        /// <param name="propName"></param>
        /// <returns></returns>
        public bool GetPropBool(String propName)
        {
            if (propDict.ContainsKey(propName))
            {
                return Convert.ToString(propDict[propName]).ToLower() == "true";
            }
            else
            {
                return false;
            }
        }
    }
}