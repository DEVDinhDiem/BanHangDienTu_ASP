using ModelDb.EntityFramework;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelDb.DAO
{
   public class SlideDAO
    {
        _2k1ShopDb db = null;
        public SlideDAO()
        {
            db = new _2k1ShopDb();
        }

        public List<Slide> ListAll()
        {
            return db.Slides.Where(x => x.Status == true).OrderBy(y => y.DisplayOrder).ToList();
        }
        public Slide GetByID(long id)
        {
            return db.Slides.Find(id);
        }
        public IEnumerable<Slide> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Slide> model = db.Slides;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Description.Contains(searchString) || x.Description.Contains(searchString));
            }

            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }

        /// <summary>
        /// List all Slide for client
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IEnumerable<Slide> ListAllPaging(int page, int pageSize)
        {
            IQueryable<Slide> model = db.Slides;
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }
        public long Create(Slide Slide)
        {
           
            Slide.CreatedDate = DateTime.Now;
            db.Slides.Add(Slide);
            db.SaveChanges();
            return Slide.ID;
        }
        public long Edit(Slide slide)
        {
            var Slide = db.Slides.Find(slide.ID);
            Slide.Image = slide.Image;
            Slide.Link = slide.Link;
            Slide.Description = slide.Description;
            Slide.DisplayOrder = slide.DisplayOrder;
            db.SaveChanges();
            return Slide.ID;
        }
       
        public bool ChangeStatus(long id)
        {
            var Slide = db.Slides.Find(id);
            Slide.Status = !Slide.Status;
            db.SaveChanges();
            return Slide.Status;
        }
        public bool Delete(int id)
        {
            try
            {
                var Slide = db.Slides.Find(id);
                db.Slides.Remove(Slide);
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
