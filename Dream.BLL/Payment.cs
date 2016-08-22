using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Dream.Model;
using Dream.Model.Business;

namespace Dream.BLL
{
    public class Payment : BaseBusiness
    {
        public object CreatePaymentOrder(int userId, decimal money)
        {
            return null;
        }

        public object CreateTradeOrder(int userId, int vid)
        {
            return null;
        }

        public object ConfirmTradeOrder(int orderId)
        {
            return null;
        }

        public object ConfirmPaymentOrder(int orderId)
        {
            return null;
        }
    }
}
