using Dream.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dream.BLL
{
    /// <summary>
    /// 系统配置
    /// </summary>
    public class Config : BaseBusiness
    {
        public Config() { }

        public string GetSystemConfigValueByKey(string key)
        {
            using (DbBase db = new DbBase())
            {
                var o = db.TB_SystemConfig.FirstOrDefault(c => c.Key == key);
                if (o != null)
                {
                    return o.Value;
                }
            }
            return string.Empty;
        }
    }
}
