using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;

namespace NiceCode
{
    public class Tools
    {
        #region IP
        /// <summary>
        /// 获取当前请求IP
        /// </summary>
        public static string IP
        {
            get
            {
                //string ip = System.Web.HttpContext.Current.Request.UserHostAddress;
                //if (ip == "::1")
                //    ip = "127.0.0.1";
                //return ip;

                //取CDN用户真实IP的方法
                //当用户使用代理时，取到的是代理IP
                string result = "unknow";
                try
                {
                    result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                    if (!string.IsNullOrEmpty(result))
                    {
                        //可能有代理  
                        if (result.IndexOf(".") == -1)
                            result = null;
                        else
                        {
                            if (result.IndexOf(",") != -1)
                            {
                                result = result.Replace("  ", "").Replace("'", "");
                                string[] temparyip = result.Split(",;".ToCharArray());
                                for (int i = 0; i < temparyip.Length; i++)
                                {
                                    if (IsIP(temparyip[i]) && temparyip[i].Substring(0, 3) != "10." && temparyip[i].Substring(0, 7) != "192.168" && temparyip[i].Substring(0, 7) != "172.16.")
                                    {
                                        result = temparyip[i];
                                        break;
                                    }
                                }
                                string[] str = result.Split(',');
                                if (str.Length > 0)
                                    result = str[0].ToString().Trim();
                            }
                            else if (IsIP(result))
                                return result;
                        }
                    }
                    if (string.IsNullOrEmpty(result))
                        result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                    if (string.IsNullOrEmpty(result))
                        result = HttpContext.Current.Request.UserHostAddress;
                    if (string.IsNullOrEmpty(result))
                        result = "127.0.0.1";
                }
                catch (Exception ex)
                {
                    return result;
                }
                return result;

                //get { return HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]; }
            }
        }

        private static bool IsIP(string ip)
        {
            return Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }

        #endregion

        #region OperationSystem
        public static string GetOperationSystem(string userAgent)
        {
            string osVersion = "未知";
            if (userAgent.Contains("NT 6.3"))
            {
                osVersion = "Windows 8.1";
            }
            if (userAgent.Contains("NT 6.2"))
            {
                osVersion = "Windows 8";
            }
            if (userAgent.Contains("NT 6.1"))
            {
                osVersion = "Windows 7";
            }
            if (userAgent.Contains("NT 6.0"))
            {
                osVersion = "Windows Vista/Server 2008";
            }
            else if (userAgent.Contains("NT 5.2"))
            {
                osVersion = "Windows Server 2003";
            }
            else if (userAgent.Contains("NT 5.1"))
            {
                osVersion = "Windows XP";
            }
            else if (userAgent.Contains("NT 5"))
            {
                osVersion = "Windows 2000";
            }
            else if (userAgent.Contains("NT 4"))
            {
                osVersion = "Windows NT4";
            }
            else if (userAgent.Contains("Me"))
            {
                osVersion = "Windows Me";
            }
            else if (userAgent.Contains("98"))
            {
                osVersion = "Windows 98";
            }
            else if (userAgent.Contains("95"))
            {
                osVersion = "Windows 95";
            }
            else if (userAgent.Contains("Mac"))
            {
                osVersion = "Mac";
            }
            else if (userAgent.ToLower().Contains("unix"))
            {
                osVersion = "UNIX";
            }
            else if (userAgent.ToLower().Contains("linux"))
            {
                osVersion = "Linux";
            }
            else if (userAgent.ToLower().Contains("sunos"))
            {
                osVersion = "SunOS";
            }

            return osVersion;
        }
        #endregion

        #region  Send Email
        /// <summary>
        /// 发送邮件的方法(发送单个收件人)
        /// </summary>
        /// <param name="toMail">目的邮件地址</param>
        /// <param name="title">发送邮件的标题</param>
        /// <param name="content">发送邮件的内容</param>
        public static void SendMail(string toMail, string title, string content)
        {
            var toMailList = toMail.Split(',');
            if (toMailList.Length > 0)
            {
                for (int i = 0; i < toMailList.Length; i++)
                {
                    SendMail(new MailAddress(toMailList[i]), title, content);
                }
            }
            else
            {
                SendMail(new MailAddress(toMail), title, content);
            }
        }

        public static void SendMail(MailAddress toMail, string title, string content)
        {
            MailMessage message = new MailMessage();
            message.From = new MailAddress("kakaservice@126.com");
            message.To.Add(toMail); //收件人邮箱地址可以是多个以实现群发
            message.Subject = title;
            message.Body = "<html><head><title>" + title + "</title></head><body>" + content + "</body></html>";
            message.IsBodyHtml = true; //是否为html格式 
            message.Priority = MailPriority.High; //发送邮件的优先等级 
            object userState = message;
            var sc = new SmtpClient();

            sc.Host = "smtp.126.com";
            sc.Port = 25; //指定发送邮件端口 
            sc.Credentials = new System.Net.NetworkCredential("kakaservice@126.com", "KakaDaddy2048");
            //sc.Credentials = new System.Net.NetworkCredential("service@kaka.in", "KakaDaddy1024");
            try
            {
                //sc.Send(message);
                sc.SendCompleted += new SendCompletedEventHandler(client_SendCompleted);
                sc.SendAsync(message, userState); //发送邮件 
            }
            catch (Exception ex)
            {
                new TxtLogHelper().Error(ex);
            }
        }

        static void client_SendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            string errorMessage = string.Empty;
            try
            {
                if (e.Error != null)
                {
                    errorMessage = e.Error.Source + " " + e.Error.Message + " " + e.Error.StackTrace;
                    new TxtLogHelper().Info("email send completed" + errorMessage);
                }
            }
            catch (Exception ex)
            {
                new TxtLogHelper().Info("email send completed" + errorMessage);
            }
        }
        #endregion

        #region Device/Request

        public static string Host
        {
            get
            {
                return System.Web.HttpContext.Current.Request.Url.Host;
            }
        }

        public static string CurrentRequestInfo
        {
            get
            {
                string result = string.Empty;
                try
                {
                    result += "get:" + System.Web.HttpContext.Current.Request.Url.ToString();

                    var form = System.Web.HttpContext.Current.Request.Form;

                    if (form == null)
                    {
                        return result;
                    }

                    if (form.Count == 0)
                    {
                        return result;
                    }

                    result += "---post:";
                    foreach (var item in form.Keys)
                    {
                        result += item + "=" + form[item.ToString()] + ",";
                    }
                }
                catch (Exception ex)
                {
                }

                return result;
            }
        }

        public static int CurrentDeviceType
        {
            get
            {
                foreach (var item in System.Web.HttpContext.Current.Request.Headers.Keys)
                {
                    if (System.Web.HttpContext.Current.Request.Headers[item.ToString()].ToLower().Contains("android"))
                    {
                        return 1;//Android
                    }
                    else if (System.Web.HttpContext.Current.Request.Headers[item.ToString()].ToLower().Contains("iphone"))
                    {
                        return 2;//IOS
                    }
                    else if (System.Web.HttpContext.Current.Request.Headers[item.ToString()].ToLower().Contains("windows"))
                    {
                        return 3; //PC
                    }
                    else if (System.Web.HttpContext.Current.Request.Headers[item.ToString()].ToLower().Contains("mac"))
                    {
                        return 4; //MAC
                    }
                }
                return 0; //Unknow
            }
        }

        #endregion
    }
}
