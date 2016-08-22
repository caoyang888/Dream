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
    public class VideoBusiness : BaseBusiness
    {
        public ResultSets GetVideos(int cate)
        {
            ResultSets result = new ResultSets();
            using (DbBase db = new DbBase())
            {
                var videoQuery = db.TB_Video.Where(v => 1 == 1);
                if (cate > 0)
                {
                    videoQuery = videoQuery.Where(v => v.CategoryId == cate);
                }

                var videoList = videoQuery.ToList();

                var list = new List<object>();
                var vb = new VideoBusiness();
                for (int i = 0; i < videoList.Count; i++)
                {
                    if (videoList[i].Id > 0)
                    {
                        var videoInfo = vb.VideoDetails(videoList[i].Id);
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

        public object VideoDetails(int vid)
        {
            using (DbBase db = new DbBase())
            {
                if (vid > 0)
                {
                    var video = db.TB_Video.Find(vid);
                    var lb = new LecturerBusiness();
                    if (video != null)
                    {
                        return new
                        {
                            id = video.Id,
                            name = video.Name,
                            coverImage = video.CoverImage,
                            price = video.Price,
                            profile = video.Profile,
                            createDate = video.CreateDate.GetFormatString1(),
                            lecturer = lb.LecturerDetails(video.LecturerId),
                            category = CategoryDetails(video.CategoryId),
                            studyTarget = video.StudyTarget,
                        };
                    }
                }
            }
            return null;
        }

        public object CategoryDetails(int cate)
        {
            using (DbBase db = new DbBase())
            {
                var category = db.TB_Category.Find(cate);
                if (category != null)
                {
                    return new
                    {
                        cid = category.Id,
                        name = category.Name,
                    };
                }
            }
            return null;
        }
    }
}