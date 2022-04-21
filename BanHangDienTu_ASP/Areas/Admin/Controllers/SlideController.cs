using BanHangDienTu_ASP.Common;
using ModelDb.DAO;
using ModelDb.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanHangDienTu_ASP.Areas.Admin.Controllers
{
    public class SlideController : BaseController
    {
        // GET: Admin/Slide
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new SlideDAO();
            var model = dao.ListAllPaging(searchString, page, pageSize);

            ViewBag.SearchString = searchString;
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Slide model)
        {
            if (ModelState.IsValid)
            {
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                model.CreatedBy = session.UserName;
                new SlideDAO().Create(model);
                return RedirectToAction("Index");
            }
            return View(model.ID);
        }
        [HttpGet]
        public ActionResult Edit(long id)
        {
            var dao = new SlideDAO();
            var slide = dao.GetByID(id);
            return View(slide);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Slide model)
        {
            if (ModelState.IsValid)
            {
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                model.ModifiedBy = session.UserName;
                model.ModifiedDate = DateTime.Now;
                new SlideDAO().Edit(model);
                return RedirectToAction("Index");
            }
            return View();
        }
      
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new SlideDAO().Delete(id);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            var result = new SlideDAO().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
    }
}