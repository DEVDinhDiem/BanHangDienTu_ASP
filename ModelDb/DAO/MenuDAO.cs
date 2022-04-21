using ModelDb.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelDb.DAO
{
  public  class MenuDAO
    {
        _2k1ShopDb db = null;

        public MenuDAO()
        {
            db = new _2k1ShopDb();
        }
        public List<Menu> ListByGroupId(int groupId)
        {
            return db.Menus.Where(x => x.TypeID == groupId&& x.Status==true).OrderBy(x=>x.DisplayOrder).ToList();
        }
    }
}
