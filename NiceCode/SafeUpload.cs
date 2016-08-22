using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace NiceCode
{
    public class SafeUpload
    {
        /// <summary>
        /// 默认文件绝对URL
        /// </summary>
        private static string FileUploadAbsoluteURL = ConfigurationManager.AppSettings["FileUploadAbsoluteURL"] == null ? "/UploadImages/" : ConfigurationManager.AppSettings["FileUploadAbsoluteURL"];
        private static string FileSavedAbsolutePath = ConfigurationManager.AppSettings["FileSavedAbsolutePath"] == null ? System.Web.HttpContext.Current.Server.MapPath("\\UploadImages\\") : ConfigurationManager.AppSettings["FileSavedAbsolutePath"];

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="file">文件</param>
        /// <param name="DirectoryName">文件夹名称</param>
        /// <param name="AbsoluteURL">输出参数 文件绝对URL地址</param>
        /// <returns></returns>
        public static FileResult Save(HttpPostedFileBase file, long p = 30)
        {
            FileResult result = null;
            var b = CheckSafe(file);
            if (b)
            {
                result = new FileResult();
                int year = DateTime.Now.Year;
                int month = DateTime.Now.Month;
                int day = DateTime.Now.Day;
                string uploadPath = FileSavedAbsolutePath + year + "\\" + month + "\\" + day + "\\";
                //string uploadPath = System.Web.HttpContext.Current.Server.MapPath("\\UploadImages\\") + year + "\\" + month + "\\" + day + "\\";

                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }
                string fileName = DateTime.Now.ToString("yyyyMMddHHmmssfffff") + System.IO.Path.GetExtension(file.FileName);
                file.SaveAs(uploadPath + fileName);
                result.Length = file.ContentLength;
                result.AbsoluteURL = FileUploadAbsoluteURL + year + "/" + month + "/" + day + "/" + fileName;
                try
                {
                    System.Drawing.Image image = System.Drawing.Image.FromStream(file.InputStream);
                    result.Width = image.Width;
                    result.Height = image.Height;

                    var clearlyImage = System.Drawing.Image.FromStream(new MemoryStream(MakeClearlyImage(uploadPath + fileName, p: p)));
                    clearlyImage.Save(uploadPath + "c" + fileName);

                    result.ClearlyUrl = FileUploadAbsoluteURL + year + "/" + month + "/" + day + "/c" + fileName;
                }
                catch (Exception e)
                {
                    new TxtLogHelper().Error(e);
                    return null;
                }
            }
            return result;
        }


        /// <summary>
        /// 验证文件
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private static bool CheckSafe(HttpPostedFileBase file)
        {
            bool result = false;

            //验证文件是否为空
            if (file == null)
            {
                return result;
            }

            //验证文件大小
            if (file.ContentLength == 0)
            {
                return result;
            }

            //验证文件后缀名
            string fileExtension = System.IO.Path.GetExtension(file.FileName).ToLower();
            string[] allowedExtensions = { ".gif", ".png", ".jpeg", ".jpg", ".rar", ".zip", ".rmvb", ".avi", ".mp4", ".flv", ".swf", ".mkv" };
            for (int i = 0; i < allowedExtensions.Length; i++)
            {
                if (fileExtension == allowedExtensions[i])
                {
                    result = true;
                }
            }

            //验证文件头标识
            //FileExtension[] fe = { FileExtension.BMP, FileExtension.GIF, FileExtension.JPG, FileExtension.PNG, FileExtension.RAR, FileExtension.ZIP, FileExtension.TXT, FileExtension.DOC, FileExtension.DOCX, FileExtension.XLS, FileExtension.XLSX, FileExtension.CHM, FileExtension.PDF, FileExtension.AMR };
            //if (result.result && IsAllowedExtension(file, fe))
            //{
            //    result.result = true;
            //    return result;
            //}
            return result;
        }

        /// <summary>
        /// 验证文件头标示
        /// </summary>
        /// <param name="fu"></param>
        /// <param name="fileEx"></param>
        /// <returns></returns>
        private static bool IsAllowedExtension(HttpPostedFileBase fu, FileExtension[] fileEx)
        {
            int fileLen = fu.ContentLength;
            byte[] imgArray = new byte[fileLen];
            fu.InputStream.Read(imgArray, 0, fileLen);
            MemoryStream ms = new MemoryStream(imgArray);
            System.IO.BinaryReader br = new System.IO.BinaryReader(ms);
            string fileclass = "";
            byte buffer;
            try
            {
                buffer = br.ReadByte();
                fileclass = buffer.ToString();
                buffer = br.ReadByte();
                fileclass += buffer.ToString();
            }
            catch
            {
            }
            br.Close();
            ms.Close();
            foreach (FileExtension fe in fileEx)
            {
                if (Int32.Parse(fileclass) == (int)fe)
                    return true;
            }
            return false;
        }

        public static bool IsPic(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return false;

            string fileExtension = System.IO.Path.GetExtension(fileName).ToLower();
            switch (fileExtension)
            {
                case ".png":
                    return true;
                case ".gif":
                    return true;
                case ".jpg":
                    return true;
                case ".jpeg":
                    return true;
                default:
                    break;
            }
            return false;
        }

        public static bool IsVideo(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return false;

            string fileExtension = System.IO.Path.GetExtension(fileName).ToLower();
            switch (fileExtension)
            {
                case ".rmvb":
                    return true;
                case ".avi":
                    return true;
                case ".mp4":
                    return true;
                case ".flv":
                    return true;
                case ".swf":
                    return true;
                case ".mkv":
                    return true;
                default:
                    break;
            }
            return false;
        }

        public static byte[] MakeClearlyImage(string originalImagePath, long p = 30)
        {
            MemoryStream stream = new MemoryStream();

            //获取原始图片  
            System.Drawing.Image originalImage = System.Drawing.Image.FromFile(originalImagePath);

            //新建一个bmp图片,并设置缩略图大小.  
            System.Drawing.Image bitmap = new System.Drawing.Bitmap(originalImage.Width, originalImage.Height);

            //新建一个画板  
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);

            //设置高质量插值法  
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;

            //设置高质量,低速度呈现平滑程度  
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //清空画布并设置背景色  
            g.Clear(System.Drawing.Color.FromArgb(100, 255, 255, 255));
            //在指定位置并且按指定大小绘制原图片的指定部分  
            //第一个System.Drawing.Rectangle是原图片的画布坐标和宽高,第二个是原图片写在画布上的坐标和宽高,最后一个参数是指定数值单位为像素  
            g.DrawImage(originalImage, new System.Drawing.Point() { X = 0, Y = 0 });

            System.Drawing.Imaging.ImageCodecInfo encoder = System.Drawing.Imaging.ImageCodecInfo.GetImageEncoders().FirstOrDefault(a => a.MimeType == "image/jpeg");
            try
            {
                if (encoder != null)
                {
                    System.Drawing.Imaging.EncoderParameters encoderParams = new System.Drawing.Imaging.EncoderParameters(1);
                    //设置 jpeg 质量为 60
                    encoderParams.Param[0] = new System.Drawing.Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.Quality, p);
                    bitmap.Save(stream, encoder, encoderParams);
                    encoderParams.Dispose();
                    return stream.ToArray();
                }
            }
            catch (System.Exception e)
            {
                new TxtLogHelper().Error(e);
                throw e;
            }
            finally
            {
                originalImage.Dispose();
                bitmap.Dispose();
                g.Dispose();
            }
            return null;
        }
    }

    public enum FileExtension
    {
        JPG = 255216,
        GIF = 7173,
        BMP = 6677,
        PNG = 13780,
        RAR = 8297,
        ZIP = 8075,
        TXT = 102100,
        DOC = 208207,
        XLS = 208207,
        DOCX = 208207,
        XLSX = 208207,
        PDF = 3780,
        CHM = 7384,
        AMR = 1
    }

    public class FileResult
    {
        public string AbsoluteURL { get; set; }
        public string ClearlyUrl { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public int Length { get; set; }
    }
}
