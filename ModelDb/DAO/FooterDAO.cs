using ModelDb.EntityFramework;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelDb.DAO
{
   public class FooterDAO
    {
        _2k1ShopDb db = null;
        public FooterDAO()
        {
            db = new _2k1ShopDb();
        }
        public Footer GetFooter()
        {
            return db.Footers.SingleOrDefault(x => x.Status == true);
        }
        public Footer ViewDetail(long id)
        {
            return db.Footers.Find(id);
        }
        public string Create(Footer footer)
        {
            db.Footers.Add(footer);
            db.SaveChanges();
            return "Index";
        }
        public bool Edit(Footer footer)
        {

            var Footer = db.Footers.Find(footer.ID);
            Footer.ID = footer.ID;
            Footer.Content = footer.Content;
            Footer.Status = footer.Status;
            db.SaveChanges();
            return true;

        }
        public bool ChangeStatus(long id)
        {
            var Footer = db.Footers.Find(id);
            Footer.Status = !Footer.Status;
            db.SaveChanges();
            return Footer.Status;
        }
        public bool Delete(int id)
        {
            try
            {
                var Footer = db.Footers.Find(id);
                db.Footers.Remove(Footer);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public IEnumerable<Footer> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Footer> model = db.Footers;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Content.Contains(searchString) || x.Content.Contains(searchString));
            }

            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }
    }
}
