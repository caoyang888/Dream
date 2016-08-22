using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dream.Model
{
    [Table("UserHistory")]
    public class TB_UserHistory : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int VideoId { get; set; }
        public int CategoryId { get; set; }
        public HistoryType HistoryType { get; set; }
        public decimal Price { get; set; }
        public DateTime CreateDate { get; set; }
    }

    public enum HistoryType : int
    {
        View = 1,
        Favorite = 2,
        Buy = 3,
    }
}
