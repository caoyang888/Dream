using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dream.Model
{
    [Table("SignInLog")]
    public class TB_SignInLog : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string IP { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
