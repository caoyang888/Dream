using System;
using System.IO;

namespace NiceCode
{
    /// <summary>
    /// 文本日志
    /// </summary>
    public class TxtLogHelper
    {
        public string logPath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\" + DateTime.Now.ToString("yyyyMM") + "\\";
        private StreamWriter sw;

        /// <summary>
        /// 添加文本日志
        /// </summary>
        /// <param name="message"></param>
        /// <param name="type"></param>
        private void Add(object message, LogHelperTxtType type)
        {
            if (!Directory.Exists(logPath))
            {
                Directory.CreateDirectory(logPath);
            }
            sw = new StreamWriter(logPath + DateTime.Now.ToString("yyyyMMdd") + ".log", true);

            sw.Write(DateTime.Now + "\t" + type.ToString() + "\t" + message + "\r\n");
            sw.Flush();
            sw.Close();
            sw.Dispose();
            sw = null;
        }

        /// <summary>
        /// 添加文本日志
        /// </summary>
        /// <param name="message"></param>
        public void Info(object message)
        {
            Add(message, LogHelperTxtType.Info);
        }

        /// <summary>
        /// 添加文本日志
        /// </summary>
        /// <param name="ex"></param>
        public void Info(System.Exception ex)
        {
            Add(ex.Message + "\r" + ex.StackTrace, LogHelperTxtType.Info);
        }

        /// <summary>
        /// 添加文本日志
        /// </summary>
        /// <param name="message"></param>
        public void Error(object message)
        {
            Add(message, LogHelperTxtType.Error);
        }

        /// <summary>
        /// 添加文本日志
        /// </summary>
        /// <param name="ex"></param>
        public void Error(System.Exception ex)
        {
            Add(ex.Source + " " + ex.Message + " " + ex.StackTrace, LogHelperTxtType.Error);
        }
    }

    /// <summary>
    /// 日志类型
    /// </summary>
    public enum LogHelperTxtType
    {
        Info,
        Error
    }
}
