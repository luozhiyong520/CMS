using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;


using CMS.Template.Enum;
using CMS.Template.Except;
using CMS.Template.Parser.AST;
using System.Text.RegularExpressions;


namespace CMS.Template.Complex
{
    public delegate object TemplateFunction(object[] args);
    public partial class TemplateManager
    {

    

        bool silentErrors;
        bool isPreview;  //自己添加的是否预览标志
        Dictionary<string, TemplateFunction> functions;

        Dictionary<string, ITagHandler> customTags;

        VariableScope variables;		// current variable scope
        Expression currentExpression;	// current expression being evaluated

        TextWriter writer;				// all output is sent here

        UserTemplate mainTemplate;			// main template to execute
        UserTemplate currentTemplate;		// current template being executed

        ITemplateHandler handler;		// handler will be set as "this" object

        /// <summary>
        /// create template manager using a template
        /// </summary>
        public TemplateManager(UserTemplate template,bool isPreview)
        {
            this.mainTemplate = template;
            this.currentTemplate = template;
            this.silentErrors = false;
            this.isPreview=isPreview;

            Init();
        }


        public static TemplateManager FromString(string template,bool isPreview)
        {
            UserTemplate itemplate = UserTemplate.FromString("", template);
            return new TemplateManager(itemplate,isPreview);
        }

        public static TemplateManager FromFile(string filename,bool isPreview)
        {
            UserTemplate template = UserTemplate.FromFile("", filename);
            return new TemplateManager(template,isPreview);
        }

        /// <summary>
        /// handler is used as "this" object, and will receive
        /// before after process message
        /// </summary>
        public ITemplateHandler Handler
        {
            get { return this.handler; }
            set { this.handler = value; }
        }

        /// <summary>
        /// if silet errors is set to true, then any exceptions will not show in the output
        /// If set to false, all exceptions will be displayed.
        /// </summary>
        public bool SilentErrors
        {
            get { return this.silentErrors; }
            set { this.silentErrors = value; }
        }

        private Dictionary<string, ITagHandler> CustomTags
        {
            get
            {
                if (customTags == null)
                    customTags = new Dictionary<string, ITagHandler>(StringComparer.CurrentCultureIgnoreCase);
                return customTags;
            }
        }

        /// <summary>
        /// registers custom tag processor
        /// </summary>
        public void RegisterCustomTag(string tagName, ITagHandler handler)
        {
            CustomTags.Add(tagName, handler);
        }

        /// <summary>
        /// checks whether there is a handler for tagName
        /// </summary>
        public bool IsCustomTagRegistered(string tagName)
        {
            return CustomTags.ContainsKey(tagName);
        }

        /// <summary>
        /// unregistered tagName from custom tags
        /// </summary>
        public void UnRegisterCustomTag(string tagName)
        {
            CustomTags.Remove(tagName);
        }

        /// <summary>
        /// adds template that can be used within execution 
        /// </summary>
        /// <param name="template"></param>
        public void AddTemplate(UserTemplate template)
        {
            mainTemplate.Templates.Add(template.Name, template);
        }

        void Init()
        {
            this.functions = new Dictionary<string, TemplateFunction>(StringComparer.InvariantCultureIgnoreCase);

            this.variables = new VariableScope();

            functions.Add("equals", new TemplateFunction(FuncEquals));
            functions.Add("notequals", new TemplateFunction(FuncNotEquals));
            functions.Add("iseven", new TemplateFunction(FuncIsEven));
            functions.Add("isodd", new TemplateFunction(FuncIsOdd));
            functions.Add("isempty", new TemplateFunction(FuncIsEmpty));
            functions.Add("isnotempty", new TemplateFunction(FuncIsNotEmpty));
            functions.Add("isnumber", new TemplateFunction(FuncIsNumber));
            functions.Add("toupper", new TemplateFunction(FuncToUpper));
            functions.Add("toescape", new TemplateFunction(FuncToEscape));
            functions.Add("tolower", new TemplateFunction(FuncToLower));
            functions.Add("isdefined", new TemplateFunction(FuncIsDefined));
            functions.Add("ifdefined", new TemplateFunction(FuncIfDefined));
            functions.Add("length", new TemplateFunction(FuncLength));
            functions.Add("tolist", new TemplateFunction(FuncToList));
            functions.Add("isnull", new TemplateFunction(FuncIsNull));
            functions.Add("gt", new TemplateFunction(FuncGt));
            functions.Add("lt", new TemplateFunction(FuncLt));
            functions.Add("not", new TemplateFunction(FuncNot));

            functions.Add("iif", new TemplateFunction(FuncIif));
            functions.Add("format", new TemplateFunction(FuncFormat));
            functions.Add("trim", new TemplateFunction(FuncTrim));
            functions.Add("filter", new TemplateFunction(FuncFilter));

            functions.Add("compare", new TemplateFunction(FuncCompare));
            functions.Add("or", new TemplateFunction(FuncOr));
            functions.Add("and", new TemplateFunction(FuncAnd));
            functions.Add("comparenocase", new TemplateFunction(FuncCompareNoCase));
            functions.Add("stripnewlines", new TemplateFunction(FuncStripNewLines));
            functions.Add("typeof", new TemplateFunction(FuncTypeOf));
            functions.Add("cint", new TemplateFunction(FuncCInt));
            functions.Add("cdouble", new TemplateFunction(FuncCDouble));
            functions.Add("cdate", new TemplateFunction(FuncCDate));
            functions.Add("cdatestring", new TemplateFunction(FuncCDateString));
            functions.Add("stringtounicode", new TemplateFunction(FuncStringToUnicode)); //自己添加的
            functions.Add("now", new TemplateFunction(FuncNow));
            functions.Add("createtypereference", new TemplateFunction(FuncCreateTypeReference));

            functions.Add("substr",new TemplateFunction(FunSubStr)); //自己添加的substring字符串

            functions.Add("filterhtml", new TemplateFunction(FunFilterHtml)); //自己添加过滤html
            functions.Add("filtertag", new TemplateFunction(FunFilterTag)); //自己添加过滤html
            functions.Add("filterts", new TemplateFunction(FunFilterTs)); //自己添加过滤html
        }

