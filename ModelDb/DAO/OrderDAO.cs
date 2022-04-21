using ModelDb.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelDb.DAO
{
    public class OrderDAO
    {
        _2k1ShopDb db = null;
        public OrderDAO()
        {
            db = new _2k1ShopDb();
        }
        public long Insert(Order order)
        {
            db.Orders.Add(order);
            db.SaveChanges();
            return order.ID;
        }
    }
}
