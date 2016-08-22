using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dream.Model
{
    [Table("VideoOrder")]
    public class TB_VideoOrder : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int VideoId { get; set; }
        public string Remark { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
