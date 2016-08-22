using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dream.Model.Business.Admin
{
    public class VideoModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CoverImage { get; set; }
        public string Profile { get; set; }
        public decimal Price { get; set; }
        public string StudyTarget { get; set; }
        public DateTime CreateDate { get; set; }
        public TB_Category Category { get; set; }
        public TB_Lecturer Lecturer { get; set; }
    }
}
