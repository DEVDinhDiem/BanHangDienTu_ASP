using ModelDb.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelDb.DAO
{
    public class OrderDetailDAO
    {
        _2k1ShopDb db = null;
        public OrderDetailDAO()
        {
            db = new _2k1ShopDb();
        }
        public bool Insert(OrderDetail detail)
        {
            try
            {
                db.OrderDetails.Add(detail);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;

            }
        }
    }
}
