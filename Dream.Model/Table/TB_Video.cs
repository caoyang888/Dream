using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dream.Model
{
    [Table("Video")]
    public class TB_Video : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string CoverImage { get; set; }
        public string Profile { get; set; }
        public int LecturerId { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public string StudyTarget { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
