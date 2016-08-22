using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dream.Model.Business.Admin
{
    public class SignInLogModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Nickname { get; set; }
        public string HeadShowUrl { get; set; }
        public string Phone { get; set; }
        public string IP { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
