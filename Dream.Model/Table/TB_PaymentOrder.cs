using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dream.Model
{
    [Table("PaymentOrder")]
    public class TB_PaymentOrder : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public decimal ActuallyAmount { get; set; }
        public int OutId { get; set; }
        public string Remark { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
