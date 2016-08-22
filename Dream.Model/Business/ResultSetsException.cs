using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dream.Model.Business
{
    public class ResultSetsException : Exception
    {
        public ResultSets Result { get; set; }
        public ResultSetsException(int errorCode)
        {
            Result = new ResultSets();
            Result.errorCode = errorCode;
        }
    }
}
