using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dream.Model
{
    [Table("SystemLog")]
    public class TB_SystemLog : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public string Content { get; set; }
        public SystemLogType Type { get; set; }
        public string IP { get; set; }
        public string Remark { get; set; }
        public DateTime CreateDate { get; set; }
    }

    public enum SystemLogType : int
    {
        /// <summary>
        /// 常规记录
        /// </summary>
        [Description("常规记录")]
        Normal = 1,

        /// <summary>
        /// 系统调试
        /// </summary>
        [Description("系统调试")]
        Debug = 2,

        /// <summary>
        /// 程序异常
        /// </summary>
        [Description("程序异常")]
        Danger = 3,
    }
}
