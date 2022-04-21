using ModelDb.EntityFramework;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelDb.DAO
{
   public class ProductCategoryDAO
    {
        _2k1ShopDb db = null;
        public ProductCategoryDAO()
        {
            db = new _2k1ShopDb();
        }
        public ProductCategory GetByID(long id)
        {
            return db.ProductCategories.Find(id);
        }
        public List<ProductCategory> ListAll()
        {
            return db.ProductCategories.Where(x => x.Status == true).OrderBy(x => x.DisplayOrder).ToList();
        }

        public ProductCategory ViewDetail(long id)
        {
            return db.ProductCategories.Find(id);
        }
        public long Insert(ProductCategory productcategory)
        {
            productcategory.CreatedDate = DateTime.Now;
            db.ProductCategories.Add(productcategory);
            db.SaveChanges();
            return productcategory.ID;
        }
        public long Edit(ProductCategory productcate)
        {
            var ProduCategory = db.ProductCategories.Find(productcate.ID);
            ProduCategory.Name = productcate.Name;
            ProduCategory.MetaTitle = productcate.MetaTitle;
            ProduCategory.DisplayOrder = productcate.DisplayOrder;
            ProduCategory.Status = productcate.Status;
            ProduCategory.ModifiedBy = productcate.ModifiedBy;
            ProduCategory.ShowOnHome = productcate.ShowOnHome;
            productcate.ModifiedDate = DateTime.Now;
            db.SaveChanges();
            return ProduCategory.ID;
        }
        public bool ChangeStatus(long id)
        {
            var produCategory = db.ProductCategories.Find(id);
            produCategory.Status = !produCategory.Status;
            db.SaveChanges();
            return produCategory.Status;
        }
        public bool Delete(int id)
        {
            try
            {
                var producategory = db.ProductCategories.Find(id);
                db.ProductCategories.Remove(producategory);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public IEnumerable<ProductCategory> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<ProductCategory> model = db.ProductCategories;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString) || x.Name.Contains(searchString));
            }

            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }
    }
}