        #region Functions
        bool CheckArgCount(int count, string funcName, object[] args)
        {
            if (count != args.Length)
            {
                DisplayError(string.Format("{0} 函数要求有 {1} 个参数, 已传递 {2} 个参数.", funcName, count, args.Length), currentExpression.Line, currentExpression.Col);
                return false;
            }
            else
                return true;
        }

        bool CheckArgCount(int count1, int count2, string funcName, object[] args)
        {
            if (args.Length < count1 || args.Length > count2)
            {
                string msg = string.Format("{0} 函数要求至少有 {1} 到 {2} 个参数,已传递 {3} 个参数.", funcName, count1, count2, args.Length);
                DisplayError(msg, currentExpression.Line, currentExpression.Col);
                return false;
            }
            else
                return true;
        }

        object FuncIsEven(object[] args)
        {
            if (!CheckArgCount(1, "iseven", args))
                return null;

            try
            {
                int value = Convert.ToInt32(args[0]);
                return value % 2 == 0;
            }
            catch (FormatException)
            {
                throw new TemplateRuntimeException("IsEven 参数必须是整型.", currentExpression.Line, currentExpression.Col);
            }
        }

        object FuncIsOdd(object[] args)
        {
            if (!CheckArgCount(1, "isdd", args))
                return null;

            try
            {
                int value = Convert.ToInt32(args[0]);
                return value % 2 == 1;
            }
            catch (FormatException)
            {
                throw new TemplateRuntimeException("IsOdd 参数必须是整型.", currentExpression.Line, currentExpression.Col);
            }
        }

        /// <summary>
        /// changed by pf need check length
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private object FunSubStr(object[] args)
        {
            if (!CheckArgCount(2, "SubStr", args))
                return null;
            string sourceStr = args[0].ToString();
            int length=int.Parse(args[1].ToString());
            if(sourceStr.Length<length)
            {
                length=sourceStr.Length;
            }
            return sourceStr.Substring(0, length);
        }

        object FuncIsEmpty(object[] args)
        {
            if (!CheckArgCount(1, "isempty", args))
                return null;

            if (args[0] == null)
                return true;

            string value = args[0].ToString();
            return value.Length == 0;
        }

        object FuncIsNotEmpty(object[] args)
        {
            if (!CheckArgCount(1, "isnotempty", args))
                return null;

            if (args[0] == null)
                return false;

            string value = args[0].ToString();
            return value.Length > 0;
        }


        object FuncEquals(object[] args)
        {
            if (!CheckArgCount(2, "equals", args))
                return null;

            return args[0].Equals(args[1]);
        }
        object FuncGt(object[] args)
        {
            if (!CheckArgCount(2, "gt", args))
                return null;
            //int c1 = Convert.ToInt32(args[0]);
            //int c2 = Convert.ToInt32(args[1]);
            //return c1 > c2;
            IComparable c1 = args[0] as IComparable;
            IComparable c2 = args[1] as IComparable;
            if (c1 == null || c2 == null)
                return false;
            else
                return c1.CompareTo(c2) == 1;
        }

        object FuncLt(object[] args)
        {
            if (!CheckArgCount(2, "lt", args))
                return null;
            //int c1 = Convert.ToInt32(args[0]);
            //int c2 = Convert.ToInt32(args[1]);
            //return c1 < c2;
            IComparable c1 = args[0] as IComparable;
            IComparable c2 = args[1] as IComparable;
            if (c1 == null || c2 == null)
                return false;
            else
                return c1.CompareTo(c2) == -1;
        }

        object FuncNotEquals(object[] args)
        {
            if (!CheckArgCount(2, "notequals", args))
                return null;

            return !args[0].Equals(args[1]);
        }


