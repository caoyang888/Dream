using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Dream.Model.Business
{
    [Serializable]
    /// <summary>
    /// 结果类
    /// </summary>
    public class ResultSets
    {
        public string message { get; set; }
        public object data { get; set; }

        private int _errorCode;

        public int errorCode
        {
            get
            {
                return _errorCode;
            }
            set
            {
                _errorCode = value;
                using (DbBase db = new DbBase())
                {
                    var o = db.TB_ErrorCode.Find(_errorCode);
                    if (o != null)
                    {
                        message = o.Remark;
                    }
                }
            }
        }

        [ScriptIgnore]
        public static _Message Message = new _Message();
    }

    public class _Message
    {
        public string Message { get; set; }
        public string this[int messageId]
        {
            get
            {
                using (DbBase db = new DbBase())
                {
                    var model = db.TB_ErrorCode.FirstOrDefault(m => m.Id == messageId);
                    if (model == null)
                    {
                        return string.Empty;
                    }
                    return model.Remark;
                }
            }
        }
    }
}
