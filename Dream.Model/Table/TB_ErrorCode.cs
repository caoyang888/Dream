using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dream.Model
{
    [Table("ErrorCode")]
    public class TB_ErrorCode : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public string Remark { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
