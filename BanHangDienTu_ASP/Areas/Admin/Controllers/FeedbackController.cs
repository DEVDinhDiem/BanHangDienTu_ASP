using ModelDb.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanHangDienTu_ASP.Areas.Admin.Controllers
{
    public class FeedbackController : BaseController
    {
        // GET: Admin/Feedback
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new FeedbackDAO();
            var model = dao.ListAllPaging(searchString, page, pageSize);

            ViewBag.SearchString = searchString;
            return View(model);
        }
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            SetAlert("Xóa tin tức thành công", "success");
            new FeedbackDAO().Delete(id);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            var result = new FeedbackDAO().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
    }
}