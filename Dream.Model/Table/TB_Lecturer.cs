using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dream.Model
{
    [Table("Lecturer")]
    public class TB_Lecturer : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public string Nickname { get; set; }
        public string Profile { get; set; }
        public string HeadShowUrl { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
