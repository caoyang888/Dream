using Dream.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Dream.Model.Business;
using System.Data.Entity;

namespace Dream.Admin.BLL
{
    public class UserBusiness : BaseBusiness
    {
        public List<TB_User> UserList(string key = "", int pageIndex = 0, int pageSize = 20)
        {
            var list = new List<TB_User>();
            using (DbBase db = new DbBase())
            {
                var query = db.TB_User.Where(v => 1 == 1);

                if (!string.IsNullOrEmpty(key))
                {
                    query = query.Where(v => v.Nickname.Contains(key));
                }
                Total = query.Count();
                query = query.OrderByDescending(v => v.CreateDate).Skip(pageIndex * pageSize).Take(pageSize);
                list = query.ToList();
            }
            return list;
        }

        public TB_User UserDetails(int id)
        {
            using (DbBase db = new DbBase())
            {
                return db.TB_User.Find(id);
            }
        }

        public ResultSets EditUser(int id, string nickname, string headShowUrl, decimal money)
        {
            ResultSets result = new ResultSets();
            using (DbBase db = new DbBase())
            {
                var user = db.TB_User.Find(id);
                if (user == null)
                    return result;
                user.Nickname = nickname;
                user.Money = money;
                if (!string.IsNullOrEmpty(headShowUrl))
                    user.HeadShowUrl = headShowUrl;

                db.Entry(user).State = EntityState.Modified;
                if (db.SaveChanges() > 0)
                    result.errorCode = 0;
            }
            return result;
        }
    }
}
