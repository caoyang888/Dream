using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dream.Model
{
    [Table("Category")]
    public class TB_Category : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