        object FuncIsNumber(object[] args)
        {
            if (!CheckArgCount(1, "isnumber", args))
                return null;

            try
            {
                int value = Convert.ToInt32(args[0]);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        object FuncToUpper(object[] args)
        {
            if (!CheckArgCount(1, "toupper", args))
                return null;

            return args[0].ToString().ToUpper();
        }


        object FuncToEscape(object[] args)
        {
            if (!CheckArgCount(1, "toescape", args))
                return null;
            return Escape(args[0].ToString());
        }


        private string Escape(string s)
        {
            StringBuilder sb = new StringBuilder();
            byte[] byteArr = System.Text.Encoding.Unicode.GetBytes(s);

            for (int i = 0; i < byteArr.Length; i += 2)
            {
                sb.Append("%u");
                sb.Append(byteArr[i + 1].ToString("X2"));//把字节转换为十六进制的字符串表现形式  

                sb.Append(byteArr[i].ToString("X2"));
            }
            return sb.ToString();

        }


        object FuncToLower(object[] args)
        {
            if (!CheckArgCount(1, "toupper", args))
                return null;

            return args[0].ToString().ToLower();
        }

        object FuncLength(object[] args)
        {
            if (!CheckArgCount(1, "length", args))
                return null;

            return args[0].ToString().Length;
        }


        object FuncIsDefined(object[] args)
        {
            if (!CheckArgCount(1, "isdefined", args))
                return null;

            return variables.IsDefined(args[0].ToString());
        }

        object FuncIfDefined(object[] args)
        {
            if (!CheckArgCount(2, "ifdefined", args))
                return null;

            if (variables.IsDefined(args[0].ToString()))
            {
                return args[1];
            }
            else
                return string.Empty;
        }

        object FuncToList(object[] args)
        {
            if (!CheckArgCount(2, 3, "tolist", args))
                return null;

            object list = args[0];

            string property;
            string delim;

            if (args.Length == 3)
            {
                property = args[1].ToString();
                delim = args[2].ToString();
            }
            else
            {
                property = string.Empty;
                delim = args[1].ToString();
            }

            if (!(list is IEnumerable))
            {
                throw new TemplateRuntimeException("argument 1 of tolist has to be IEnumerable", currentExpression.Line, currentExpression.Col);
            }

            IEnumerator ienum = ((IEnumerable)list).GetEnumerator();
            StringBuilder sb = new StringBuilder();
            int index = 0;
            while (ienum.MoveNext())
            {
                if (index > 0)
                    sb.Append(delim);

                if (args.Length == 2) // do not evalulate property
                    sb.Append(ienum.Current);
                else
                {
                    sb.Append(EvalProperty(ienum.Current, property));
                }
                index++;
            }

            return sb.ToString();

        }

        object FuncIsNull(object[] args)
        {
            if (!CheckArgCount(1, "isnull", args))
                return null;

            return args[0] == null;
        }

        object FuncNot(object[] args)
        {
            if (!CheckArgCount(1, "not", args))
                return null;

            if (args[0] is bool)
                return !(bool)args[0];
            else
            {
                throw new TemplateRuntimeException("当前'not'的参数不是布尔型", currentExpression.Line, currentExpression.Col);
            }

        }

        object FuncIif(object[] args)
        {
            if (!CheckArgCount(3, "iif", args))
                return null;

            if (args[0] is bool)
            {
                bool test = (bool)args[0];
                return test ? args[1] : args[2];
            }
            else
            {
                throw new TemplateRuntimeException("当前'iif'的参数不是布尔型", currentExpression.Line, currentExpression.Col);
            }
        }

        object FuncFormat(object[] args)
        {
            if (!CheckArgCount(2, "format", args))
                return null;

            string format = args[1].ToString();

            if (args[0] is IFormattable)
                return ((IFormattable)args[0]).ToString(format, null);
            else
                return args[0].ToString();
        }

        object FuncTrim(object[] args)
        {
            if (!CheckArgCount(1, "trim", args))
                return null;

            return args[0].ToString().Trim();
        }

        object FuncFilter(object[] args)
        {
            if (!CheckArgCount(2, "filter", args))
                return null;

            object list = args[0];

            string property;
            property = args[1].ToString();

            if (!(list is IEnumerable))
            {
                throw new TemplateRuntimeException("argument 1 of filter has to be IEnumerable", currentExpression.Line, currentExpression.Col);
            }

            IEnumerator ienum = ((IEnumerable)list).GetEnumerator();
            List<object> newList = new List<object>();

            while (ienum.MoveNext())
            {
                object val = EvalProperty(ienum.Current, property);
                if (val is bool && (bool)val)
                    newList.Add(ienum.Current);
            }

            return newList;

        }

        object FuncCompare(object[] args)
        {
            if (!CheckArgCount(2, "compare", args))
                return null;

            IComparable c1 = args[0] as IComparable;
            IComparable c2 = args[1] as IComparable;
            if (c1 == null || c2 == null)
                return false;
            else
                return c1.CompareTo(c2);
        }

        object FuncOr(object[] args)
        {
            if (!CheckArgCount(2, "or", args))
                return null;

            if (args[0] is bool && args[1] is bool)
                return (bool)args[0] || (bool)args[1];
            else
                return false;
        }

        object FuncAnd(object[] args)
        {
            if (!CheckArgCount(2, "add", args))
                return null;

            if (args[0] is bool && args[1] is bool)
                return (bool)args[0] && (bool)args[1];
            else
                return false;
        }

        object FuncCompareNoCase(object[] args)
        {
            if (!CheckArgCount(2, "compareNoCase", args))
                return null;

            string s1 = args[0].ToString();
            string s2 = args[1].ToString();

            return string.Compare(s1, s2, true) == 0;
        }

        object FuncStripNewLines(object[] args)
        {
            if (!CheckArgCount(1, "StripNewLines", args))
                return null;

            string s1 = args[0].ToString();
            return s1.Replace(Environment.NewLine, " ");

        }

        object FuncTypeOf(object[] args)
        {
            if (!CheckArgCount(1, "TypeOf", args))
                return null;

            return args[0].GetType().Name;

        }

        object FuncCInt(object[] args)
        {
            if (!CheckArgCount(1, "cint", args))
                return null;

            return Convert.ToInt32(args[0]);
        }

        object FuncCDouble(object[] args)
        {
            if (!CheckArgCount(1, "cdouble", args))
                return null;

            return Convert.ToDouble(args[0]);
        }

        object FuncCDate(object[] args)
        {
            if (!CheckArgCount(1, "cdate", args))
                return null;

            return Convert.ToDateTime(args[0]);
        }

        object FuncCDateString(object[] args)
        {
            if (!CheckArgCount(2, "cdatestring", args))
                return null;

            return Convert.ToDateTime(args[0]).ToString(args[1].ToString());
        }

        /// <summary>
        /// 把string转成万国码，后加函数
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        object FuncStringToUnicode(object[] args)
        {
            if (!CheckArgCount(1, "stringtounicode", args))
                return null;
            if (args == null || args[0] == null)
                return null;
            char[] charbuffers = args[0].ToString().ToCharArray();
            byte[] buffer;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < charbuffers.Length; i++)
            {
                buffer = System.Text.Encoding.Unicode.GetBytes(charbuffers[i].ToString());
                sb.Append(String.Format("\\u{0:X2}{1:X2}", buffer[1], buffer[0]));
            }
            return sb.ToString();
        }

