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
    public class CategoryController : BaseController
    {
        // GET: Admin/Category
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new CategoryDAO();
            var model = dao.ListAllPaging(searchString, page, pageSize);

            ViewBag.SearchString = searchString;
            return View(model);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Category model)
        {
            if (ModelState.IsValid)
            {
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                model.CreatedBy = session.UserName;
                var currentCulture = Session[CommonConstants.CurrentCulture];
                model.Language = currentCulture.ToString();
                var id = new CategoryDAO().Insert(model);
                if (id > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", StaticResources.Resources.InsertCategoryFailed);
                }
            }
            return View(model);
        }
        [HttpGet]
        public ActionResult Edit(long id)
        {
            var dao = new CategoryDAO();
            var category = dao.GetByID(id);
            return View(category);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Category model)
        {
            if (ModelState.IsValid)
            {
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                model.ModifiedBy = session.UserName;
                model.ModifiedDate=DateTime.Now;
                new CategoryDAO().Edit(model);
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            SetAlert("Xóa tin tức thành công", "success");
            new CategoryDAO().Delete(id);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            var result = new CategoryDAO().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
    }
}