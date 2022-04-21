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
    public class ProductCategoryController : BaseController
    {
        // GET: Admin/ProductCategory
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new ProductCategoryDAO();
            var model = dao.ListAllPaging(searchString, page, pageSize);

            ViewBag.SearchString = searchString;
            return View(model);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(ProductCategory model)
        {
            if (ModelState.IsValid)
            {
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                model.CreatedBy = session.UserName;
                var id = new ProductCategoryDAO().Insert(model);
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
            var dao = new ProductCategoryDAO();
            var productcate = dao.GetByID(id);
            return View(productcate);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(ProductCategory model)
        {
            if (ModelState.IsValid)
            {
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                model.ModifiedBy = session.UserName;
                model.ModifiedDate = DateTime.Now;
                new ProductCategoryDAO().Edit(model);
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            SetAlert("Xóa tin tức thành công", "success");
            new ProductCategoryDAO().Delete(id);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            var result = new ProductCategoryDAO().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
    }
}