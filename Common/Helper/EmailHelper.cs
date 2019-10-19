using System;
using System.Configuration;
using System.Net.Mail;
using System.ComponentModel;

namespace Common
{
    public class EmailHelper
    {
        /// <summary>
        /// �����ʼ�
        /// </summary>
        /// <param name="handler">�����ʼ���Ϻ���¼����������μ�SendCompletedCallback</param>
        /// <param name="host">�ʼ�������������smpt.gmail.com</param>
        /// <param name="from">�ʼ��ķ����ַ�����磺test@gmail.com</param>
        /// <param name="password">�ʼ������ַ������</param>
        /// <param name="displayName">�ʼ������ַҪ��ʾ������</param>
        /// <param name="to">Ҫ���͵���Ŀ���ʼ�</param>
        /// <param name="subject">�ʼ�����</param>
        /// <param name="content">�ʼ�����</param>
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
            string userState = "�����ʼ�";
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
        /// �����ʼ�
        /// </summary>
        /// <param name="host">�ʼ�������</param>
        /// <param name="from">�ʼ��ķ����ַ</param>
        /// <param name="password">�ʼ������ַ������</param>
        /// <param name="displayName">�ʼ������ַҪ��ʾ������</param>
        /// <param name="to">Ҫ���͵���Ŀ���ʼ����������֮����|����</param>
        /// <param name="subject">�ʼ�����</param>
        /// <param name="content">�ʼ�����</param>
        /// <param name="isHtml">�Ƿ�Html</param>
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
                            client.Send(message);       //�����ʼ�

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
