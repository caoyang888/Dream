using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Dream.Model;
using Dream.Model.Business;
using Dream.Model.Business.Admin;
using NiceCode;
using System.Data.Entity;

namespace Dream.Admin.BLL
{
    public class SystemCore : BaseBusiness
    {
        public List<TB_SystemLog> SystemLog(string key, int pageIndex, int pageSize)
        {
            using (DbBase db = new DbBase())
            {
                var query = db.TB_SystemLog.Where(l => 1 == 1);
                if (!string.IsNullOrEmpty(key))
                {
                    query = query.Where(l => l.Content.Contains(key));
                }
                Total = query.Count();
                return query.OrderByDescending(l => l.CreateDate).Skip(pageIndex * pageSize).Take(pageSize).ToList();
            }
        }

        public bool SignIn(string name, string password)
        {
            password = password.ToMD5();
            using (DbBase db = new DbBase())
            {
                var admin = db.TB_Admin.FirstOrDefault(a => a.UserName == name && a.Password == password);
                if (admin != null)
                {
                    System.Web.HttpContext.Current.Session["admin"] = admin.Id;
                    return true;
                }
            }
            return false;
        }

        public List<SignInLogModel> SignInLog(string key, int pageIndex, int pageSize)
        {
            using (DbBase db = new DbBase())
            {
                var query = from l in db.TB_SignInLog
                            join u in db.TB_User
                            on l.UserId equals u.Id
                            select new SignInLogModel
                            {
                                CreateDate = l.CreateDate,
                                HeadShowUrl = u.HeadShowUrl,
                                Id = l.Id,
                                IP = l.IP,
                                Nickname = u.Nickname,
                                Phone = u.Phone,
                                UserId = u.Id
                            };
                if (!string.IsNullOrEmpty(key))
                {
                    query = query.Where(l => l.Nickname.Contains(key));
                }
                Total = query.Count();
                return query.OrderByDescending(l => l.CreateDate).Skip(pageIndex * pageSize).Take(pageSize).ToList();
            }
        }

        public List<TB_Admin> AdminList()
        {
            using (DbBase db = new DbBase())
            {
                return db.TB_Admin.ToList();
            }
        }

        public ResultSets AddAdmin(string username, string name, string pwd)
        {
            ResultSets result = new ResultSets();
            using (DbBase db = new DbBase())
            {
                var admin = db.TB_Admin.FirstOrDefault(a => a.UserName == name);
                if (admin != null)
                    return result;

                admin = new TB_Admin()
                {
                    CreateDate = DateTime.Now,
                    Password = pwd.ToMD5(),
                    UserName = username,
                    Name = name
                };
                db.Entry(admin).State = EntityState.Added;
                if (db.SaveChanges() > 0)
                    result.errorCode = 0;
            }
            return result;
        }

        public TB_Admin GetAdmin(int id)
        {
            using (DbBase db = new DbBase())
            {
                return db.TB_Admin.Find(id);
            }
        }
    }
}
