using ModelDb.EntityFramework;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelDb.DAO
{
    public class FeedbackDAO
    {
        _2k1ShopDb db = null;

        public FeedbackDAO()
        {
            db = new _2k1ShopDb();
        }
        public Feedback GetByID(long id)
        {
            return db.Feedbacks.Find(id);
        }
        public IEnumerable<Feedback> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Feedback> model = db.Feedbacks;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString) || x.Name.Contains(searchString));
            }

            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }

        /// <summary>
        /// List all Feedback for client
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IEnumerable<Feedback> ListAllPaging(int page, int pageSize)
        {
            IQueryable<Feedback> model = db.Feedbacks;
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }
        public bool ChangeStatus(long id)
        {
            var Feedback = db.Feedbacks.Find(id);
            Feedback.Status = !Feedback.Status;
            db.SaveChanges();
            return Feedback.Status;
        }
        public bool Delete(int id)
        {
            try
            {
                var Feedback = db.Feedbacks.Find(id);
                db.Feedbacks.Remove(Feedback);
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