        object FuncNow(object[] args)
        {
            if (!CheckArgCount(0, "now", args))
                return null;

            return DateTime.Now;
        }

        /// <summary>
        /// 过滤html方法
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        object FunFilterHtml(object[] args)
        {
            if (!CheckArgCount(1, "filterhtml", args))
                return "";
            if (args == null || args[0] == null)
                return "";
            var str = Regex.Replace(args[0].ToString(), "<[^>]+>", "");
            str = Regex.Replace(str, "&nbsp;", "");
            return Regex.Replace(str, " ", "");
        }

        /// <summary>
        /// 过滤html将p替换成\r\n
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        object FunFilterTs(object[] args)
        {
            if (!CheckArgCount(1, "filterts", args))
                return "";
            if (args == null || args[0] == null)
                return "";
            var str = Regex.Replace(args[0].ToString(), "<p>", "");
            str = Regex.Replace(str, "</p>", "\r\n");
            str = Regex.Replace(str, "<[^>]+>", "");
            str = Regex.Replace(str, "&nbsp;", "");
            return Regex.Replace(str, " ", "");
        }
        /// <summary>
        /// 过滤html方法
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        object FunFilterTag(object[] args)
        {
            if (!CheckArgCount(1, "filtertag", args))
                return "";
            if (args == null || args[0] == null)
                return "";
            var str = Regex.Replace(args[0].ToString(), "<[^>]+>", "");
            return str;
         }

        object FuncCreateTypeReference(object[] args)
        {
            if (!CheckArgCount(1, "createtypereference", args))
                return null;

            string typeName = args[0].ToString();


            Type type = System.Type.GetType(typeName, false, true);
            if (type != null)
                return new StaticTypeReference(type);

            Assembly[] asms = AppDomain.CurrentDomain.GetAssemblies();
            foreach (Assembly asm in asms)
            {
                type = asm.GetType(typeName, false, true);
                if (type != null)
                    return new StaticTypeReference(type);
            }

            throw new TemplateRuntimeException("无法创建类型 " + typeName + ".", currentExpression.Line, currentExpression.Col);
        }

        #endregion

        /// <summary>
        /// gets library of functions that are available
        /// for the tempalte execution
        /// </summary>
        public Dictionary<string, TemplateFunction> Functions
        {
            get { return functions; }
        }

