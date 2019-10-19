using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Serialization;
using System.IO;
using System.Text.RegularExpressions;
using System.Runtime.Serialization.Formatters.Binary;

namespace Common
{
    /// <summary>
    /// xml读写工具
    /// </summary>
    public class XmlHelper
    {
        private XmlHelper(){}

        /// <summary>
        /// 从xml字符串还原对象.
        /// </summary>
        /// <param name="pXml"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object DeserializeFromXML(string pXml, System.Type type)
        {
            object obj = null;
            Stream stream = new FileStream(pXml, FileMode.Open, FileAccess.Read);
            BufferedStream bufferedStream = new BufferedStream(stream, 512);
            XmlTextReader reader = new XmlTextReader(bufferedStream);
            try
            {
                XmlSerializer ser = new XmlSerializer(type, "WlStock");
                obj = ser.Deserialize(reader);
            }
            catch (Exception ee)
            {
                throw ee;
            }
            finally
            {
                bufferedStream.Close();
                reader.Close();
            }
            return obj;
        }

        /// <summary>
        /// 从xml字符串还原对象.
        /// </summary>
        /// <param name="pXml"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object DeserializeFromURI(Stream stream, System.Type type)
        {
            object obj = null;
            XmlTextReader reader = new XmlTextReader(stream);
            try
            {
                XmlSerializer ser = new XmlSerializer(type, "WlStock");
                obj = ser.Deserialize(reader);
            }
            catch (Exception ee)
            {
                throw ee;
            }
            finally
            {
                reader.Close();
                stream.Close();
            }
            return obj;
        }


        /// <summary>
        /// 获取字符串
        /// </summary>
        /// <param name="pStringList"></param>
        /// <param name="pOjbectName"></param>
        /// <param name="pStringItemName"></param>
        /// <returns></returns>
        public static string GetStringArrayListXml(ArrayList pStringList, string pOjbectName, string pStringItemName)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement first = doc.CreateElement(pOjbectName);

            for (int i = 0; i < pStringList.Count; i++)
            {
                XmlElement item = doc.CreateElement(pStringItemName);
                item.InnerText = pStringList[i] as string;
                first.AppendChild(item);
            }
            doc.AppendChild(first);
            return GetDocumentString(doc);
        }

        /// <summary>
        /// 获取XMLDocument的字符串
        /// </summary>
        /// <param name="pDoc"></param>
        /// <returns></returns>
        public static string GetDocumentString(XmlDocument pDoc)
        {
            //获取字符串
            string str = "";
            StringWriter wr = new StringWriter();
            try
            {
                pDoc.Save(wr);
                str = wr.ToString();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                wr.Close();
            }
            return str;
        }

        /// <summary>
        /// 将对象序列化到文件里
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static void SerializeToXML(object obj, string toWritePath)
        {
            Stream stream = new FileStream(toWritePath, FileMode.Create, FileAccess.Write);
            BufferedStream bufferedStream = new BufferedStream(stream, 512);
            XmlTextWriter writer = new XmlTextWriter(bufferedStream, Encoding.GetEncoding("gb2312"));
            writer.Formatting = Formatting.Indented;
            try
            {
                System.Type type = obj.GetType();
                //序列化到文件中
                XmlSerializer ser = new XmlSerializer(type, "WlStock");
                ser.Serialize(writer, obj);
            }
            catch (Exception ee)
            {
                throw ee;
            }
            finally
            {
                stream.Close();
                writer.Close();
            }
        }

        private static byte[] readObject(object obj)
        {
            MemoryStream stream = new  MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream,   obj);   
            stream.Close();
            return stream.ToArray();
        }
                 
        private static object getObject(byte[] bytes)
        {
            MemoryStream stream = new  MemoryStream(bytes,   0,   bytes.Length);
            BinaryFormatter formatter = new BinaryFormatter();
            return formatter.Deserialize(stream);
        }

        /// <summary>
        /// 深克隆对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="original"></param>
        /// <returns></returns>
        public static T Clone<T>(object original)
        {
            Type t = original.GetType();
            return (T)getObject(readObject(original));
        }

        /// <summary>
        /// 从XML字符串中反序列化对象
        /// </summary>
        /// <typeparam name="T">结果对象类型</typeparam>
        /// <param name="s">包含对象的XML字符串</param>
        /// <param name="encoding">编码方式</param>
        /// <returns>反序列化得到的对象</returns>
        public static T XmlDeserialize<T>(string s, Encoding encoding)
        {
            if (string.IsNullOrEmpty(s))
                throw new ArgumentNullException("s");
            if (encoding == null)
                throw new ArgumentNullException("encoding");

            XmlSerializer mySerializer = new XmlSerializer(typeof(T));
            using (MemoryStream ms = new MemoryStream(encoding.GetBytes(s)))
            {
                using (StreamReader sr = new StreamReader(ms, encoding))
                {
                    return (T)mySerializer.Deserialize(sr);
                }
            }
        }

        /// <summary>
        /// 读入一个文件，并按XML的方式反序列化对象。
        /// </summary>
        /// <typeparam name="T">结果对象类型</typeparam>
        /// <param name="path">文件路径</param>
        /// <param name="encoding">编码方式</param>
        /// <returns>反序列化得到的对象</returns>
        public static T XmlDeserializeFromFile<T>(string path, Encoding encoding)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException("path");
            if (encoding == null)
                throw new ArgumentNullException("encoding");

            string xml = File.ReadAllText(path, encoding);
            return XmlDeserialize<T>(xml, encoding);
        }

        /// <summary>
        /// 将一个对象序列化为XML字符串
        /// </summary>
        /// <param name="o">要序列化的对象</param>
        /// <param name="encoding">编码方式</param>
        /// <returns>序列化产生的XML字符串</returns>
        public static string XmlSerialize(object o, Encoding encoding)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                XmlSerializeInternal(stream, o, encoding);

                stream.Position = 0;
                using (StreamReader reader = new StreamReader(stream, encoding))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        private static void XmlSerializeInternal(Stream stream, object o, Encoding encoding)
        {
            if (o == null)
                throw new ArgumentNullException("o");
            if (encoding == null)
                throw new ArgumentNullException("encoding");

            XmlSerializer serializer = new XmlSerializer(o.GetType());

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.NewLineChars = "\r\n";
            settings.Encoding = encoding;
            settings.IndentChars = "    ";

            using (XmlWriter writer = XmlWriter.Create(stream, settings))
            {
                serializer.Serialize(writer, o);
                writer.Close();
            }
        }
    }
}
