using ModelDb.EntityFramework;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelDb.DAO
{
    public class CategoryDAO
    {
        _2k1ShopDb db = null;
        
        public CategoryDAO()
        {
            db = new _2k1ShopDb();
        }
        public Category GetByID(long id)
        {
            return db.Categories.Find(id);
        }
        public long Insert(Category category)
        {
            
            category.CreatedDate = DateTime.Now;
            db.Categories.Add(category);
            db.SaveChanges();
            return category.ID;
        }
        public long Edit(Category category)
        {
            var Category = db.Categories.Find(category.ID);
            Category.Name = category.Name;
            Category.MetaTitle = category.MetaTitle;
            Category.DisplayOrder = category.DisplayOrder;
            Category.Status = category.Status;
            Category.ShowOnHome = category.ShowOnHome;
            Category.ModifiedBy = category.ModifiedBy;
            category.ModifiedDate = DateTime.Now;
            db.SaveChanges();
            return category.ID;
        }
        public List<Category> ListAll()
        {
            return db.Categories.Where(x => x.Status == true).ToList();
        }
        public ProductCategory ViewDetail(long id)
        {
            return db.ProductCategories.Find(id);
        }
        public IEnumerable<Category> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Category> model = db.Categories;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString) || x.Name.Contains(searchString));
            }

            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }
        public bool ChangeStatus(long id)
        {
            var category = db.Categories.Find(id);
            category.Status = !category.Status;
            db.SaveChanges();
            return category.Status;
        }
        public bool Delete(int id)
        {
            try
            {
                var category = db.Categories.Find(id);
                db.Categories.Remove(category);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}