        /// <summary>
        /// sets value for variable called name
        /// </summary>
        public void SetValue(string name, object value)
        {
            variables[name] = value;
        }

        /// <summary>
        /// gets value for variable called name.
        /// Throws exception if value is not found
        /// </summary>
        public object GetValue(string name)
        {
            if (variables.IsDefined(name))
                return variables[name];
            else
                throw new Exception("变量'" + name + "'不存在.");
        }

        /// <summary>
        /// processes current template and sends output to writer
        /// </summary>
        /// <param name="writer"></param>
        public void Process(TextWriter writer)
        {
            this.writer = writer;
            this.currentTemplate = mainTemplate;

            if (handler != null)
            {
                SetValue("this", handler);
                handler.BeforeProcess(this);
            }

            ProcessElements(mainTemplate.Elements);

            if (handler != null)
                handler.AfterProcess(this);
        }

        /// <summary>
        /// 解析自定义模板内容
        /// </summary>
        /// <param name="templateContent"></param>
        /// <param name="writer"></param>
        public void Process(string templateContent, TextWriter writer)
        {
            this.writer = writer;
            this.currentTemplate = mainTemplate;

            if (handler != null)
            {
                SetValue("this", handler);
                handler.BeforeProcess(this);
            }

            ProcessElements(mainTemplate.Elements);

            if (handler != null)
                handler.AfterProcess(this);
        }

        /// <summary>
        /// processes templates and returns string value
        /// 自己添加的方法
        /// </summary>
        public List<string> Process()
        {
            List<string> result=new List<string>();
            StringWriter writer = null;
            if (!isPreview)//生成            
            {
                var maxPagecount = this.getUpTagMaxPagecount();
                var m = 0;
                for (int i = 0; i < maxPagecount; i++)
                {
                    m = i + 1;
                    SetValue("curpage", m.ToString());
                    writer = new StringWriter();
                    Process(writer);
                    result.Add(writer.ToString());
                    if (variables.IsDefined("pagecount") && maxPagecount > 1 && m >= int.Parse(variables["pagecount"].ToString()))
                        break;
                }                           
            }
            else//预览
            {
                SetValue("curpage", 1);
                writer = new StringWriter();
                Process(writer);
                result.Add(writer.ToString());
            }
            return result;
        }

        /// <summary>
        /// 处理不需要分页的模板，原来的方法，只是改了方法名
        /// </summary>
        /// <returns></returns>
        public string SingleProcess()
        {
            StringWriter writer = null;
            writer = new StringWriter();
            Process(writer);
            return writer.ToString();
        }



        /// <summary>
        /// resets all variables. If TemplateManager is used to 
        /// process template multiple times, Reset() must be 
        /// called prior to Process if varialbes need to be cleared
        /// </summary>
        public void Reset()
        {
            variables.Clear();
        }

        /// <summary>
        /// processes list of elements.
        /// This method is mostly used by extenders of the manager
        /// from custom functions or custom tags.
        /// </summary>
        public void ProcessElements(List<Element> list)
        {
            foreach (Element elem in list)
            {
                ProcessElement(elem);
            }
        }

        protected void ProcessElement(Element elem)
        {
            if (elem is Text)
            {
                Text text = (Text)elem;
                WriteValue(text.Data);
            }
            else if (elem is Expression)
                ProcessExpression((Expression)elem);
            else if (elem is TagIf)
                ProcessIf((TagIf)elem);
            else if (elem is Tag)
                ProcessTag((Tag)elem, isPreview);
        }

        protected void ProcessExpression(Expression exp)
        {
            object value = EvalExpression(exp);
            WriteValue(value);
        }

        /// <summary>
        /// evaluates expression.
        /// This method is used by TemplateManager extensibility.
        /// </summary>
        public object EvalExpression(Expression exp)
        {
            currentExpression = exp;

            try
            {

                if (exp is StringLiteral)
                    return ((StringLiteral)exp).Content;
                else if (exp is Name)
                {
                    return GetValue(((Name)exp).Id);
                }
                else if (exp is FieldAccess)
                {
                    FieldAccess fa = (FieldAccess)exp;
                    object obj = EvalExpression(fa.Exp);
                    string propertyName = fa.Field;
                    return EvalProperty(obj, propertyName);
                }
                else if (exp is MethodCall)
                {
                    MethodCall ma = (MethodCall)exp;
                    object obj = EvalExpression(ma.CallObject);
                    string methodName = ma.Name;

                    return EvalMethodCall(obj, methodName, EvalArguments(ma.Args));
                }
                else if (exp is IntLiteral)
                    return ((IntLiteral)exp).Value;
                else if (exp is DoubleLiteral)
                    return ((DoubleLiteral)exp).Value;
                else if (exp is FCall)
                {
                    FCall fcall = (FCall)exp;
                    if (!functions.ContainsKey(fcall.Name))
                    {
                        string msg = string.Format("函数 '{0}' 未定义.", fcall.Name);
                        throw new TemplateRuntimeException(msg, exp.Line, exp.Col);
                    }

                    TemplateFunction func = functions[fcall.Name];
                    object[] values = EvalArguments(fcall.Args);

                    return func(values);
                }
                else if (exp is StringExpression)
                {
                    StringExpression stringExp = (StringExpression)exp;
                    StringBuilder sb = new StringBuilder();
                    foreach (Expression ex in stringExp.Expressions)
                        sb.Append(EvalExpression(ex));

                    return sb.ToString();
                }
                else if (exp is BinaryExpression)
                    return EvalBinaryExpression(exp as BinaryExpression);
                else if (exp is ArrayAccess)
                    return EvalArrayAccess(exp as ArrayAccess);
                else
                    throw new TemplateRuntimeException("无效的表达式类型: " + exp.GetType().Name, exp.Line, exp.Col);

            }
            catch (TemplateRuntimeException ex)
            {
                DisplayError(ex);
                return null;
            }
            catch (Exception ex)
            {
                DisplayError(new TemplateRuntimeException(ex.Message, currentExpression.Line, currentExpression.Col));
                return null;
            }
        }

