using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using CMS.BLL;
using System.IO;
using CMS.Template.Parser;
using CMS.Template.Parser.AST;
using CMS.Template.Complex;
using CMS.Template.Lexer;
using CMS.Model;
using CMS.HtmlService.Contract;
using Factory;
using CMS.HtmlService;



namespace CMS.TemplateTest
{
    class Program
    {
        public class User
        {
            public User()
            { }
            public int id { get; set; }
            public string name { get; set; }
            public string country { get; set; }
            public string password { get; set; }
        }
        private static List<User> GetUserList()
        {
            List<User> lstUser = new List<User>();
            lstUser.Add(new User { id = 1, name = "张三", country = "中国.浙江", password = "1111" });
            lstUser.Add(new User { id = 2, name = "李四", country = "中国.福建", password = "2222" });
            lstUser.Add(new User { id = 3, name = "王五", country = "中国.江苏", password = "3333" });
            lstUser.Add(new User { id = 4, name = "赵六", country = "中国.江西", password = "4444" });
            lstUser.Add(new User { id = 5, name = "钱七", country = "中国.北京", password = "5555" });
            return lstUser;
        }
        /* 词法解析
          static void Main(string[] args)
        {
            string data = File.ReadAllText(@"E:\软件安装包\AderTemplates_2.0_轻量级模板引擎\NOVA.Demo\bin\Debug\t3.txt");
            TemplateLexer lexer = new TemplateLexer(data);

            Token token = lexer.Next();
            do
            {
                Console.WriteLine(token.Data);
                token = lexer.Next();
              //  Console.ReadLine();
            } while (token.TokenKind != TokenKind.EOF);
            Console.ReadLine();
        }
         */
  
        static void Main(string[] args)
        {
            //string data = File.ReadAllText(@"D:\CMS_V1\code\branches\v1.0\CMS.TemplateTest\template\jrtt.shtml");
            //TemplateLexer lexer = new TemplateLexer(data);
            //TemplateParser parser = new TemplateParser(lexer);
            //List<Element> elems = parser.Parse();//将token流转化为element流
            //TagParser tagParser = new TagParser(elems);
            //elems = tagParser.CreateHierarchy();//构建语法树，明确element父子关系

            //////语义分析和输出
            //UserTemplate template = new UserTemplate("", elems);
            //TemplateManager manager = new TemplateManager(template);
            ////string id = "1";
            ////ArticleBLL articleBll = Factory.BusinessFactory.CreateBll<ArticleBLL>();
            ////Dictionary<string, object> directory = new Dictionary<string, object>();
            ////directory.Add("Id", id);
            ////Article article = articleBll.Get(directory);
            ////manager.SetValue("art", article);
            ////manager.SetValue("maketype", "1");
            //Console.WriteLine(manager.Process(false)[0]);

            //string sourceStr = "abscsssssssss";
            //Console.Write(sourceStr.Substring(0, 2));

           // using (WCFFactory<IEngine> ChannelFactory = Factory.WCFFactory<IEngine>.GetFactorty())
           // {
           //     IEngine proxy = ChannelFactory.CreateChannel();

           ////   生成文章页调用方式
                 
           //     //PageTemplateBLL templateBll = Factory.BusinessFactory.CreateBll<PageTemplateBLL>();
           //     //TemplateBModel template = templateBll.GetTemplateByNewsID(2).ToBModel();
                
           //     //Console.WriteLine(proxy.GetHTMLByArticle(template,2));


           //  //   生成列表页调用方式

           // PageTemplateBLL templateBll = Factory.BusinessFactory.CreateBll<PageTemplateBLL>();
           // Dictionary<string, object> dictionary = new Dictionary<string, object>();
           // dictionary.Add("TemplateId", "7");
           // TemplateBModel template = templateBll.Get(dictionary).ToBModel();
           // List<string> result = proxy.GetHTMLByTemplate(template, false);


           // //    for (int i = 0; i < result.Count;i++ )
           // //    {
           // //        Console.WriteLine(result[i]);
           // //    }              
                
           // //    Console.ReadLine();
           // }

            //PageTemplateBLL templateBll = Factory.BusinessFactory.CreateBll<PageTemplateBLL>();
            //Dictionary<string, object> dictionary = new Dictionary<string, object>();
            //dictionary.Add("ID", "002");
            //TemplateBModel template = templateBll.Get(dictionary).ToBModel();
            //string result = (new Engine()).GetHTMLByTemplate(template, true)[0];
            //File.WriteAllText(@"..\..\list.shtml", result, Encoding.GetEncoding("gb2312"));


            //dictionary.Clear();
            //dictionary.Add("ID", "001");
            //template = templateBll.Get(dictionary).ToBModel();
            //result = (new Engine()).GetHTMLByArticle(template, 3);
            //File.WriteAllText(@"..\..\news.shtml", result, Encoding.GetEncoding("gb2312"));
            //Console.WriteLine("Finished.");
            //Console.ReadLine();

            CreateTest();
            //dictionary.Clear();
            //dictionary.Add("ID", "001");
            //template = templateBll.Get(dictionary).ToBModel();
            //result = (new Engine()).GetHTMLByArticle(template, 3);
            //File.WriteAllText(@"..\..\news.shtml", result, Encoding.GetEncoding("gb2312"));
            //Console.WriteLine("Finished.");
            Console.ReadLine();

        }
    

