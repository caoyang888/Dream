using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Dream.Model;
using Dream.Model.Business;
using NiceCode;

namespace Dream.BLL
{
    public class LecturerBusiness : BaseBusiness
    {
        public object LecturerDetails(int lid)
        {
            using (DbBase db = new DbBase())
            {
                var lecturer = db.TB_Lecturer.Find(lid);
                if (lecturer != null)
                {
                    return new
                    {
                        nickname = lecturer.Nickname,
                        id = lecturer.Id,
                        profile = lecturer.Profile,
                        headShowUrl = lecturer.HeadShowUrl,
                        createDate = lecturer.CreateDate.GetFormatString1(),
                    };
                }
            }
            return null;
        }
    }
}