        protected object EvalArrayAccess(ArrayAccess arrayAccess)
        {
            object obj = EvalExpression(arrayAccess.Exp);

            object index = EvalExpression(arrayAccess.Index);

            if (obj is Array)
            {
                Array array = (Array)obj;
                if (index is Int32)
                {
                    return array.GetValue((int)index);
                }
                else
                    throw new TemplateRuntimeException("索引必须是整数类型.", arrayAccess.Line, arrayAccess.Col);
            }
            else
                return EvalMethodCall(obj, "get_Item", new object[] { index });

        }

        protected object EvalBinaryExpression(BinaryExpression exp)
        {
            switch (exp.Operator)
            {
                case TokenKind.OpOr:
                    {
                        object lhsValue = EvalExpression(exp.Lhs);
                        if (Util.ToBool(lhsValue))
                            return true;

                        object rhsValue = EvalExpression(exp.Rhs);
                        return Util.ToBool(rhsValue);
                    }
                case TokenKind.OpAnd:
                    {
                        object lhsValue = EvalExpression(exp.Lhs);
                        if (!Util.ToBool(lhsValue))
                            return false;

                        object rhsValue = EvalExpression(exp.Rhs);
                        return Util.ToBool(rhsValue);

                    }
                case TokenKind.OpIs:
                    {
                        object lhsValue = EvalExpression(exp.Lhs);
                        object rhsValue = EvalExpression(exp.Rhs);

                        return lhsValue.Equals(rhsValue);
                    }
                case TokenKind.OpIsNot:
                    {
                        object lhsValue = EvalExpression(exp.Lhs);
                        object rhsValue = EvalExpression(exp.Rhs);

                        return !lhsValue.Equals(rhsValue);

                    }
                case TokenKind.OpGt:
                    {
                        object lhsValue = EvalExpression(exp.Lhs);
                        object rhsValue = EvalExpression(exp.Rhs);

                        IComparable c1 = lhsValue as IComparable;
                        IComparable c2 = rhsValue as IComparable;
                        if (c1 == null || c2 == null)
                            return false;
                        else
                            return c1.CompareTo(c2) == 1;

                    }
                case TokenKind.OpLt:
                    {
                        object lhsValue = EvalExpression(exp.Lhs);
                        object rhsValue = EvalExpression(exp.Rhs);

                        IComparable c1 = lhsValue as IComparable;
                        IComparable c2 = rhsValue as IComparable;
                        if (c1 == null || c2 == null)
                            return false;
                        else
                            return c1.CompareTo(c2) == -1;

                    }
                case TokenKind.OpGte:
                    {
                        object lhsValue = EvalExpression(exp.Lhs);
                        object rhsValue = EvalExpression(exp.Rhs);

                        IComparable c1 = lhsValue as IComparable;
                        IComparable c2 = rhsValue as IComparable;
                        if (c1 == null || c2 == null)
                            return false;
                        else
                            return c1.CompareTo(c2) >= 0;

                    }
                case TokenKind.OpLte:
                    {
                        object lhsValue = EvalExpression(exp.Lhs);
                        object rhsValue = EvalExpression(exp.Rhs);

                        IComparable c1 = lhsValue as IComparable;
                        IComparable c2 = rhsValue as IComparable;
                        if (c1 == null || c2 == null)
                            return false;
                        else
                            return c1.CompareTo(c2) <= 0;

                    }
                default:
                    throw new TemplateRuntimeException("操作 " + exp.Operator.ToString() + " 不受支持.", exp.Line, exp.Col);
            }
        }

        protected object[] EvalArguments(Expression[] args)
        {
            object[] values = new object[args.Length];
            for (int i = 0; i < values.Length; i++)
                values[i] = EvalExpression(args[i]);

            return values;
        }

