using System;
using System.Configuration;
using System.Net.Mail;
using System.ComponentModel;

namespace Common
{
    public class EmailHelper
    {
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="handler">发送邮件完毕后的事件处理器，参见SendCompletedCallback</param>
        /// <param name="host">邮件服务器，例如smpt.gmail.com</param>
        /// <param name="from">邮件的发起地址，例如：test@gmail.com</param>
        /// <param name="password">邮件发起地址的密码</param>
        /// <param name="displayName">邮件发起地址要显示的名称</param>
        /// <param name="to">要发送到的目标邮件</param>
        /// <param name="subject">邮件主题</param>
        /// <param name="content">邮件内容</param>
        public static void SendMail(SendCompletedEventHandler handler, string host, string from, string password,string displayName, string to, string subject, string content)
        {
            System.Text.Encoding encoding = System.Text.Encoding.GetEncoding("gb2312");
            SmtpClient client = new SmtpClient(host);
            client.Credentials = new System.Net.NetworkCredential(from, password);
            // Specify the e-mail sender.
            // Create a mailing address that includes a UTF8 character
            // in the display name.
            MailAddress fromAddress = new MailAddress(from, displayName, encoding);
            // Set destinations for the e-mail message.
            MailAddress toAddress = new MailAddress(to);
            // Specify the message content.
            MailMessage message = new MailMessage(from, to);
            message.Body = content;
            // Include some non-ASCII characters in body and subject.
            message.BodyEncoding = encoding;
            message.Subject = subject;
            message.SubjectEncoding = encoding;
            // Set the method that is called back when the send operation ends.
            client.SendCompleted += handler;
            // The userState can be any object that allows your callback 
            // method to identify this send operation.
            // For this example, the userToken is a string constant.
            string userState = "发送邮件";
            client.SendAsync(message, userState);
            message.Dispose();
        }

        public static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            // Get the unique identifier for this asynchronous operation.
            String token = (string)e.UserState;

            if (e.Cancelled)
            {
                Console.WriteLine("[{0}] Send canceled.", token);
            }
            if (e.Error != null)
            {
                Console.WriteLine("[{0}] {1}", token, e.Error.ToString());
            }
            else
            {
                Console.WriteLine("Message sent.");
            }
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="host">邮件服务器</param>
        /// <param name="from">邮件的发起地址</param>
        /// <param name="password">邮件发起地址的密码</param>
        /// <param name="displayName">邮件发起地址要显示的名称</param>
        /// <param name="to">要发送到的目标邮件，多个邮箱之间用|隔开</param>
        /// <param name="subject">邮件主题</param>
        /// <param name="content">邮件内容</param>
        /// <param name="isHtml">是否Html</param>
        /// <returns></returns>
        public static bool SendMail(string host, string from, string password, string displayName, string to, string subject, string content, bool isHtml)
        {
            try
            {
                SmtpClient client = new SmtpClient();
                MailAddress fromAddr = new MailAddress(from);
                client.Host = host;
                client.Port = 25;
                client.Credentials = new System.Net.NetworkCredential(from, password);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                MailMessage message = new MailMessage();
                message.From = fromAddr;
                message.Subject = subject;
                message.Body = content;
                message.IsBodyHtml = isHtml;
                message.Priority = MailPriority.Normal;
                string[] mtuser = to.Split('|');
                foreach (string m in mtuser)
                {
                    if (m != "" && m != null)
                    {
                        message.To.Clear();
                        message.To.Add(m);
                        try
                        {
                            client.Send(message);       //发送邮件

                        }
                        catch (Exception ex)
                        {
                            Loger.Error(ex.Message);
                            return false;
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Loger.Error(ex.Message);
                return false;
            }
        }

        public static bool SendMail(string displayName, string to, string subject, string content, bool isHtml)
        {
            bool flag = SendMail("mail.wlstock.com", "kefu@wlstock.com", "wl333333", displayName, to, subject, content, isHtml);
            return flag;
        }
    }
}
