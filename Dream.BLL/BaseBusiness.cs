using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dream.BLL
{
    public class BaseBusiness
    {
        protected DbLog log;
        public BaseBusiness()
        {
            log = new DbLog();
        }
    }
}