        protected static object EvalProperty(object obj, string propertyName)
        {
            if (obj is StaticTypeReference)
            {
                Type type = (obj as StaticTypeReference).Type;

                PropertyInfo pinfo = type.GetProperty(propertyName, BindingFlags.Public | BindingFlags.IgnoreCase | BindingFlags.GetProperty | BindingFlags.Static);
                if (pinfo != null)
                    return pinfo.GetValue(null, null);

                FieldInfo finfo = type.GetField(propertyName, BindingFlags.Public | BindingFlags.IgnoreCase | BindingFlags.GetField | BindingFlags.Static);
                if (finfo != null)
                    return finfo.GetValue(null);
                else
                    throw new Exception("在 '" + type.Name + "' 类型中无法找到属性名 '" + propertyName + "'.");


            }
            else
            {
                PropertyInfo pinfo = obj.GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.IgnoreCase | BindingFlags.GetProperty | BindingFlags.Instance);

                if (pinfo != null)
                    return pinfo.GetValue(obj, null);

                FieldInfo finfo = obj.GetType().GetField(propertyName, BindingFlags.Public | BindingFlags.IgnoreCase | BindingFlags.GetField | BindingFlags.Instance);

                if (finfo != null)
                    return finfo.GetValue(obj);
                else
                    throw new Exception("在 '" + obj.GetType().Name + "' 类型中无法找到属性名 '" + propertyName + "'.");

            }

        }


        protected object EvalMethodCall(object obj, string methodName, object[] args)
        {
            Type[] types = new Type[args.Length];
            for (int i = 0; i < args.Length; i++)
                types[i] = args[i].GetType();

            if (obj is StaticTypeReference)
            {
                Type type = (obj as StaticTypeReference).Type;
                MethodInfo method = type.GetMethod(methodName,
                    BindingFlags.Public | BindingFlags.IgnoreCase | BindingFlags.Static,
                    null, types, null);

                if (method == null)
                    throw new Exception(string.Format("在类型 {1} 中未找到静态方法 {0}.", methodName, type.Name));

                return method.Invoke(null, args);
            }
            else
            {

                MethodInfo method = obj.GetType().GetMethod(methodName,
                    BindingFlags.Public | BindingFlags.IgnoreCase | BindingFlags.Instance,
                    null, types, null);

                if (method == null)
                    throw new Exception(string.Format("在类型 {1} 中未找到静态方法 {0}.", methodName, obj.GetType().Name));

                return method.Invoke(obj, args);
            }
        }


        protected void ProcessIf(TagIf tagIf)
        {
            bool condition = false;

            try
            {
                object value = EvalExpression(tagIf.Test);

                condition = Util.ToBool(value);
            }
            catch (Exception ex)
            {
                DisplayError("if条件错误: " + ex.Message,
                    tagIf.Line, tagIf.Col);
                return;
            }

            if (condition)
                ProcessElements(tagIf.InnerElements);
            else
                ProcessElement(tagIf.FalseBranch);

        }

        protected void ProcessTag(Tag tag,bool isPreview)
        {
            string name = tag.Name.ToLowerInvariant();
            try
            {
                switch (name)
                {
                    case "template":
                        // skip those, because those are processed first
                        break;
                    case "else":
                        ProcessElements(tag.InnerElements);
                        break;
                    case "apply":
                        object val = EvalExpression(tag.AttributeValue("template"));
                        ProcessTemplate(val.ToString(), tag);
                        break;
                    case "foreach":
                        ProcessForEach(tag);
                        break;
                    case "for":
                        ProcessFor(tag);
                        break;
                    case "set":
                        ProcessTagSet(tag);
                        break;
                    case "uptag":  //自己添加的方法
                        ProcessUptag(tag);
                        break;
                    default:
                        ProcessTemplate(tag.Name, tag);
                        break;

                }
            }
            catch (TemplateRuntimeException ex)
            {
                DisplayError(ex);
            }
            catch (Exception ex)
            {
                DisplayError("'" + name + "' 标签执行错误: " + ex.Message, tag.Line, tag.Col);

            }
        }

