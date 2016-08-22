using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;

namespace NiceCode
{
    public static class Extension
    {
        public static string Round(this decimal data)
        {
            return Math.Round(data, 2).ToString("0.00");
        }

        public static int ToIntX100(this decimal data)
        {
            return Convert.ToInt32(Math.Round(data, 2) * 100);
        }

        public static decimal ToDecimal(this string data)
        {
            decimal result = 0;
            decimal.TryParse(data, out result);
            return result;
        }

        public static int ToInt(this string data)
        {
            return Convert.ToInt32(data.ToDouble());
        }

        public static long ToLong(this string data)
        {
            return Convert.ToInt64(data.ToDouble());
        }

        public static double ToDouble(this string data)
        {
            double result = 0;
            double.TryParse(data, out result);
            return result;
        }

        public static double ToDouble(this decimal data)
        {
            double result = 0;
            double.TryParse(data.ToString(), out result);
            return Math.Round(result, 2);
        }

        public static bool ToBool(this string data)
        {
            bool result = false;
            bool.TryParse(data, out result);
            return result;
        }

        public static string ToMD5(this string data)
        {
            if (string.IsNullOrEmpty(data))
                return string.Empty;
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(data, "md5");
        }

        public static List<decimal> ToDecimalList(this string data)
        {
            List<decimal> list = new List<decimal>();
            if (!string.IsNullOrEmpty(data))
            {
                var stringArray = data.Split(',').ToList();
                for (int i = 0; i < stringArray.Count; i++)
                {
                    list.Add(stringArray[i].ToDecimal());
                }
            }
            return list;
        }

        public static List<int> ToIntList(this string data)
        {
            List<int> list = new List<int>();
            if (!string.IsNullOrEmpty(data))
            {
                var stringArray = data.Split(',').ToList();
                for (int i = 0; i < stringArray.Count; i++)
                {
                    list.Add(stringArray[i].ToInt());
                }
            }
            return list;
        }

        public static List<double> ToDoubleList(this string data)
        {
            List<double> list = new List<double>();
            if (!string.IsNullOrEmpty(data))
            {
                var stringArray = data.Split(',').ToList();
                for (int i = 0; i < stringArray.Count; i++)
                {
                    list.Add(stringArray[i].ToDouble());
                }
            }
            return list;
        }

        public static string HtmlFormat(this string data)
        {
            return data.Replace("<", "&lt;").Replace(">", "&gt;").Replace("\"", "&quot;");
        }

        public static int Value(this Enum value)
        {
            return Convert.ToInt32(value);
        }

        public static string GetDayOfWeek(this Enum value)
        {
            var d = Convert.ToInt32(value);
            string s = "周一";
            switch (d)
            {
                case 0:
                    s = "周日";
                    break;
                case 1:
                    s = "周一";
                    break;
                case 2:
                    s = "周二";
                    break;
                case 3:
                    s = "周三";
                    break;
                case 4:
                    s = "周四";
                    break;
                case 5:
                    s = "周五";
                    break;
                case 6:
                    s = "周六";
                    break;
                default:
                    break;
            }
            return s;
        }

        /// <summary>
        /// xx分钟前 xx小时前 昨天 xx月xx日
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string GetFormatString1(this DateTime date)
        {
            string v = string.Empty;
            var s = Convert.ToInt32((DateTime.Now - date).TotalSeconds);
            if (s <= 60 && s >= 0)
            {
                v = s + "秒前";
            }
            else if (s >= 60 && s < 3600)
            {
                v = Convert.ToInt32(s / 60).ToString() + "分钟前";
            }
            else if (s >= 3600 && s < 86400)
            {
                v = Convert.ToInt32(s / 3600).ToString() + "小时前";
            }
            else
            {
                s = Convert.ToInt32((date - DateTime.Now.Date).TotalSeconds);
                if (s >= 86400 && s < 172800)
                {
                    v = "昨天";
                }
                else
                {
                    v = date.ToString("MM月dd日");
                }
            }
            return v;
        }

