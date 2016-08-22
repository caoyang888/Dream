using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dream.Model
{
    [Table("User")]
    public class TB_User : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public string Nickname { get; set; }
        public string HeadShowUrl { get; set; }
        public string Phone { get; set; }
        public string Wechat { get; set; }
        public string Password { get; set; }
        public decimal Money { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
