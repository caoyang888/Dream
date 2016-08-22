using Dream.Model;
using Dream.Model.Business;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dream.Admin.BLL
{
    public class LecturerBusiness : BaseBusiness
    {
        public TB_Lecturer LecturerDetails(int id)
        {
            using (DbBase db = new DbBase())
            {
                return db.TB_Lecturer.Find(id);
            }
        }

        public List<TB_Lecturer> Lecturers(string key = "")
        {
            using (DbBase db = new DbBase())
            {
                if (!string.IsNullOrEmpty(key))
                {
                    return db.TB_Lecturer.Where(l => l.Nickname.Contains(key)).ToList();
                }
                return db.TB_Lecturer.ToList();
            }
        }

        public ResultSets Edit(int id, string nickname, string profile, string headShowUrl)
        {
            ResultSets result = new ResultSets();
            using (DbBase db = new DbBase())
            {
                var lecturer = db.TB_Lecturer.Find(id);
                if (lecturer != null)
                {
                    if (!string.IsNullOrEmpty(headShowUrl))
                    {
                        lecturer.HeadShowUrl = headShowUrl;
                    }
                    lecturer.Nickname = nickname;
                    lecturer.Profile = profile;
                    db.Entry(lecturer).State = EntityState.Modified;
                    if (db.SaveChanges() > 0)
                    {
                        result.errorCode = 0;
                    }
                }
            }
            return result;
        }

        public ResultSets Add(string nickname, string profile, string headShowUrl)
        {
            ResultSets result = new ResultSets();
            using (DbBase db = new DbBase())
            {
                var lecturer = new TB_Lecturer();
                if (!string.IsNullOrEmpty(headShowUrl))
                {
                    lecturer.HeadShowUrl = headShowUrl;
                }
                lecturer.Nickname = nickname;
                lecturer.Profile = profile;
                db.Entry(lecturer).State = EntityState.Added;
                if (db.SaveChanges() > 0)
                {
                    result.errorCode = 0;
                }
            }
            return result;
        }
    }
}