        /// <summary>
        ///  xx月xx日 昨天 今天 明天 xx月xx日
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string GetFormatString2(this DateTime date)
        {
            string v = string.Empty;
            var s = Convert.ToInt32((date - DateTime.Now.Date).TotalSeconds);
            if (s < 86400 && s > 0)
            {
                v = "今天";
            }
            else if (s >= 86400 && s < 172800)
            {
                v = "昨天";
            }
            else
            {
                v = date.ToString("MM月dd日");
            }
            return v;
        }

        /// <summary>
        ///  yyyy年MM月dd日
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string GetFormatString3(this DateTime date)
        {
            return date.ToString("yyyy年MM月dd日");
        }

        public static long GetJsTimeString(this DateTime date)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));
            return (date.Ticks - startTime.Ticks) / 10000;
        }

        /// <summary>
        /// 返回枚举项的描述信息。
        /// </summary>
        /// <param name="value">要获取描述信息的枚举项。</param>
        /// <returns>枚举想的描述信息。</returns>
        public static string Description(this Enum value, bool isTop = false)
        {
            Type enumType = value.GetType();
            DescriptionAttribute attr = null;
            if (isTop)
            {
                attr = (DescriptionAttribute)Attribute.GetCustomAttribute(enumType, typeof(DescriptionAttribute));
            }
            else
            {
                // 获取枚举常数名称。
                string name = Enum.GetName(enumType, value);
                if (name != null)
                {
                    // 获取枚举字段。
                    FieldInfo fieldInfo = enumType.GetField(name);
                    if (fieldInfo != null)
                    {
                        // 获取描述的属性。
                        attr = Attribute.GetCustomAttribute(fieldInfo, typeof(DescriptionAttribute), false) as DescriptionAttribute;
                    }
                }
            }

            if (attr != null && !string.IsNullOrEmpty(attr.Description))
                return attr.Description;
            else
                return string.Empty;

        }

        public static string Name(this Enum value)
        {
            var type = value.GetType();
            return Enum.GetName(type, value);
        }

        public static string ToString32(this Guid value)
        {
            if (value == null)
                return string.Empty;
            return value.ToString().Replace("-", "");
        }

        public static double ToDateTimeDouble(this DateTime date)
        {
            TimeZone tz = TimeZone.CurrentTimeZone;
            DateTime dtZone = new DateTime(1970, 1, 1, 0, 0, 0);
            return date.Subtract(dtZone).TotalMilliseconds;
        }

        public static double ToDateTimeInt(this DateTime date)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (int)(date - startTime).TotalSeconds;
        }

        public static string SubstringCase1(this string data)
        {
            var returnValue = data;
            try
            {
                var length = data.Length;
                if (length > 6 && length < 10)
                {
                    returnValue = data.Substring(0, 4) + "***" + data.Substring(6, length - 6);
                }
                else if (length > 10 && length < 16)
                {
                    returnValue = data.Substring(0, 4) + "***" + data.Substring(10, length - 10);
                }
                else if (length > 16 && length < 20)
                {
                    returnValue = data.Substring(0, 4) + "***" + data.Substring(16, length - 16);
                }
            }
            catch { }
            return returnValue;
        }

        public static XmlDocument ToXmlDocument(this XDocument xDocument)
        {
            var xmlDocument = new XmlDocument();
            using (var xmlReader = xDocument.CreateReader())
            {
                xmlDocument.Load(xmlReader);
            }
            return xmlDocument;
        }

        public static XDocument ToXDocument(this XmlDocument xmlDocument)
        {
            using (var nodeReader = new XmlNodeReader(xmlDocument))
            {
                nodeReader.MoveToContent();
                return XDocument.Load(nodeReader);
            }
        }

        public static string DistanceTxt(this  double? data)
        {
            if (data <= 50)
            {
                return "50米以内";
            }
            else if (data > 50 && data <= 500)
            {
                return "500米以内";
            }
            else if (data > 500 && data <= 1000)
            {
                return "1公里以内";
            }
            else if (data > 1000 && data <= 5000)
            {
                return "5公里以内";
            }
            else if (data > 5000 && data <= 10000)
            {
                return "10公里以内";
            }
            return "10公里以上";
        }
    }
}
