using Dream.Model;
using NiceCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dream.BLL
{
    /// <summary>
    /// 系统日志
    /// </summary>
    public class DbLog
    {
        public DbLog() { }

        public int Total { get; set; }

        public string Add(string content, SystemLogType type = SystemLogType.Normal, string remark = "")
        {
            using (DbBase db = new DbBase())
            {
                var log = new TB_SystemLog()
                {
                    Content = content + " --- " + Tools.CurrentRequestInfo,
                    CreateDate = DateTime.Now,
                    Type = type,
                    IP = Tools.IP,
                };
                if (!string.IsNullOrEmpty(remark))
                {
                    log.Remark = remark;
                }
                else
                {
                    log.Remark = string.Empty;
                }
                db.Entry(log).State = System.Data.Entity.EntityState.Added;
                db.SaveChanges();
                return log.Remark;
            }
        }

        public void Add(Exception ex)
        {
            Add(ex.Source + " " + ex.Message + " " + ex.StackTrace, SystemLogType.Danger);
        }
    }
}