        protected void ProcessTagSet(Tag tag)
        {
            Expression expName = tag.AttributeValue("name");
            if (expName == null)
            {
                throw new TemplateRuntimeException("Set 语法缺少 name 属性定义.", tag.Line, tag.Col);
            }

            Expression expValue = tag.AttributeValue("value");
            if (expValue == null)
            {
                throw new TemplateRuntimeException("Set 语法缺少 value 属性定义.", tag.Line, tag.Col);
            }


            string name = EvalExpression(expName).ToString();
            if (!Util.IsValidVariableName(name))
                throw new TemplateRuntimeException("'" + name + "' 为不存在的变量名.", expName.Line, expName.Col);

            object value = EvalExpression(expValue);

            this.SetValue(name, value);
        }

         
        protected void ProcessForEach(Tag tag)
        {
            Expression expCollection = tag.AttributeValue("collection");
            if (expCollection == null)
            {
                throw new TemplateRuntimeException("foreach 语法中必须定义属性: collection.", tag.Line, tag.Col);
            }

            object collection = EvalExpression(expCollection);
            if (!(collection is IEnumerable))
            {
                throw new TemplateRuntimeException("循环中的集合对象必须是enumerable类型或继承至该类型.", tag.Line, tag.Col);
            }

            Expression expVar = tag.AttributeValue("var");
            if (expCollection == null)
            {
                throw new TemplateRuntimeException("foreach 语法缺少 var 属性定义.", tag.Line, tag.Col);
            }
            object varObject = EvalExpression(expVar);
            if (varObject == null)
                varObject = "foreach";
            string varname = varObject.ToString();

            Expression expIndex = tag.AttributeValue("index");
            string indexname = null;
            if (expIndex != null)
            {
                object obj = EvalExpression(expIndex);
                if (obj != null)
                    indexname = obj.ToString();
            }

            IEnumerator ienum = ((IEnumerable)collection).GetEnumerator();
            int index = 0;
            while (ienum.MoveNext())
            {
                index++;
                object value = ienum.Current;
                variables[varname] = value;
                if (indexname != null)
                    variables[indexname] = index;

                ProcessElements(tag.InnerElements);
            }
        }

        protected void ProcessFor(Tag tag)
        {
            Expression expFrom = tag.AttributeValue("from");
            if (expFrom == null)
            {
                throw new TemplateRuntimeException("For 语法缺少 start 属性.", tag.Line, tag.Col);
            }

            Expression expTo = tag.AttributeValue("to");
            if (expTo == null)
            {
                throw new TemplateRuntimeException("For 语法缺少 to 属性.", tag.Line, tag.Col);
            }

            Expression expIndex = tag.AttributeValue("index");
            if (expIndex == null)
            {
                throw new TemplateRuntimeException("For 语法缺少 index 属性.", tag.Line, tag.Col);
            }

            object obj = EvalExpression(expIndex);
            string indexName = obj.ToString();

            int start = Convert.ToInt32(EvalExpression(expFrom));
            int end = Convert.ToInt32(EvalExpression(expTo));

            for (int index = start; index <= end; index++)
            {
                SetValue(indexName, index);
                //variables[indexName] = index;

                ProcessElements(tag.InnerElements);
            }
        }

        protected void ExecuteCustomTag(Tag tag)
        {
            ITagHandler tagHandler = customTags[tag.Name];

            bool processInnerElements = true;
            bool captureInnerContent = false;

            tagHandler.TagBeginProcess(this, tag, ref processInnerElements, ref captureInnerContent);

            string innerContent = null;

            if (processInnerElements)
            {
                TextWriter saveWriter = writer;

                if (captureInnerContent)
                    writer = new StringWriter();

                try
                {
                    ProcessElements(tag.InnerElements);

                    innerContent = writer.ToString();
                }
                finally
                {
                    writer = saveWriter;
                }
            }

            tagHandler.TagEndProcess(this, tag, innerContent);

        }

        protected void ProcessTemplate(string name, Tag tag)
        {
            if (customTags != null && customTags.ContainsKey(name))
            {
                ExecuteCustomTag(tag);
                return;
            }

            UserTemplate useTemplate = currentTemplate.FindTemplate(name);
            if (useTemplate == null)
            {
                string msg = string.Format("模板 '{0}' 未找到.", name);
                throw new TemplateRuntimeException(msg, tag.Line, tag.Col);
            }

            // process inner elements and save content
            TextWriter saveWriter = writer;
            writer = new StringWriter();
            string content = string.Empty;

            try
            {
                ProcessElements(tag.InnerElements);

                content = writer.ToString();
            }
            finally
            {
                writer = saveWriter;
            }

            UserTemplate saveTemplate = currentTemplate;
            variables = new VariableScope(variables);
            variables["innerText"] = content;

            try
            {
                foreach (TagAttribute attrib in tag.Attributes)
                {
                    object val = EvalExpression(attrib.Expression);
                    variables[attrib.Name] = val;
                }

                currentTemplate = useTemplate;
                ProcessElements(currentTemplate.Elements);
            }
            finally
            {
                variables = variables.Parent;
                currentTemplate = saveTemplate;
            }
        }

        /// <summary>
        /// writes value to current writer
        /// </summary>
        /// <param name="value">value to be written</param>
        public void WriteValue(object value)
        {
            if (value == null)
                writer.Write("");
                //writer.Write("[null]");
            else
                writer.Write(value);
        }

        private void DisplayError(Exception ex)
        {
            if (ex is TemplateRuntimeException)
            {
                TemplateRuntimeException tex = (TemplateRuntimeException)ex;
                DisplayError(ex.Message, tex.Line, tex.Col);
            }
            else
                DisplayError(ex.Message, 0, 0);
        }

        private void DisplayError(string msg, int line, int col)
        {
            if (!silentErrors)
                writer.Write("[错误 ({0}, {1}): {2}]", line, col, msg);
        }
    }
}
