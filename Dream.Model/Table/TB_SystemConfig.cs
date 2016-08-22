using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dream.Model
{
    [Table("SystemConfig")]
    public class TB_SystemConfig : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public string Remark { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
