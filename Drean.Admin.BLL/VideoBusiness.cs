using Dream.Model;
using Dream.Model.Business;
using Dream.Model.Business.Admin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dream.Admin.BLL
{
    public class VideoBusiness : BaseBusiness
    {
        #region Video
        public List<VideoModel> VideoList(string key = "", int pageIndex = 0, int pageSize = 20)
        {
            var list = new List<VideoModel>();
            using (DbBase db = new DbBase())
            {
                var query = db.TB_Video.Where(v => 1 == 1);

                if (!string.IsNullOrEmpty(key))
                {
                    query = query.Where(v => v.Name.Contains(key));
                }
                Total = query.Count();
                query = query.OrderByDescending(v => v.CreateDate).Skip(pageIndex * pageSize).Take(pageSize);
                var lb = new LecturerBusiness();
                list = query.ToList().Select(v => new VideoModel
                {
                    CoverImage = v.CoverImage,
                    CreateDate = v.CreateDate,
                    Id = v.Id,
                    Name = v.Name,
                    Profile = v.Profile,
                    Price = v.Price,
                    StudyTarget = v.StudyTarget,
                    Category = CategoryInfo(v.CategoryId),
                    Lecturer = lb.LecturerDetails(v.LecturerId),
                }).ToList();
            }
            return list;
        }

        public VideoModel VideoDetails(int id)
        {
            VideoModel model = null;
            using (DbBase db = new DbBase())
            {
                var video = db.TB_Video.Find(id);
                if (video != null)
                {
                    var lb = new LecturerBusiness();
                    model = new VideoModel()
                    {
                        CoverImage = video.CoverImage,
                        CreateDate = video.CreateDate,
                        Id = video.Id,
                        Name = video.Name,
                        Profile = video.Profile,
                        Price = video.Price,
                        StudyTarget = video.StudyTarget,
                        Category = CategoryInfo(video.CategoryId),
                        Lecturer = lb.LecturerDetails(video.LecturerId),
                    };
                }
            }
            return model;
        }

        public ResultSets EditVideo(int id, string name, string coverImage, string profile, string studyTarget, int lecturerId, decimal price, int categoryId)
        {
            ResultSets result = new ResultSets();
            using (DbBase db = new DbBase())
            {
                var video = db.TB_Video.Find(id);
                if (video != null)
                {
                    if (!string.IsNullOrEmpty(coverImage))
                    {
                        video.CoverImage = coverImage;
                    }
                    video.Name = name;
                    video.Profile = profile;
                    video.StudyTarget = studyTarget;
                    video.LecturerId = lecturerId;
                    video.Price = price;
                    video.CategoryId = categoryId;
                    db.Entry(video).State = EntityState.Modified;
                    if (db.SaveChanges() > 0)
                    {
                        result.errorCode = 0;
                    }
                }
            }
            return result;
        }

        public ResultSets AddVideo(string name, string coverImage, string profile, string studyTarget, int lecturerId, decimal price, int categoryId)
        {
            ResultSets result = new ResultSets();
            using (DbBase db = new DbBase())
            {
                var video = new TB_Video()
                {
                    Name = name,
                    CoverImage = coverImage,
                    Profile = profile,
                    StudyTarget = studyTarget,
                    LecturerId = lecturerId,
                    Price = price,
                    CategoryId = categoryId,
                    CreateDate = DateTime.Now,
                };
                db.Entry(video).State = EntityState.Added;
                if (db.SaveChanges() > 0)
                {
                    result.errorCode = 0;
                }
            }
            return result;
        }

        public ResultSets DeleteVideo(int id)
        {
            ResultSets result = new ResultSets();
            using (DbBase db = new DbBase())
            {
                var video = db.TB_Video.Find(id);
                if (video != null)
                {
                    db.Entry(video).State = EntityState.Deleted;
                    if (db.SaveChanges() > 0)
                    {
                        result.errorCode = 0;
                    }
                }
            }
            return result;
        }

        #endregion

        #region Category
        public TB_Category CategoryInfo(int id)
        {
            using (DbBase db = new DbBase())
            {
                return db.TB_Category.Find(id);
            }
        }

        public List<TB_Category> Categories()
        {
            using (DbBase db = new DbBase())
            {
                return db.TB_Category.ToList();
            }
        }

        public ResultSets AddCategory(string name)
        {
            ResultSets result = new ResultSets();
            using (DbBase db = new DbBase())
            {
                var category = new TB_Category()
                {
                    CreateDate = DateTime.Now,
                    Name = name,
                };
                db.Entry(category).State = EntityState.Added;
                if (db.SaveChanges() > 0)
                {
                    result.errorCode = 0;
                }
            }
            return result;
        }

        public ResultSets EditCategory(int id, string name)
        {
            ResultSets result = new ResultSets();
            using (DbBase db = new DbBase())
            {
                var category = db.TB_Category.Find(id);
                category.Name = name;
                db.Entry(category).State = EntityState.Modified;
                if (db.SaveChanges() > 0)
                {
                    result.errorCode = 0;
                }
            }
            return result;
        }
        #endregion
    }
}
