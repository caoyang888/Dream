using Dream.Model.Business;
using NiceCode;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dream.Model.Validation
{
    public class TokenValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value != null)
            {
                var tokenModel = value as UserTokenModel;
                if (tokenModel != null)
                {
                    var result = ValidateToken(tokenModel.userId, tokenModel.token);
                    if (result.errorCode > 0)
                        return false;

                    using (DbBase db = new DbBase())
                    {
                        return db.TB_User.Count(u => u.Id == tokenModel.userId) > 0;
                    }
                }
            }
            return false;
        }
        public override string FormatErrorMessage(string name)
        {
            return ResultSets.Message[1];
        }

        private ResultSets ValidateToken(int userId, string token)
        {
            ResultSets result = new ResultSets();
            result.errorCode = 6;
                       

            if (userId <= 0 || string.IsNullOrEmpty(token))
            {
                return result;
            }

            var desString = DESEncrypt.Decrypt(token);
            if (string.IsNullOrEmpty(desString))
            {
                return result;
            }
            var arr = desString.Split('&');
            if (arr.Length == 2)
            {
                var date = DateTime.Now;
                DateTime.TryParse(arr[1], out date);
                if (date < DateTime.Parse("2015-10-27 10:39:00"))
                {
                    return result;
                }

                if (arr[0].ToInt() > 0 && arr[0].ToInt() == userId)
                {
                    result.errorCode = 0;
                    return result;
                }
            }
            return result;
        }
    }
}