        //private static void Main(string[] args)
        //{
        //  // ArticleBLL 

        //    ArticleBLL articleBll = Factory.BusinessFactory.CreateBll<ArticleBLL>();
        //   DataTable DT = articleBll.GetPaged(1, 1).Result;
        //   foreach (var s in DT.Rows)
        //   {
        //       foreach (var s1 in DT.Columns)
        //       {
        //           Console.WriteLine(((DataRow)s)[s1.ToString()]);
        //       }
        //   }
        //    Console.Read();
        //}

        //PageTemplateBLL templateBll = Factory.BusinessFactory.CreateBll<PageTemplateBLL>();
        //Dictionary<string, object> dictionary = new Dictionary<string, object>();
        //dictionary.Add("ID", templateId);
        //PageTemplate template = templateBll.Get(dictionary);

        public static void CreateTest()
        {
            //string filePath = AppDomain.CurrentDomain.BaseDirectory + "template\\index_template.htm";
            //TemplateManager tm = TemplateManager.FromFile(filePath, false);
            string data = "<div><#uptag  tagtype=\"01\" clsid=\"001\" pagesize=\"2\" pagecount=\"3\" viewedit=\"1\" id=\"dataId007\"  var=\"d\" index=\"i\"><#/uptag>  ${art.Title} ${Url} <ul><li><#abc  m=\"1\"><#/abc></li></ul></div>";
            TemplateManager tm = TemplateManager.FromString(data, false);
            UserTemplate ut = UserTemplate.FromString("", data);
            Console.WriteLine(ut.Elements.Count.ToString());
            foreach (var e in ut.Elements)
            {
                //Text
                var text = e as Text;
                if (text != null)
                {
                    Console.WriteLine("Text:{0},Col:{1},Line:{2}", text.Data, text.Col.ToString(), text.Line.ToString());
                }
                Console.WriteLine("\r");

                //Tag
                var tag = e as Tag;
                if(tag != null)
                {
                    Console.WriteLine("TagName:{0},Col:{1},Line:{2}", tag.Name,tag.Col.ToString(),tag.Line.ToString());
                    Console.WriteLine("tag attributes start ....");
                    foreach (var m in tag.Attributes)
                    {
                        Console.WriteLine(m.Name);
                        
                        if (m.Expression is StringLiteral)
                        {
                            var sl = (StringLiteral)m.Expression;
                            Console.WriteLine("StringLiteral:{0}", sl.Content);
                        }
                        if (m.Expression is FieldAccess)
                        {
                            var fa = (FieldAccess)m.Expression;
                            Console.WriteLine("FieldAccess:{0}", fa.Field);
                        }                       
                    }
                    Console.WriteLine("tag attributes end ....");
                    Console.WriteLine("\n");
                }

                //Expression
                var ex = e as Expression;
                if (ex != null)
                {
                    Console.WriteLine("Expression Start...");
                    if (ex is StringLiteral)
                    {
                        var sl = (StringLiteral)ex;
                        Console.WriteLine("StringLiteral:{0}", sl.Content);
                    }
                    if (ex is FieldAccess)
                    {
                        var fa = (FieldAccess)ex;
                        if (fa.Exp is Name)
                        {
                            var name = (Name)fa.Exp;
                            Console.WriteLine("Field's Exp is {0}", name.Id);
                        }
                        Console.WriteLine("FieldAccess:{0}", fa.Field);
                    }
                    if (ex is Name)
                    {
                        var name = (Name)ex;
                        Console.WriteLine("Name's Id:{0}", name.Id);
                    }
                    Console.WriteLine("Expression End....");
                    Console.WriteLine("\r");
                }

                var tagclose = e as TagClose;
                if (tagclose != null)
                {
                    Console.WriteLine("TagClose Start...");
                    Console.WriteLine("TagClose:{0},Col:{1},Line:{1}", tagclose.Name, tagclose.Col.ToString(), tagclose.Line.ToString());
                    Console.WriteLine("TagClose End...");
                }
            }
            
            Console.ReadKey();
        }
    }
}
