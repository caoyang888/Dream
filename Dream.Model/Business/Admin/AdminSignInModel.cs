using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dream.Model.Business.Admin
{
    public class AdminSignInModel
    {
        [Required(ErrorMessage = "登录名不能为空！")]
        public string Name { get; set; }

        [Required(ErrorMessage = "密码不能为空！")]
        public string Password { get; set; }

        [Required(ErrorMessage = "验证码不能为空！")]
        public string Code { get; set; }

        public string Message { get; set; }
    }
}
