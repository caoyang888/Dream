using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Dream.Model;
using Dream.Model.Business;
using NiceCode;
using System.Data.Entity;

namespace Dream.BLL
{
    public class UserBusiness : BaseBusiness
    {
        public ResultSets SignIn(string phone)
        {
            ResultSets result = new ResultSets();
            using (DbBase db = new DbBase())
            {
                var user = db.TB_User.FirstOrDefault(a => a.Phone == phone);
                if (user == null)
                {
                    result = AddUser(phone);
                    return result;
                }
                else
                {
                    result.errorCode = 0;
                    result.data = GetUserInfo(user.Id);
                }
            }
            return result;
        }

        public ResultSets AddUser(string phone, string nickname = "", string password = "", string wechat = "", string headShowUrl = "")
        {
            ResultSets result = new ResultSets();
            result.errorCode = 1;
            using (DbBase db = new DbBase())
            {
                var user = db.TB_User.FirstOrDefault(u => u.Phone == phone);
                if (user != null)
                {
                    return result;
                }

                user = new TB_User()
                {
                    Nickname = nickname,
                    Phone = phone,
                    CreateDate = DateTime.Now,
                    Money = 0,
                    Password = password,
                    HeadShowUrl = headShowUrl,
                    Wechat = wechat,
                };
                db.Entry(user).State = EntityState.Added;
                if (db.SaveChanges() > 0)
                {
                    result.errorCode = 0;
                    result.data = GetUserInfo(user.Id);
                    //Tools.SendMail(System.Configuration.ConfigurationManager.AppSettings["Coder"], "新用户注册：[" + user.Phone + "]", "IP：[" + Tools.IP + "]");
                }
                else
                {
                    //log
                }
            }
            return result;
        }


        public ResultSets GetUserHistory(HistoryType type, int userId)
        {
            ResultSets result = new ResultSets();
            using (DbBase db = new DbBase())
            {
                var history = db.TB_UserHistory.Where(h => h.HistoryType ==type);
                if (userId > 0)
                {
                    history = history.Where(h => h.UserId == userId);
                }

                var historyList = history.ToList();

                var list = new List<object>();
                var vb = new VideoBusiness();
                for (int i = 0; i < historyList.Count; i++)
                {
                    if (historyList[i].VideoId > 0)
                    {
                        var videoInfo = vb.VideoDetails(historyList[i].VideoId);
                        if (videoInfo != null)
                        {
                            list.Add(videoInfo);
                        }
                    }
                }

                result.data = list;
            }
            return result;
        }

        public object GetUserInfo(int userId = 0)
        {
            using (DbBase db = new DbBase())
            {
                if (userId > 0)
                {
                    var user = db.TB_User.Find(userId);
                    return new
                    {
                        userId = user.Id,
                        nickname = user.Nickname,
                        phone = user.Phone,
                        headShowUrl = user.HeadShowUrl,
                        money = user.Money,
                    };
                }
                return null;
            }
        }
    }
}
